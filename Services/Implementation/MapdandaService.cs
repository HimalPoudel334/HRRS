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
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNumber))
        {
            return ResultDto.Failure("मापदण्डामा नाम र क्रम सङ्ख्या हुनु पर्छ।");
        }

        var anusuchi = await _dbContext.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("अनुसूची फेला परेन।");
        }

        var exsitingMapdanda = _dbContext.MapdandaTables
            .Where(x => x.AnusuchiId == dto.AnusuchiId);

        if (dto.ParichhedId.HasValue) exsitingMapdanda = _dbContext.MapdandaTables.Where(x => x.ParichhedId == dto.ParichhedId);
        if (dto.SubParichhedId.HasValue) exsitingMapdanda = _dbContext.MapdandaTables.Where(x => x.SubParichhedId == dto.SubParichhedId);

        // Validations 

        var mapdandaTable = exsitingMapdanda.FirstOrDefault();
        if (mapdandaTable == null)
            return ResultDto.Failure("मापदण्ड तालिका फेला परेन।");
        
        var testDanda = mapdandaTable?.Mapdandas.FirstOrDefault();

        if (testDanda != null)
        {
            if (testDanda.FormType != dto.FormType)
                return ResultDto.Failure($"Invalid Form Type for Mapdanda. Mapdanda should have Form Type {testDanda.FormType}");

            if (testDanda.IsAvailableDivided && !dto.IsAvailableDivided)
            {
                var msg = testDanda.IsAvailableDivided ? "मापदण्डामा शय्या सङ्ख्याको गणना हुनु पर्छ।" : "मापदण्डामा शय्या सङ्ख्याको गणना हुनु हुँदैन।";
                return ResultDto.Failure(msg);
            }

            if (!string.IsNullOrEmpty(testDanda.Parimaad) && string.IsNullOrEmpty(dto.Parimaad))
                return ResultDto.Failure("मापदण्डामा परिमाण हुनु पर्छ।");

            if (string.IsNullOrEmpty(testDanda.Parimaad) && !string.IsNullOrEmpty(dto.Parimaad))
                return ResultDto.Failure("मापदण्डामा परिमाण हुनु हुँदैन।");
        }


        var mapdanda = new Mapdanda
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Parimaad = dto.Parimaad,
            IsAvailableDivided = dto.IsAvailableDivided,
            FormType = mapdandaTable!.FormType,
            Is25Active = dto.Is25Active,
            Is50Active = dto.Is50Active,
            Is100Active = dto.Is100Active,
            Is200Active = dto.Is200Active,
            IsCol5Active = dto.IsCol5Active,
            IsCol6Active = dto.IsCol6Active,
            IsCol7Active = dto.IsCol7Active,
            IsCol8Active = dto.IsCol8Active,
            IsCol9Active = dto.IsCol9Active,
            Value25 = dto.Value25,
            Value50 = dto.Value50,
            Value100 = dto.Value100,
            Value200 = dto.Value200,
            Col5 = dto.Col5,
            Col6 = dto.Col6,
            Col7 = dto.Col7,
            Col8 = dto.Col8,
            Col9 = dto.Col9,
            IsGroup = dto.IsGroup,
            IsSubGroup = dto.IsSubGroup,
            IsSection = dto.IsSection,
        };
        //mapdanda.HasGroup = testDanda.SerialNumber == 



        await _dbContext.Mapdandas.AddAsync(mapdanda);
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();


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

    public async Task<ResultWithDataDto<MapdandaTableDto>> GetAdminMapdandas(HospitalStandardQueryParams dto)
    {
        var mapdandaTableQuery = _dbContext.MapdandaTables.AsQueryable();

        if (dto.AnusuchiId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.AnusuchiId == dto.AnusuchiId && x.Parichhed == null);
        if (dto.ParichhedId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.ParichhedId == dto.ParichhedId && x.SubParichhed == null);
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
            }).FirstOrDefaultAsync();

        return ResultWithDataDto<MapdandaTableDto>.Success(mapdandaTables);
    }

    public async Task<ResultWithDataDto<FormType?>> GetFormTypeForMapdanda(HospitalStandardQueryParams dto)
    {
        throw new NotImplementedException();
    } 
}
