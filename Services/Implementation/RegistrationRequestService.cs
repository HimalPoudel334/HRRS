using HRRS.Dto;
using HRRS.Dto.HealthStandard;
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

        public RegistrationRequestService(ApplicationDbContext context) => _context = context;

        
        public async Task<ResultWithDataDto<List<RegistrationRequest?>>> GetAllRegistrationRequestsAsync()
        {
            var requests =  await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .ToListAsync();

            return ResultWithDataDto<List<RegistrationRequest?>>.Success(requests);
        }

        public async Task<ResultWithDataDto<RegistrationRequest?>> GetRegistrationRequestByIdAsync(int id)
        {
            var request = await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .FirstOrDefaultAsync(x => x.Id == id);
            return request == null
                ? ResultWithDataDto<RegistrationRequest?>.Failure("Registration request not found")
                : ResultWithDataDto<RegistrationRequest?>.Success(request);
        }

        public async Task<ResultWithDataDto<string>> ApproveRegistrationRequestAsync(int id, long handledById)
        {
            var request = await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (request == null)
                return ResultWithDataDto<string>.Failure("Registration request not found");

            request.Status = RequestStatus.Approved;
            request.HandledById = handledById;
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
            };
            await _context.HealthFacilities.AddAsync(healthFacility);

            var password = AuthService.GenerateRandomPassword();
            var newUser = new User
            {
                UserName = request.HealthFacility.PanNumber,
                Password = password,
                HealthFacility = healthFacility,
                IsFirstLogin = true
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            //TODO: 
            //create a service that sends mail to the health facility with the username and password

            return ResultWithDataDto<string>.Success($"Registration request approved successfully.\n The username is {newUser.UserName} and password is {password}");
        }

        public async Task<ResultWithDataDto<string>> RejectRegistrationRequestAsync(int id, long handledById, StandardRemarkDto dto)
        {
            var request = await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
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
