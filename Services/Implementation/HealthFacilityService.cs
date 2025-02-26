using System.Net.Http;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using HRRS.Dto;
using HRRS.Dto.Auth;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Services.Implementation
{
    public class HealthFacilityService : IHealthFacilityService
    {
        private readonly ApplicationDbContext _context;

        public HealthFacilityService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Create(HealthFacilityDto dto)
        {
            var facilityType = await _context.FacilityTypes.FindAsync(dto.FacilityTypeId);
            if (facilityType is null)
                return ResultDto.Failure("Facility type cannot be found");


            var facility = new HealthFacility
            {
                FacilityName = dto.FacilityName,
                FacilityType = facilityType,
                PanNumber = dto.PanNumber,
                BedCount = dto.BedCount,
                SpecialistCount = dto.SpecialistCount,
                AvailableServices = dto.AvailableServices,
                District = dto.District,
                LocalLevel = dto.LocalLevel,
                WardNumber = dto.WardNumber,
                Tole = dto.Tole,
                DateOfInspection = dto.DateOfInspection,
                FacilityEmail = dto.FacilityEmail,
                FacilityPhoneNumber = dto.FacilityPhoneNumber,
                FacilityHeadName = dto.FacilityHeadName,
                FacilityHeadPhone = dto.FacilityHeadPhone,
                FacilityHeadEmail = dto.FacilityHeadEmail,
                ExecutiveHeadName = dto.ExecutiveHeadName,
                ExecutiveHeadMobile = dto.ExecutiveHeadMobile,
                ExecutiveHeadEmail = dto.ExecutiveHeadEmail,
                PermissionReceivedDate = dto.PermissionReceivedDate,
                LastRenewedDate = dto.LastRenewedDate,
                ApporvingAuthority = dto.ApporvingAuthority,
                RenewingAuthority = dto.RenewingAuthority,
                ApprovalValidityTill = dto.ApprovalValidityTill,
                RenewalValidityTill = dto.RenewalValidityTill,
                UpgradeDate = dto.UpgradeDate,
                UpgradingAuthority = dto.UpgradingAuthority,
                IsLetterOfIntent = dto.IsLetterOfIntent,
                IsExecutionPermission = dto.IsExecutionPermission,
                IsRenewal = dto.IsRenewal,
                IsUpgrade = dto.IsUpgrade,
                IsServiceExtension = dto.IsServiceExtension,
                IsBranchExtension = dto.IsBranchExtension,
                IsRelocation = dto.IsRelocation,
                Others = dto.Others,
                ApplicationSubmittedAuthority = dto.ApplicationSubmittedAuthority,
                ApplicationSubmittedDate = dto.ApplicationSubmittedDate

            };


            await _context.HealthFacilities.AddAsync(facility);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }

        public async Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id)
        {
            var healthFacility = await _context.HealthFacilities.Include(x => x.FacilityType).FirstOrDefaultAsync(x => x.Id == id);
            if(healthFacility == null)
            {
                return ResultWithDataDto<HealthFacilityDto>.Failure("स्वास्थ्य संस्था फेला परेन।");
            }
            var healthFacilityDto = new HealthFacilityDto
            {
                Id = id,
                FacilityName = healthFacility.FacilityName,
                FacilityType = healthFacility.FacilityType.Name,
                FacilityTypeId = healthFacility.FacilityTypeId,
                PanNumber = healthFacility.PanNumber,
                BedCount = healthFacility.BedCount,
                SpecialistCount = healthFacility.SpecialistCount,
                AvailableServices = healthFacility.AvailableServices,
                District = healthFacility.District,
                LocalLevel = healthFacility.LocalLevel,
                WardNumber = healthFacility.WardNumber,
                Tole = healthFacility.Tole,
                DateOfInspection = healthFacility.DateOfInspection,
                FacilityEmail = healthFacility.FacilityEmail,
                FacilityPhoneNumber = healthFacility.FacilityPhoneNumber,
                FacilityHeadName = healthFacility.FacilityHeadName,
                FacilityHeadPhone = healthFacility.FacilityHeadPhone,
                FacilityHeadEmail = healthFacility.FacilityHeadEmail,
                ExecutiveHeadName = healthFacility.ExecutiveHeadName,
                ExecutiveHeadMobile = healthFacility.ExecutiveHeadMobile,
                ExecutiveHeadEmail = healthFacility.ExecutiveHeadEmail,
                PermissionReceivedDate = healthFacility.PermissionReceivedDate,
                LastRenewedDate = healthFacility.LastRenewedDate,
                ApporvingAuthority = healthFacility.ApporvingAuthority,
                RenewingAuthority = healthFacility.RenewingAuthority,
                ApprovalValidityTill = healthFacility.ApprovalValidityTill,
                RenewalValidityTill = healthFacility.RenewalValidityTill,
                UpgradeDate = healthFacility.UpgradeDate,
                UpgradingAuthority = healthFacility.UpgradingAuthority,
                IsLetterOfIntent = healthFacility.IsLetterOfIntent,
                IsExecutionPermission = healthFacility.IsExecutionPermission,
                IsRenewal = healthFacility.IsRenewal,
                IsUpgrade = healthFacility.IsUpgrade,
                IsServiceExtension = healthFacility.IsServiceExtension,
                IsBranchExtension = healthFacility.IsBranchExtension,
                IsRelocation = healthFacility.IsRelocation,
                Others = healthFacility.Others,
                ApplicationSubmittedAuthority = healthFacility.ApplicationSubmittedAuthority,
                ApplicationSubmittedDate = healthFacility.ApplicationSubmittedDate
            };
            return ResultWithDataDto<HealthFacilityDto>.Success(healthFacilityDto);
        }

        public async Task<ResultWithDataDto<List<HealthFacilityDto>>> GetAll(HttpContext context)
        {
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                return ResultWithDataDto<List<HealthFacilityDto>>.Failure("Cannot find user");
            }

            var userIdInt = long.Parse(userId);
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userIdInt);
            if (user == null) {
                return ResultWithDataDto<List<HealthFacilityDto>>.Failure("Cannot find user");
            }

            if (user.Role is not null) 
            { 
                var res =  await _context.HealthFacilities
                    .Include(x => x.FacilityType)
                    .Where(x => x.BedCount == user.Role.BedCount)
                    .Select(facility => new HealthFacilityDto() {
                    Id = facility.Id,
                    FacilityName = facility.FacilityName,
                    FacilityType = facility.FacilityType.Name,
                    FacilityTypeId = facility.FacilityTypeId,
                    PanNumber = facility.PanNumber,
                    BedCount = facility.BedCount,
                    SpecialistCount = facility.SpecialistCount,
                    AvailableServices = facility.AvailableServices,
                    District = facility.District,
                    LocalLevel = facility.LocalLevel,
                    WardNumber = facility.WardNumber,
                    Tole = facility.Tole,
                    DateOfInspection = facility.DateOfInspection,
                    FacilityEmail = facility.FacilityEmail,
                    FacilityPhoneNumber = facility.FacilityPhoneNumber,
                    FacilityHeadName = facility.FacilityHeadName,
                    FacilityHeadPhone = facility.FacilityHeadPhone,
                    FacilityHeadEmail = facility.FacilityHeadEmail,
                    ExecutiveHeadName = facility.ExecutiveHeadName,
                    ExecutiveHeadMobile = facility.ExecutiveHeadMobile,
                    ExecutiveHeadEmail = facility.ExecutiveHeadEmail,
                    PermissionReceivedDate = facility.PermissionReceivedDate,
                    LastRenewedDate = facility.LastRenewedDate,
                    ApporvingAuthority = facility.ApporvingAuthority,
                    RenewingAuthority = facility.RenewingAuthority,
                    ApprovalValidityTill = facility.ApprovalValidityTill,
                    RenewalValidityTill = facility.RenewalValidityTill,
                    UpgradeDate = facility.UpgradeDate,
                    UpgradingAuthority = facility.UpgradingAuthority,
                    IsLetterOfIntent = facility.IsLetterOfIntent,
                    IsExecutionPermission = facility.IsExecutionPermission,
                    IsRenewal = facility.IsRenewal,
                    IsUpgrade = facility.IsUpgrade,
                    IsServiceExtension = facility.IsServiceExtension,
                    IsBranchExtension = facility.IsBranchExtension,
                    IsRelocation = facility.IsRelocation,
                    Others = facility.Others,
                    ApplicationSubmittedAuthority = facility.ApplicationSubmittedAuthority,
                    ApplicationSubmittedDate = facility.ApplicationSubmittedDate
                })
                .OrderByDescending(x => x.Id)
                .ToListAsync();

                return ResultWithDataDto<List<HealthFacilityDto>>.Success(res);
            }

            if (role == "Hospital")
            {
                var facility = await _context.HealthFacilities.Include(x => x.FacilityType).Where(x=> x.Id == user.HealthFacilityId).SingleOrDefaultAsync();
                if (facility == null) {
                    return new ResultWithDataDto<List<HealthFacilityDto>>(true, null, "स्वास्थ्य संस्था फेला परेन।");
                }
                var dto = new HealthFacilityDto()
                {
                    Id = facility.Id,
                    FacilityName = facility.FacilityName,
                    FacilityType = facility.FacilityType.Name,
                    FacilityTypeId = facility.FacilityTypeId,
                    PanNumber = facility.PanNumber,
                    BedCount = facility.BedCount,
                    SpecialistCount = facility.SpecialistCount,
                    AvailableServices = facility.AvailableServices,
                    District = facility.District,
                    LocalLevel = facility.LocalLevel,
                    WardNumber = facility.WardNumber,
                    Tole = facility.Tole,
                    DateOfInspection = facility.DateOfInspection,
                    FacilityEmail = facility.FacilityEmail,
                    FacilityPhoneNumber = facility.FacilityPhoneNumber,
                    FacilityHeadName = facility.FacilityHeadName,
                    FacilityHeadPhone = facility.FacilityHeadPhone,
                    FacilityHeadEmail = facility.FacilityHeadEmail,
                    ExecutiveHeadName = facility.ExecutiveHeadName,
                    ExecutiveHeadMobile = facility.ExecutiveHeadMobile,
                    ExecutiveHeadEmail = facility.ExecutiveHeadEmail,
                    PermissionReceivedDate = facility.PermissionReceivedDate,
                    LastRenewedDate = facility.LastRenewedDate,
                    ApporvingAuthority = facility.ApporvingAuthority,
                    RenewingAuthority = facility.RenewingAuthority,
                    ApprovalValidityTill = facility.ApprovalValidityTill,
                    RenewalValidityTill = facility.RenewalValidityTill,
                    UpgradeDate = facility.UpgradeDate,
                    UpgradingAuthority = facility.UpgradingAuthority,
                    IsLetterOfIntent = facility.IsLetterOfIntent,
                    IsExecutionPermission = facility.IsExecutionPermission,
                    IsRenewal = facility.IsRenewal,
                    IsUpgrade = facility.IsUpgrade,
                    IsServiceExtension = facility.IsServiceExtension,
                    IsBranchExtension = facility.IsBranchExtension,
                    IsRelocation = facility.IsRelocation,
                    Others = facility.Others,
                    ApplicationSubmittedAuthority = facility.ApplicationSubmittedAuthority,
                    ApplicationSubmittedDate = facility.ApplicationSubmittedDate

                };
                return ResultWithDataDto<List<HealthFacilityDto>>.Success([dto]);

            }


            var facilityDto = await _context.HealthFacilities.Include(x => x.FacilityType).Select(facility => new HealthFacilityDto()
                {
                Id = facility.Id,
                FacilityName = facility.FacilityName,
                FacilityType = facility.FacilityType.Name,
                FacilityTypeId = facility.FacilityTypeId,
                PanNumber = facility.PanNumber,
                BedCount = facility.BedCount,
                SpecialistCount = facility.SpecialistCount,
                AvailableServices = facility.AvailableServices,
                District = facility.District,
                LocalLevel = facility.LocalLevel,
                WardNumber = facility.WardNumber,
                Tole = facility.Tole,
                DateOfInspection = facility.DateOfInspection,
                FacilityEmail = facility.FacilityEmail,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                FacilityHeadName = facility.FacilityHeadName,
                FacilityHeadPhone = facility.FacilityHeadPhone,
                FacilityHeadEmail = facility.FacilityHeadEmail,
                ExecutiveHeadName = facility.ExecutiveHeadName,
                ExecutiveHeadMobile = facility.ExecutiveHeadMobile,
                ExecutiveHeadEmail = facility.ExecutiveHeadEmail,
                PermissionReceivedDate = facility.PermissionReceivedDate,
                LastRenewedDate = facility.LastRenewedDate,
                ApporvingAuthority = facility.ApporvingAuthority,
                RenewingAuthority = facility.RenewingAuthority,
                ApprovalValidityTill = facility.ApprovalValidityTill,
                RenewalValidityTill = facility.RenewalValidityTill,
                UpgradeDate = facility.UpgradeDate,
                UpgradingAuthority = facility.UpgradingAuthority,
                IsLetterOfIntent = facility.IsLetterOfIntent,
                IsExecutionPermission = facility.IsExecutionPermission,
                IsRenewal = facility.IsRenewal,
                IsUpgrade = facility.IsUpgrade,
                IsServiceExtension = facility.IsServiceExtension,
                IsBranchExtension = facility.IsBranchExtension,
                IsRelocation = facility.IsRelocation,
                Others = facility.Others,
                ApplicationSubmittedAuthority = facility.ApplicationSubmittedAuthority,
                ApplicationSubmittedDate = facility.ApplicationSubmittedDate
            }).OrderByDescending(x => x.Id)
            .ToListAsync();

            if(facilityDto.Count == 0)
            {
                return new ResultWithDataDto<List<HealthFacilityDto>>(true, facilityDto, "कुनै पनि स्वास्थ्य संस्था फेला परेन।");
            }

            return ResultWithDataDto<List<HealthFacilityDto>>.Success(facilityDto);
        }

        
        public async Task Update(int id, HealthFacilityDto healthFacilityDto)
        {
            throw new NotImplementedException();
        }
    }
}