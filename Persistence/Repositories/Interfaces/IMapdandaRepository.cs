using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRRS.Persistence.Repositories.Interfaces
{
    public interface IMapdandaRepository
    {
        Task<IEnumerable<Mapdanda>> GetAllAsync();
        Task<Mapdanda?> GetByIdAsync(int id);
        Task AddAsync(Mapdanda mapdanda);
        Task UpdateAsync(Mapdanda mapdanda);
        Task DeleteAsync(int id);
        Task<List<Mapdanda>> GetByAnusuchiId(int anusuchi_id);
    }
}
