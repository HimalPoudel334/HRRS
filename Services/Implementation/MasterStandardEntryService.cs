using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation
{
    public class MasterStandardEntryService : IMasterStandardEntryService
    {
        private readonly ApplicationDbContext _context;

        public MasterStandardEntryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultWithDataDto<string>> Create(SubmissionTypeDto dto, int healthFacilityId)
        {
            var healthFacility = await _context.HealthFacilities.FindAsync(healthFacilityId);
            if (healthFacility is null)
            {
                return new ResultWithDataDto<string>(false, null, "Health facility not found");
            }

            var masterEntry = new MasterStandardEntry
            {
                SubmissionCode = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                EntryStatus = EntryStatus.Draft,
                HealthFacility = healthFacility,
                SubmissionType = dto.Type,
                BedCount = healthFacility.BedCount,
            };

            await _context.MasterStandardEntries.AddAsync(masterEntry);
            await _context.SaveChangesAsync();

            return ResultWithDataDto<string>.Success(masterEntry.SubmissionCode.ToString());
        }
        public async Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByHospitalId(int healthFacilityId)
        {
            var masterEntry = await _context.MasterStandardEntries
                .Where(m => m.HealthFacilityId == healthFacilityId).Where(x => x.EntryStatus != EntryStatus.Draft).OrderByDescending(x => x.CreatedAt).ToListAsync();

            if (masterEntry is null)
            {
                return new ResultWithDataDto<List<MasterStandardEntry>>(false, null, "Master standard entry not found");
            }

            return ResultWithDataDto<List<MasterStandardEntry>>.Success(masterEntry);
        }

        public async Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByUserHospitalId(int healthFacilityId)
        {
            var masterEntry = await _context.MasterStandardEntries
                .Where(m => m.HealthFacilityId == healthFacilityId).OrderByDescending(x => x.CreatedAt).ToListAsync();

            if (masterEntry is null)
            {
                return new ResultWithDataDto<List<MasterStandardEntry>>(false, null, "Master standard entry not found");
            }

            return ResultWithDataDto<List<MasterStandardEntry>>.Success(masterEntry);
        }

        public async Task<ResultDto> UserFinalSubmission(Guid submissionCode)
        {
            var entry = await _context.MasterStandardEntries.FindAsync(submissionCode);
            if (entry is null)
            {
                return ResultDto.Failure("Cannot find submission entry");
            }

            entry.EntryStatus = EntryStatus.Pending;
            await _context.SaveChangesAsync();
            return ResultDto.Success();

        }

        public async Task<ResultDto> ApproveStandardsWithRemark(Guid entryId, StandardRemarkDto dto)
        {
            var entry = await _context.MasterStandardEntries.FindAsync(entryId);

            if (entry == null)
            {
                return ResultDto.Failure("Cannot find entry");
            }

            entry.EntryStatus = EntryStatus.Approved;
            entry.Remarks = dto.Remarks;
            entry.UpdatedAt = DateTime.Now;

            var stds = await _context.HospitalStandardEntrys.Where(x => x.MasterStandardEntry == entry).ToListAsync();
            foreach (var std in stds)
            {
                std.Status = entry.EntryStatus;
            }

            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }

        public async Task<ResultDto> RejectStandardsWithRemark(Guid entryId, StandardRemarkDto dto)
        {

            var entry = await _context.MasterStandardEntries.FindAsync(entryId);

            if (entry == null)
            {
                return ResultDto.Failure("Cannot find entry");
            }

            entry.EntryStatus = EntryStatus.Rejected;
            entry.Remarks = dto.Remarks;
            entry.UpdatedAt = DateTime.Now;

            var stds = await _context.HospitalStandardEntrys.Where(x => x.MasterStandardEntry == entry).ToListAsync();
            foreach (var std in stds)
            {
                std.Status = entry.EntryStatus;
            }

            await _context.SaveChangesAsync();

            return ResultDto.Success();

        }


        public async Task<ResultDto> PendingHospitalStandardsEntry(Guid entryId, long facilityId)
        {

            var entry = await _context.MasterStandardEntries.FindAsync(entryId);

            if (entry == null)
            {
                return ResultDto.Failure("Cannot find entry");
            }
            var healthFacility = await _context.HealthFacilities.FirstOrDefaultAsync(x => x.Id == facilityId);
            if (healthFacility is null)
            {
                return ResultDto.Failure("You are not allowed to preform this action");
            }
            var isAlreadyPending = await _context.MasterStandardEntries.Where(x => x.HealthFacilityId == healthFacility!.Id).AnyAsync(x => x.EntryStatus == EntryStatus.Pending);
            if (isAlreadyPending)
            {
                return ResultDto.Failure("You have alread a pending submission");
            }


            entry.EntryStatus = EntryStatus.Pending;

            var entries = await _context.HospitalStandardEntrys.Where(x => x.MasterStandardEntry == entry).ToListAsync();
            foreach (var item in entries) { item.Status = entry.EntryStatus; }

            
            await _context.SaveChangesAsync();

            return ResultDto.Success();

        }

    }
}
