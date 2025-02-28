using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using HRRS.Persistence.Entities;

namespace Persistence.Entities
{
    [Index(nameof(PanNumber), IsUnique = true)]
    public class HealthFacility
    {
        public int Id { get; set; }
        public string FacilityName { get; set; }

        public int FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }

        public string PanNumber { get; set; }

        public int BedCount { get; set; }

        public int SpecialistCount { get; set; }

        public string AvailableServices { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int LocalLevelId { get; set; }
        public LocalLevel LocalLevel { get; set; }

        public int WardNumber { get; set; }

        public string Tole { get; set; }

        public string DateOfInspection { get; set; }

        public string? FacilityEmail { get; set; }


        public string? FacilityPhoneNumber { get; set; }

        public string? FacilityHeadName { get; set; }


        public string? FacilityHeadPhone { get; set; }

        public string? FacilityHeadEmail { get; set; }

        public string? ExecutiveHeadName { get; set; }


        public string? ExecutiveHeadMobile { get; set; }

        public string? ExecutiveHeadEmail { get; set; }


        public string? PermissionReceivedDate { get; set; }


        public string? LastRenewedDate { get; set; }

        public string? ApporvingAuthority { get; set; }

        public string? RenewingAuthority { get; set; }


        public string? ApprovalValidityTill { get; set; }


        public string? RenewalValidityTill { get; set; }


        public string? UpgradeDate { get; set; }

        public string? UpgradingAuthority { get; set; }

        public bool? IsLetterOfIntent { get; set; }

        public bool? IsExecutionPermission { get; set; }

        public bool? IsRenewal { get; set; }

        public bool? IsUpgrade { get; set; }

        public bool? IsServiceExtension { get; set; }

        public bool? IsBranchExtension { get; set; }

        public bool? IsRelocation { get; set; }

        public string? Others { get; set; }

        public string? ApplicationSubmittedDate { get; set; }

        public string? ApplicationSubmittedAuthority { get; set; }
    }

    public class FacilityType
    {
        [Key]
        public int SN { get; set; }
        public string HOSP_CODE { get; set; }
        public string HOSP_TYPE { get; set; }

        public bool ACTIVE { get; set; }
    }
}
