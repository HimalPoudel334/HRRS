using HRRS.Dto;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;
using Persistence.Entities;

namespace HRRS.Services.Implementation
{
    public class HealthFacilityService : IHealthFacilityService
    {
        private readonly IHealthFacilityRepositoroy _healthFacilityRepository;
        public HealthFacilityService(IHealthFacilityRepositoroy healthFacilityRepository)
        {
            _healthFacilityRepository = healthFacilityRepository;
        }
        public async Task Create(HealthFacilityDto dto)
        {
            var healthFacility = new HealthFacility
            {
                FacilityName = dto.FacilityName,
                FacilityType = dto.FacilityType,
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
                Others = dto.Others

            };
            await _healthFacilityRepository.Create(healthFacility);

            

        }

        public async Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id)
        {
            var healthFacility = await _healthFacilityRepository.GetById(id);
            if(healthFacility == null)
            {
                return new ResultWithDataDto<HealthFacilityDto>(false, null, "Not found");
            }
            var healthFacilityDto = new HealthFacilityDto
            {
                FacilityName = healthFacility.FacilityName,
                FacilityType = healthFacility.FacilityType,
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
                Others = healthFacility.Others
            };
            return new ResultWithDataDto<HealthFacilityDto>(true, healthFacilityDto, "Success");
        }

        Task IHealthFacilityService.Update(int id, HealthFacilityDto healthFacilityDto)
        {
            throw new NotImplementedException();
        }
    }
}