using HRRS.Dto;
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

        public async Task<ResultWithDataDto<List<RegistrationRequestDto?>>> GetAllRegistrationRequestsAsync(long userId)
        {
            var requestQuery = _context.RegistrationRequests
                .Include(x => x.HandledBy)
                .Include(x => x.HealthFacility)
                .AsQueryable();

            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId);
            if(user!.Role is not null && user!.Role.Title != Role.SuperAdmin)
            {
                requestQuery = requestQuery.Where(x => x.HealthFacility.BedCount == user.Role.BedCount);
            }

            var requests = await requestQuery
                .OrderByDescending(x => x.CreatedAt)
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
                BedCount = x.HealthFacility.BedCount
            }).ToListAsync();

            return ResultWithDataDto<List<RegistrationRequestDto?>>.Success(requests);
        }

        public async Task<ResultWithDataDto<RegistrationRequestWithFacilityDto?>> GetRegistrationRequestByIdAsync(int id)
        {
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
                .Select(x => new RegistrationRequestWithFacilityDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    HandledBy = x.HandledBy != null ? x.HandledBy.UserName : null,
                    HandledById = x.HandledById,
                    HealthFacility = new RegisterFacilityDto
                    {
                        FacilityName = x.HealthFacility.FacilityName,
                        FacilityType = x.HealthFacility.FacilityType.HOSP_TYPE,
                        PanNumber = x.HealthFacility.PanNumber,
                        BedCount = x.HealthFacility.BedCount,
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
                    Remarks = x.Remarks

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

            var user = await _context.Users.FindAsync(handledById);
            if (user == null) 
                return ResultWithDataDto<string>.Failure("User not found");

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
                .Where(x => x.Status == RequestStatus.Pending)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (request == null)
                return ResultWithDataDto<string>.Failure("Registration request not found");

            if (await _context.HealthFacilities.AnyAsync(x => x.PanNumber == request.HealthFacility.PanNumber))
                return ResultWithDataDto<string>.Failure("Health Facility already exists");

            if (await _context.HealthFacilities.AnyAsync(x => x.FacilityEmail != null && x.FacilityEmail.Equals(request.HealthFacility.Email))) return ResultWithDataDto<string>.Failure("Email already exists");
            if (await _context.HealthFacilities.AnyAsync(x => x.FacilityPhoneNumber != null && x.FacilityPhoneNumber.Equals(request.HealthFacility.PhoneNumber))) return ResultWithDataDto<string>.Failure("Phone number already exists");

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
                FacilityHeadPhone = request.HealthFacility.MobileNumber

            };
            await _context.HealthFacilities.AddAsync(healthFacility);

            var newUser = new User
            {
                UserName = dto.Username,
                Password = AuthService.GenerateHashedPassword(dto.Password),
                HealthFacility = healthFacility,
                IsFirstLogin = true
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            //TODO: 
            //create a service that sends mail to the health facility with the username and password

            return ResultWithDataDto<string>.Success($"Registration request approved successfully.\n The username is {newUser.UserName} and password is {dto.Password}");
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

    }
}
