using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Data.Repositories
{
    public interface IConsumptionRepository : IGenericRepository<Consumption>
    {
        Task<IQueryable<Consumption>> GetConsumptionAsync(string username);

        Task AddItemToConsumptionAsync(AddConsumptionViewModel model, string username);

      
    }
}
