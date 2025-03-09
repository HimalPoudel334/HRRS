
using System.Security.Claims;
using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;


namespace HRRS.Services.Implementation
{
    public class HealthFacilityService : IHealthFacilityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileService;
        private readonly IRoleResolver _roleResolver;

        public HealthFacilityService(ApplicationDbContext context, IFileUploadService fileService, IRoleResolver roleResolver)
        {
            _context = context;
            _fileService = fileService;
            _roleResolver = roleResolver;
        }

        public async Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id, long userId)
        {
            var healthFacility = await _roleResolver
                .FacilitiesResolver(userId)
                .Include(x => x.FacilityType)
                .Include(x => x.Province)
                .Include(x => x.BedCount)
                .Include(x => x.District)
                .Include(x => x.LocalLevel)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(healthFacility == null) return ResultWithDataDto<HealthFacilityDto>.Failure("स्वास्थ्य संस्था फेला परेन।");

            var healthFacilityDto = new HealthFacilityDto
            {
                Id = id,
                FacilityName = healthFacility.FacilityName,
                FacilityType = healthFacility.FacilityType.HOSP_TYPE,
                FacilityTypeId = healthFacility.FacilityTypeId,
                PanNumber = healthFacility.PanNumber,
                BedCount = healthFacility.BedCount.Count,
                SpecialistCount = healthFacility.SpecialistCount,
                AvailableServices = healthFacility.AvailableServices,
                District = healthFacility.District.Name,
                DistrictId = healthFacility.DistrictId,
                Province = healthFacility.Province.Name,
                LocalLevel = healthFacility.LocalLevel.Name,
                LocalLevelId = healthFacility.LocalLevelId,
                WardNumber = healthFacility.WardNumber,
                Tole = healthFacility.Tole,
                Latitude = healthFacility.Latitude,
                Longitude = healthFacility.Longitude,
                FilePath = healthFacility.FilePath != null ? _fileService.GetHealthFacilityFilePath(healthFacility.FilePath) : null,
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

        public async Task<ResultWithDataDto<List<HealthFacilityDto>>> GetAll(long userId)
        {
            var healthFacilities = await _roleResolver
                .FacilitiesResolver(userId)
                .Select(facility => new HealthFacilityDto()
            {
                Id = facility.Id,
                FacilityName = facility.FacilityName,
                FacilityType = facility.FacilityType.HOSP_TYPE,
                FacilityTypeId = facility.FacilityTypeId,
                PanNumber = facility.PanNumber,
                BedCount = facility.BedCount.Count,
                SpecialistCount = facility.SpecialistCount,
                AvailableServices = facility.AvailableServices,
                Province = facility.Province.Name,
                District = facility.District.Name,
                DistrictId = facility.DistrictId,
                LocalLevel = facility.LocalLevel.Name,
                LocalLevelId = facility.LocalLevelId,
                WardNumber = facility.WardNumber,
                Tole = facility.Tole,
                Latitude = facility.Latitude,
                Longitude = facility.Longitude,
                FilePath = facility.FilePath != null ? _fileService.GetHealthFacilityFilePath(facility.FilePath) : null,
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
            }).ToListAsync();


            return ResultWithDataDto<List<HealthFacilityDto>>.Success(healthFacilities);
            
        }

        
        public async Task Update(int id, HealthFacilityDto healthFacilityDto)
        {
            throw new NotImplementedException();
        }
    }
}