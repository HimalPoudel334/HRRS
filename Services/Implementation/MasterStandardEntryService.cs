using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
        public async Task<ResultWithDataDto<List<MasterStandardEntryDto>>> GetByHospitalId(int healthFacilityId, long userId)
        {
            var masterEntry = await _context.MasterStandardEntries
                .Where(m => m.HealthFacilityId == healthFacilityId)
                .Where(x => x.EntryStatus != EntryStatus.Draft)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new MasterStandardEntryDto
                {
                    EntryStatus = x.EntryStatus,
                    HealthFacilityId = x.HealthFacilityId,
                    SubmissionCode = x.SubmissionCode,
                    SubmissionType = x.SubmissionType,
                    Decision = x.Status.FirstOrDefault(x => x.CreatedById == userId) != null ? x.Status.First(x => x.CreatedById == userId).Status : null
                })
                .ToListAsync();

            if (masterEntry is null)
            {
                return new ResultWithDataDto<List<MasterStandardEntryDto>>(false, null, "Master standard entry not found");
            }

            return ResultWithDataDto<List<MasterStandardEntryDto>>.Success(masterEntry);
        }

        public async Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByUserHospitalId(int healthFacilityId)
        {
            var masterEntry = await _context.MasterStandardEntries
                .Where(m => m.HealthFacilityId == healthFacilityId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

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
            entry.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return ResultDto.Success();

        }

        public async Task<ResultDto> ApproveStandardsWithRemark(Guid entryId, long userId, StandardRemarkDto dto)
        {
            var entry = await _context.MasterStandardEntries.FindAsync(entryId);

            if (entry == null)
            {
                return ResultDto.Failure("Cannot find entry");
            }

            if (!await ApprovalExist(userId, entryId))
            {
                var status = new SubmissionStatus
                {
                    Status = ApprovalStatus.Approved,
                    CreatedById = userId,
                    Remarks = dto.Remarks,
                };

                if (await _context.Approvals.Where(x => x.EntryId == entryId && x.Status == ApprovalStatus.Approved).CountAsync() >= 2)
                {
                    entry.EntryStatus = EntryStatus.Approved;
                }

                entry.Status.Add(status);
                entry.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }


            return ResultDto.Success();
        }

        public async Task<ResultDto> RejectStandardsWithRemark(Guid entryId, long userId, StandardRemarkDto dto)
        {

            var entry = await _context.MasterStandardEntries.FindAsync(entryId);

            if (entry == null)
            {
                return ResultDto.Failure("Cannot find entry");
            }

            

            if (!await ApprovalExist(userId, entryId))
            {
                var status = new SubmissionStatus
                {
                    Status = ApprovalStatus.Rejected,
                    CreatedById = userId,
                    Remarks = dto.Remarks,
                };
                entry.Status.Add(status);
                entry.EntryStatus = EntryStatus.Rejected;

                entry.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }

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
            entry.UpdatedAt = DateTime.Now;

            var entries = await _context.HospitalStandardEntrys.Where(x => x.MasterStandardEntry == entry).ToListAsync();
            foreach (var item in entries) { item.UpdatedAt = entry.UpdatedAt; }

            
            await _context.SaveChangesAsync();

            return ResultDto.Success();

        }

        public async Task<ResultWithDataDto<MasterStandardEntry>> GetMasterEntryById(Guid submissionCode)
        {
            return ResultWithDataDto<MasterStandardEntry>.Success(await _context.MasterStandardEntries.FindAsync(submissionCode));
        }

        private async Task<bool> ApprovalExist(long userId, Guid entryId)
        {
            return  await _context.Approvals
               .Where(x => x.EntryId == entryId)
               .Where(x => x.CreatedById == userId)
               .AnyAsync();
        }
    }
}
