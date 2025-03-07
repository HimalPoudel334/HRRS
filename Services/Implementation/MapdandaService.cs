using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

public class MapdandaService : IMapdandaService
{
    private readonly ApplicationDbContext _dbContext;

    public MapdandaService( ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultDto> Add(MapdandaDto dto)
    {
        throw new NotImplementedException();

    }

    public async Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultDto> ToggleStatus(int mapdandaId)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(mapdandaId);
        if (mapdanda is null)
        {
            return ResultDto.Failure("मापदण्ड फेला परेन।");
        }

        mapdanda.Status = !mapdanda.Status;
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<string>>> GetMapdandaGroups(string? searchKey)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultWithDataDto<List<MapdandaTableDto>>> GetAdminMapdandas(HospitalStandardQueryParams dto)
    {
        var mapdandaTableQuery = _dbContext.MapdandaTables.AsQueryable();

        if (dto.AnusuchiId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.AnusuchiId == dto.AnusuchiId);
        if (dto.ParichhedId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.ParichhedId == dto.ParichhedId);
        if (dto.SubParichhedId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.SubParichhedId == dto.SubParichhedId);

        var mapdandaTables = await mapdandaTableQuery
            .Select(x => new MapdandaTableDto
            {
                Id = x.Id,
                TableName = x.TableName,
                AnusuchiId = x.AnusuchiId,
                ParichhedId = x.ParichhedId,
                SubParichhedId = x.SubParichhedId,
                Description = x.Description,
                Note = x.Note,
                FormType = x.FormType,
                Mapdandas = x.Mapdandas.Select(y => new MapdandaDto
                {
                    Id = y.Id,
                    SerialNumber = y.SerialNumber,
                    Name = y.Name,
                    OrderNo = y.OrderNo,
                    Parimaad = y.Parimaad,
                    FormType = y.FormType,
                    IsAvailableDivided = y.IsAvailableDivided,
                    Is25Active = y.Is25Active,
                    Is50Active = y.Is50Active,
                    Is100Active = y.Is100Active,
                    Is200Active = y.Is200Active,
                    IsCol5Active = y.IsCol5Active,
                    IsCol6Active = y.IsCol6Active,
                    IsCol7Active = y.IsCol7Active,
                    IsCol8Active = y.IsCol8Active,
                    IsCol9Active = y.IsCol9Active,
                    Value25 = y.Value25,
                    Value50 = y.Value50,
                    Value100 = y.Value100,
                    Value200 = y.Value200,
                    Col5 = y.Col5,
                    Col6 = y.Col6,
                    Col7 = y.Col7,
                    Col8 = y.Col8,
                    Col9 = y.Col9,
                    Status = y.Status,
                    IsGroup = y.IsGroup,
                    IsSubGroup = y.IsSubGroup,
                    IsSection = y.IsSection,
                    HasGroup = y.HasGroup,
                }).ToList()
            }).ToListAsync();

        return ResultWithDataDto<List<MapdandaTableDto>>.Success(mapdandaTables);
    }

    public async Task<ResultWithDataDto<FormType?>> GetFormTypeForMapdanda(HospitalStandardQueryParams dto)
    {
        throw new NotImplementedException();
    } 
}
