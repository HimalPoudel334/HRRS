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

        public HealthFacilityService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Create(HealthFacilityDto dto)
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
                Others = dto.Others,
                ApplicationSubmittedAuthority = dto.ApplicationSubmittedAuthority,
                ApplicationSubmittedDate = dto.ApplicationSubmittedDate

            };
            await _context.HealthFacilities.AddAsync(healthFacility);
            await _context.SaveChangesAsync();

            return new ResultDto(true, null);
        }

        public async Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id)
        {
            var healthFacility = await _context.HealthFacilities.FindAsync(id);
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
                Others = healthFacility.Others,
                ApplicationSubmittedAuthority = healthFacility.ApplicationSubmittedAuthority,
                ApplicationSubmittedDate = healthFacility.ApplicationSubmittedDate
            };
            return new ResultWithDataDto<HealthFacilityDto>(true, healthFacilityDto, null);
        }

        public async Task<ResultWithDataDto<List<HealthFacilityDto>>> GetAll()
        {
            var healthFacilityDto = await _context.HealthFacilities.Select(healthFacility => new HealthFacilityDto()
            {
                Id = healthFacility.Id,
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
                Others = healthFacility.Others,
                ApplicationSubmittedAuthority = healthFacility.ApplicationSubmittedAuthority,
                ApplicationSubmittedDate = healthFacility.ApplicationSubmittedDate
            }).OrderByDescending(x => x.Id)
            .ToListAsync();

            if(healthFacilityDto.Count == 0 || healthFacilityDto == null)
            {
                return new ResultWithDataDto<List<HealthFacilityDto>>(false, null, "There are no health facilities available");
            }

            return new ResultWithDataDto<List<HealthFacilityDto>>(true, healthFacilityDto, null);
        }

        Task IHealthFacilityService.Update(int id, HealthFacilityDto healthFacilityDto)
        {
            throw new NotImplementedException();
        }
    }
}