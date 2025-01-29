using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

public class MapdandaService : IMapdandaService
{

    
    
    private readonly IMapdandaRepository _mapdandaRepository;
    private readonly IHospitalStandardRespository _hospitalStandardRespository;
    private readonly ApplicationDbContext _dbContext;

    public MapdandaService(IMapdandaRepository repository, IHospitalStandardRespository hospitalStandardRespository, ApplicationDbContext dbContext)
    {
        _mapdandaRepository = repository;
        _hospitalStandardRespository = hospitalStandardRespository;
        _dbContext = dbContext;
    }


}
