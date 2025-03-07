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
                return new ResultWithDataDto<string>(false, null, "Health facility not found");

            var submissionType = await _context.SubmissionTypes.FindAsync(dto.Type);
            if (submissionType is null) return new ResultWithDataDto<string>(false, null, "Submission type not found");

            var masterEntry = new MasterStandardEntry
            {
                SubmissionCode = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                EntryStatus = EntryStatus.Draft,
                HealthFacility = healthFacility,
                SubmissionType = submissionType,
                BedCount = healthFacility.BedCount,
                IsNewEntry = false
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
                return new ResultWithDataDto<List<MasterStandardEntry>>(false, null, "Master standard entry not found");

            return ResultWithDataDto<List<MasterStandardEntry>>.Success(masterEntry);
        }

        public async Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByUserHospitalId(int healthFacilityId)
        {
            var masterEntry = await _context.MasterStandardEntries
                .Where(m => m.HealthFacilityId == healthFacilityId).OrderByDescending(x => x.CreatedAt).ToListAsync();

            if (masterEntry is null)
                return new ResultWithDataDto<List<MasterStandardEntry>>(false, null, "Master standard entry not found");

            return ResultWithDataDto<List<MasterStandardEntry>>.Success(masterEntry);
        }

        public async Task<ResultDto> UserFinalSubmission(Guid submissionCode)
        {
            var entry = await _context.MasterStandardEntries.FindAsync(submissionCode);
            if (entry is null)
                return ResultDto.Failure("Cannot find submission entry");

            entry.EntryStatus = EntryStatus.Pending;
            entry.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return ResultDto.Success();

        }

        public async Task<ResultDto> ApproveStandardsWithRemark(Guid entryId, StandardRemarkDto dto, long userId)
        {
            var entry = await _context.MasterStandardEntries.FindAsync(entryId);
            if (entry == null)
                return ResultDto.Failure("Cannot find entry");
            
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId);
            if(user!.Role is null)
                return ResultDto.Failure("Cannot find entry");

            if(user!.Role.BedCount.HasValue && user!.Role.BedCount != entry.BedCount)
                return ResultDto.Failure("Cannot find entry");

            entry.EntryStatus = EntryStatus.Approved;
            entry.Remarks = dto.Remarks;
            entry.UpdatedAt = DateTime.Now;
            entry.ApprovedBy = user;
            entry.IsNewEntry = false;

            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }

        public async Task<ResultDto> RejectStandardsWithRemark(Guid entryId, StandardRemarkDto dto, long userId)
        {

            var entry = await _context.MasterStandardEntries.FindAsync(entryId);
            if (entry == null)
                return ResultDto.Failure("Cannot find entry");

            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId);
            if (user!.Role is null)
                return ResultDto.Failure("Cannot find entry");

            if (user!.Role.BedCount.HasValue && user!.Role.BedCount != entry.BedCount)
                return ResultDto.Failure("Cannot find entry");


            entry.EntryStatus = EntryStatus.Rejected;
            entry.Remarks = dto.Remarks;
            entry.UpdatedAt = DateTime.Now;
            entry.RejectedBy = user;
            entry.IsNewEntry = false;

            await _context.SaveChangesAsync();

            return ResultDto.Success();

        }


        public async Task<ResultDto> PendingHospitalStandardsEntry(Guid entryId, long facilityId)
        {

            var entry = await _context.MasterStandardEntries.FindAsync(entryId);
            if (entry == null)
                return ResultDto.Failure("Cannot find entry");
            
            var healthFacility = await _context.HealthFacilities.FirstOrDefaultAsync(x => x.Id == facilityId);
            if (healthFacility is null)
                return ResultDto.Failure("You are not allowed to preform this action");
            
            var isAlreadyPending = await _context.MasterStandardEntries.Where(x => x.HealthFacilityId == healthFacility!.Id).AnyAsync(x => x.EntryStatus == EntryStatus.Pending);
            if (isAlreadyPending)
                return ResultDto.Failure("You have alread a pending submission");

            entry.EntryStatus = EntryStatus.Pending;
            entry.UpdatedAt = DateTime.Now;
            entry.IsNewEntry = true;

            var entries = await _context.HospitalStandardEntrys.Where(x => x.MasterStandardEntry == entry).ToListAsync();

            await _context.SaveChangesAsync();

            return ResultDto.Success();

        }

        public async Task<ResultWithDataDto<MasterStandardEntry>> GetMasterEntryById(Guid submissionCode)
        {
            return ResultWithDataDto<MasterStandardEntry>.Success(await _context.MasterStandardEntries.FindAsync(submissionCode));
        }

        public async Task<ResultWithDataDto<List<SubmissionType>>> GetAllSubmissionTypes()
        {
            var submissionTypes = await _context.SubmissionTypes.ToListAsync();
            return ResultWithDataDto<List<SubmissionType>>.Success(submissionTypes);
        }

        public async Task<ResultWithDataDto<List<MasterStandardEntryDto>>> GetAllNewSubmission(long userId)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null || user.UserType == "Hospital") return ResultWithDataDto<List<MasterStandardEntryDto>>.Failure("User not found");

            var masterEntryQuery = _context.MasterStandardEntries
                .Where(x => x.IsNewEntry);

            if (user.Role != null && user.Role.Title != Role.SuperAdmin)
                masterEntryQuery = masterEntryQuery.Where(x => x.HealthFacility.BedCount == user.Role.BedCount);

            var masterEntries = await masterEntryQuery
                .Select(x => new MasterStandardEntryDto
                {
                    HealthFacility = x.HealthFacility.FacilityName,
                    HealthFacilityId = x.HealthFacilityId,
                    EntryStatus = x.EntryStatus,
                    Remarks = x.Remarks,
                    SubmissionType = x.SubmissionType.Title,
                    SubmissionCode = x.SubmissionCode,
                    SubmittedOn = x.UpdatedAt
                }).ToListAsync();

            return ResultWithDataDto<List<MasterStandardEntryDto>>.Success(masterEntries);
        }

        public async Task<ResultWithDataDto<int>> GetNewSubmissionCount(long userId)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null || user.UserType == "Hospital") return ResultWithDataDto<int>.Failure("User not found");

            var masterEntryQuery = _context.MasterStandardEntries
                .Where(x => x.IsNewEntry);

            if (user.Role != null && user.Role.Title != Role.SuperAdmin)
                masterEntryQuery = masterEntryQuery.Where(x => x.HealthFacility.BedCount == user.Role.BedCount);

            var count = await masterEntryQuery.CountAsync();
            return ResultWithDataDto<int>.Success(count);
        }



    }
}