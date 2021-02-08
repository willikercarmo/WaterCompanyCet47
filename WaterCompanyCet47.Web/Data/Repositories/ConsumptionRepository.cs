using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Helpers;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Data.Repositories
{
    public class ConsumptionRepository : GenericRepository<Consumption>, IConsumptionRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        public ConsumptionRepository(
            DataContext context,
            IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;

        }

        public async Task AddItemToConsumptionAsync(AddConsumptionViewModel model, string username)
        {
            //throw new NotImplementedException();

            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return;
            }

            var equipment = await _context.Equipments.FindAsync(model.EquipmentId);
            if (equipment == null)
            {
                return;
            }

            var consumption = new Consumption
            {
                User = user,
                Equipment = equipment,
                ForBegin = model.ForBegin,
                ForEnd = model.ForEnd,
                ConsumptionValue = model.ConsumptionValue,
                ConsumptionDate = DateTime.UtcNow
            };

            _context.Consumptions.Add(consumption);

            await _context.SaveChangesAsync();

        }


      
        public async Task<IQueryable<Consumption>> GetConsumptionAsync(string username)
        {
            //throw new NotImplementedException();
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                //Admin
                return _context.Consumptions
                    .Include(o => o.User)
                    .Include(c => c.Equipment)
                    .OrderByDescending(c => c.ConsumptionDate);
            }

            //Costumer
            return _context.Consumptions
                .Include(i => i.Equipment)
                .Where(c => c.User == user)
                .OrderByDescending(c => c.ConsumptionDate);

        }

        

       
    }
}
