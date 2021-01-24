using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Data.Repositories
{
    public interface IConsumptionRepository : IGenericRepository<Consumption>
    {
        Task<IQueryable<Consumption>> GetConsumptionAsync(string username);
    }
}
