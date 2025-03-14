﻿using HRRS.Dto;
using HRRS.Dto.Auth;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.RegistrationRequest;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Services.Implementation
{
    public class RegistrationRequestService : IRegistrationRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileService;

        public RegistrationRequestService(ApplicationDbContext context, IFileUploadService service)
        {
            _context = context;
            _fileService = service;
        }

        // admins only
        public async Task<ResultWithDataDto<List<RegistrationRequestDto?>>> GetAllRegistrationRequestsAsync(long userId)
        {
            var requestQuery = _context.RegistrationRequests
                .Include(x => x.HandledBy)
                .Include(x => x.HealthFacility)
                .AsQueryable();

            var user = (await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId))!;

            //if(user!.Role is not null && user!.Role.Title != Role.SuperAdmin)
            //    requestQuery = requestQuery.Where(x => user.Role.FacilityTypes.Contains(x.HealthFacility.FacilityType));

            if (user.Role is not null)
            {
                requestQuery = requestQuery.Where(x => x.RoleId == user.RoleId);
            }

            var requests = await requestQuery
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.HealthFacility)
                .ThenInclude(x => x.BedCount)
                .Select(x => new RegistrationRequestDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                HandledBy = x.HandledBy != null ? x.HandledBy.UserName : null,
                HandledById = x.HandledById,
                FacilityId = x.HealthFacility.Id,
                FacilityName = x.HealthFacility.FacilityName,
                Status = x.Status,
                BedCount = x.HealthFacility.BedCount.Count,
                Remarks = x.Remarks
            }).ToListAsync();

            return ResultWithDataDto<List<RegistrationRequestDto?>>.Success(requests);
        }

        public async Task<ResultWithDataDto<RegistrationRequestWithFacilityDto?>> GetRegistrationRequestByIdAsync(int id)
        {
            var request = await _context.RegistrationRequests
                .Select(x => new RegistrationRequestWithFacilityDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    HandledBy = x.HandledBy != null ? x.HandledBy.UserName : null,
                    HandledById = x.HandledById,
                    Remarks = x.Remarks,
                    HealthFacility = new RegisterFacilityDto
                    {
                        FacilityName = x.HealthFacility.FacilityName,
                        FacilityType = x.HealthFacility.FacilityType.HOSP_TYPE,
                        PanNumber = x.HealthFacility.PanNumber,
                        BedCount = x.HealthFacility.BedCount.Count,
                        SpecialistCount = x.HealthFacility.SpecialistCount,
                        AvailableServices = x.HealthFacility.AvailableServices,
                        District = x.HealthFacility.District.Name,
                        LocalLevel = x.HealthFacility.LocalLevel.Name,
                        WardNumber = x.HealthFacility.WardNumber,
                        Tole = x.HealthFacility.Tole,
                        Longitude = x.HealthFacility.Longitude,
                        Latitude = x.HealthFacility.Latitude,
                        FilePath = x.HealthFacility.FilePath != null ? _fileService.GetHealthFacilityFilePath(x.HealthFacility.FilePath) : null,
                        Province = x.HealthFacility.Province.Name,
                        MobileNumber = x.HealthFacility.MobileNumber,
                        PhoneNumber = x.HealthFacility.PhoneNumber
                    },
                    Status = x.Status,

                })
                .FirstOrDefaultAsync(x => x.Id == id);

            return request == null
                ? ResultWithDataDto<RegistrationRequestWithFacilityDto?>.Failure("Registration request not found")
                : ResultWithDataDto<RegistrationRequestWithFacilityDto?>.Success(request);
        }

        public async Task<ResultWithDataDto<string>> ApproveRegistrationRequestAsync(int id, long handledById, LoginDto dto)
        {
            if(await _context.Users.AnyAsync(x => x.UserName == dto.Username))
                return ResultWithDataDto<string>.Failure("Username already exists");

            var user = await _context.Users.Include(x => x.Post).FirstOrDefaultAsync(x => x.UserId == handledById);
            if (user == null) 
                return ResultWithDataDto<string>.Failure("User not found");

            if(user.Post != null && user.Post.Post == UserPost.Samiti)
                return ResultWithDataDto<string>.Failure("You are not allowed to preform this action");

            var request = await _context.RegistrationRequests
                .Include(x => x.HandledBy)
                .Include(x => x.HealthFacility)
                .ThenInclude(x => x.District)
                .Include(x => x.HealthFacility)
                .ThenInclude(x => x.LocalLevel)
                .Include(x => x.HealthFacility)
                .ThenInclude(x => x.FacilityType)
                .Include(x => x.HealthFacility)
                .ThenInclude(x => x.Province)
                .Include(x => x.HealthFacility)
                .ThenInclude(x => x.BedCount)
                .Include(x => x.Role)
                .Where(x => x.Status == RequestStatus.Pending)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (request == null)
                return ResultWithDataDto<string>.Failure("Registration request not found");

            request.Status = RequestStatus.Approved;
            request.HandledBy = user;
            request.UpdatedAt = DateTime.Now;

            var healthFacility = new HealthFacility
            {
                FacilityName = request.HealthFacility.FacilityName,
                FacilityType = request.HealthFacility.FacilityType,
                PanNumber = request.HealthFacility.PanNumber,
                BedCount = request.HealthFacility.BedCount,
                SpecialistCount = request.HealthFacility.SpecialistCount,
                AvailableServices = request.HealthFacility.AvailableServices,
                District = request.HealthFacility.District,
                LocalLevel = request.HealthFacility.LocalLevel,
                WardNumber = request.HealthFacility.WardNumber,
                Tole = request.HealthFacility.Tole,
                Longitude = request.HealthFacility.Longitude,
                Latitude = request.HealthFacility.Latitude,
                FilePath = request.HealthFacility.FilePath,
                Province = request.HealthFacility.Province,
                FacilityPhoneNumber = request.HealthFacility.PhoneNumber,
                FacilityEmail = request.HealthFacility.Email,
                FacilityHeadPhone = request.HealthFacility.MobileNumber,
                Role = request.Role,
            };
            await _context.HealthFacilities.AddAsync(healthFacility);

            var newUser = new User
            {
                UserName = dto.Username,
                Password = AuthService.GenerateHashedPassword(dto.Password),
                HealthFacility = healthFacility,
                IsFirstLogin = true,
                FullName = "",
                MobileNumber = request.HealthFacility.MobileNumber,
                DistrictId = request.HealthFacility.DistrictId,
                ProvinceId = request.HealthFacility.ProvinceId,
                FacilityTypeId = request.HealthFacility.FacilityTypeId,
                FacilityEmail = request.HealthFacility.Email,
                PersonalEmail = "",
                TelephoneNumber = request.HealthFacility.PhoneNumber,

            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            //TODO: 
            //create a service that sends mail to the health facility with the username and password

            return ResultWithDataDto<string>.Success($"Registration request approved successfully. The username is {newUser.UserName} and password is {dto.Password}");
        }

        public async Task<ResultWithDataDto<string>> RejectRegistrationRequestAsync(int id, long handledById, StandardRemarkDto dto)
        {
            var request = await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .Where(x => x.Status == RequestStatus.Pending)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
                return ResultWithDataDto<string>.Failure("Registration request not found");
            request.Status = RequestStatus.Rejected;
            request.HandledById = handledById;
            request.UpdatedAt = DateTime.Now;
            request.Remarks = dto.Remarks;

            await _context.SaveChangesAsync();
            return ResultWithDataDto<string>.Success("Registration request rejected successfully");
        }

        //authorized only to Samiti type user
        public async Task<ResultDto> SifarisToPramukh(Guid submissionCode)
        {
            var masterEntry = await _context.MasterStandardEntries.FirstOrDefaultAsync(x => x.SubmissionCode == submissionCode);
            if (masterEntry == null) return ResultDto.Failure("Submission not found");

            masterEntry.EntryStatus = EntryStatus.STP;
            await _context.SaveChangesAsync();
            return ResultDto.Success();

        }

    }
}
