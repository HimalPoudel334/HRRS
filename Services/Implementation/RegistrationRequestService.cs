using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation
{
    public class RegistrationRequestService
    {
        private readonly ApplicationDbContext _context;

        public RegistrationRequestService(ApplicationDbContext context) => _context = context;

        //get all registration requests
        public async Task<ResultWithDataDto<List<RegistrationRequest?>>> GetAllRegistrationRequestsAsync()
        {
            var requests =  await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .ToListAsync();

            return ResultWithDataDto<List<RegistrationRequest?>>.Success(requests);
        }

        //get registration request by id
        public async Task<ResultWithDataDto<RegistrationRequest?>> GetRegistrationRequestByIdAsync(int id)
        {
            var request = await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .FirstOrDefaultAsync(x => x.Id == id);
            return request == null
                ? ResultWithDataDto<RegistrationRequest?>.Failure("Registration request not found")
                : ResultWithDataDto<RegistrationRequest?>.Success(request);
        }

        //approve registration request
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

            var healthFacility = request.HealthFacility;
            _context.Entry(healthFacility).State = EntityState.Detached;
            await _context.HealthFacilities.AddAsync(healthFacility);

            var password = AuthService.GenerateRandomPassword();
            var newUser = new User
            {
                UserName = request.HealthFacility.PanNumber,
                Password = AuthService.GenerateHashedPassword(password),
                HealthFacility = healthFacility
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            //TODO: 
            //create a service that sends mail to the health facility with the username and password

            return ResultWithDataDto<string>.Success($"Registration request approved successfully.\n The username is {newUser.UserName} and password is {password}");
        }

        //reject registration request
        public async Task<ResultWithDataDto<string>> RejectRegistrationRequestAsync(int id, long handledById)
        {
            var request = await _context.RegistrationRequests
                .Include(x => x.HealthFacility)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
                return ResultWithDataDto<string>.Failure("Registration request not found");
            request.Status = RequestStatus.Rejected;
            request.HandledById = handledById;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return ResultWithDataDto<string>.Success("Registration request rejected successfully");
        }



    }
}
