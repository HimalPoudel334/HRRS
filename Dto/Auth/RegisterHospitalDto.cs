namespace HRRS.Dto.Auth;

public record RegisterHospitalDto(string Username, string Password, HealthFacilityDto FacilityDto);
