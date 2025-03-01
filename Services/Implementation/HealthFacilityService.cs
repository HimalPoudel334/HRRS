
using System.Security.Claims;
using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;


namespace HRRS.Services.Implementation
{
    public class HealthFacilityService : IHealthFacilityService
    {
        private readonly ApplicationDbContext _context;

        public HealthFacilityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id)
        {
            var healthFacility = await _context.HealthFacilities.Include(x => x.FacilityType).Include(x => x.District).Include(x => x.LocalLevel).FirstOrDefaultAsync(x => x.Id == id);
            if(healthFacility == null)
            {
                return ResultWithDataDto<HealthFacilityDto>.Failure("स्वास्थ्य संस्था फेला परेन।");
            }
            var healthFacilityDto = new HealthFacilityDto
            {
                Id = id,
                FacilityName = healthFacility.FacilityName,
                FacilityType = healthFacility.FacilityType.HOSP_TYPE,
                FacilityTypeId = healthFacility.FacilityTypeId,
                PanNumber = healthFacility.PanNumber,
                BedCount = healthFacility.BedCount,
                SpecialistCount = healthFacility.SpecialistCount,
                AvailableServices = healthFacility.AvailableServices,
                District = healthFacility.District.Name,
                DistrictId = healthFacility.DistrictId,
                LocalLevel = healthFacility.LocalLevel.Name,
                LocalLevelId = healthFacility.LocalLevelId,
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
                ApplicationSubmittedDate = healthFacility.ApplicationSubmittedDate,
                HasNewSubmission = _context.MasterStandardEntries.Any(x => x.HealthFacilityId == healthFacility.Id && x.IsNewEntry)

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
                var facilityList =  await _context.HealthFacilities
                    .Include(x => x.FacilityType)
                    .Include(x => x.District)
                    .Include(x => x.LocalLevel)
                    .Where(x => x.BedCount == user.Role.BedCount)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                    var res = facilityList.Select(facility => new HealthFacilityDto() 
                    {
                        Id = facility.Id,
                        FacilityName = facility.FacilityName,
                        FacilityType = facility.FacilityType.HOSP_CODE,
                        FacilityTypeId = facility.FacilityTypeId,
                        PanNumber = facility.PanNumber,
                        BedCount = facility.BedCount,
                        SpecialistCount = facility.SpecialistCount,
                        AvailableServices = facility.AvailableServices,
                        District = facility.District.Name,
                        DistrictId = facility.DistrictId,
                        LocalLevel = facility.LocalLevel.Name,
                        LocalLevelId = facility.LocalLevelId,
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
                        ApplicationSubmittedDate = facility.ApplicationSubmittedDate,
                        HasNewSubmission = _context.MasterStandardEntries.Any(x => x.HealthFacilityId == facility.Id && x.IsNewEntry)
                    }).ToList();


                return ResultWithDataDto<List<HealthFacilityDto>>.Success(res);
            }

            if (role == "Hospital")
            {
                var facility = await _context.HealthFacilities.Include(x => x.District).Include(x => x.LocalLevel).Include(x => x.FacilityType).Where(x=> x.Id == user.HealthFacilityId).SingleOrDefaultAsync();
                if (facility == null) {
                    return new ResultWithDataDto<List<HealthFacilityDto>>(true, null, "स्वास्थ्य संस्था फेला परेन।");
                }
                var dto = new HealthFacilityDto()
                {
                    Id = facility.Id,
                    FacilityName = facility.FacilityName,
                    FacilityType = facility.FacilityType.HOSP_CODE,
                    FacilityTypeId = facility.FacilityTypeId,
                    PanNumber = facility.PanNumber,
                    BedCount = facility.BedCount,
                    SpecialistCount = facility.SpecialistCount,
                    AvailableServices = facility.AvailableServices,
                    District = facility.District.Name,
                    DistrictId = facility.DistrictId,
                    LocalLevel = facility.LocalLevel.Name,
                    LocalLevelId = facility.LocalLevelId,
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
                    ApplicationSubmittedDate = facility.ApplicationSubmittedDate,
                    HasNewSubmission = _context.MasterStandardEntries.Any(x => x.HealthFacilityId == facility.Id && x.IsNewEntry)
                };
                return ResultWithDataDto<List<HealthFacilityDto>>.Success([dto]);

            }


            var list = await _context.HealthFacilities
                .Include(x => x.District)
                .Include(x => x.LocalLevel)
                .Include(x => x.FacilityType)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            
            var facilityDtos = list.Select(facility => new HealthFacilityDto()
            {
                Id = facility.Id,
                FacilityName = facility.FacilityName,
                FacilityType = facility.FacilityType.HOSP_CODE,
                FacilityTypeId = facility.FacilityTypeId,
                PanNumber = facility.PanNumber,
                BedCount = facility.BedCount,
                SpecialistCount = facility.SpecialistCount,
                AvailableServices = facility.AvailableServices,
                District = facility.District.Name,
                DistrictId = facility.DistrictId,
                LocalLevel = facility.LocalLevel.Name,
                LocalLevelId = facility.LocalLevelId,
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
                ApplicationSubmittedDate = facility.ApplicationSubmittedDate,
                HasNewSubmission = _context.MasterStandardEntries.Any(x => x.HealthFacilityId == facility.Id && x.IsNewEntry)
            }).ToList();

            if(facilityDtos.Count == 0)
            {
                return new ResultWithDataDto<List<HealthFacilityDto>>(true, facilityDtos, "कुनै पनि स्वास्थ्य संस्था फेला परेन।");
            }

            return ResultWithDataDto<List<HealthFacilityDto>>.Success(facilityDtos);
        }

        
        public async Task Update(int id, HealthFacilityDto healthFacilityDto)
        {
            throw new NotImplementedException();
        }
    }
}