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
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return;
            }

            var equipment = await _context.Equipments.FindAsync(model.EquipmentId);
            if (user == null)
            {
                return;
            }

            var consumptionDetailTemp = await _context.ConsumptionDetailTemps
                .Where(c => c.User == user).FirstOrDefaultAsync();

            if (consumptionDetailTemp == null)
            {
                consumptionDetailTemp = new ConsumptionDetailTemp
                {
                    User = user,
                    Equipment = equipment,
                    ForMonth = model.ForPeriod,
                    ConsumptionValue = model.Consumption
                };
            }


            _context.ConsumptionDetailTemps.Add(consumptionDetailTemp);

            await _context.SaveChangesAsync();

        }

        public async Task<bool> ConfirmConsumptionAsync(string userName)
        {
            //throw new NotImplementedException();
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var consumptionTemps = await _context.ConsumptionDetailTemps
                .Include(e => e.Equipment)
                .Where(u => u.User == user)
                .FirstOrDefaultAsync();

           

            if (consumptionTemps == null)
            {
                return false;
            }

            var details = new ConsumptionDetail
            {
                Equipment = consumptionTemps.Equipment,
                ForMonth = consumptionTemps.ForMonth,
                ConsumptionValue = consumptionTemps.ConsumptionValue
            };

            var consumption = new Consumption
            {
                ConsumptionDate = DateTime.UtcNow,
                User = user,
                Equipment = details.Equipment,
                Items = details
            };

            //var consumption = consumptionTemps(o => new Consumption
            //{
            //    ConsumptionDate = DateTime.UtcNow,
            //    User = user,
            //    Equipment = o.Equipment,
            //    ConsumptionValue = o.ConsumptionValue,
            //    ForMonth = o.ForMonth,
            //    Value = 0
            //});

            //_context.Consumptions.Add(consumption);
            _context.Consumptions.Add(consumption);
            _context.ConsumptionDetailTemps.Remove(consumptionTemps);
            await _context.SaveChangesAsync();

            return true;
        }

        //public async Task<bool> ConfirmConsumptionAsync(string userName)
        //{
        //    var user = await _userHelper.GetUserByEmailAsync(userName);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    var consumptionTemps = await _context.ConsumptionDetailTemps
        //        .Include(e => e.Equipment)
        //        .Where(u => u.User == user)
        //        .ToListAsync();

        //    var consumption = new Consumption
        //    {
        //        User = user,
        //        Equipment = 
        //        ConsumptionDate = DateTime.UtcNow,
        //        Items = consumptionTemps
        //    };

        //    return true;
        //}

        //public async Task AddItemToConsumptionAsync(AddConsumptionViewModel model, string username)
        //{
        //    var user = await _userHelper.GetUserByEmailAsync(username);
        //    if (user == null)
        //    {
        //        return;
        //    }

        //    var usercombo = await _context.Users.FindAsync(model.UserId);
        //    if(usercombo == null)
        //    {
        //        return;
        //    }

        //    var consumptionDetailTemp = await _context.ConsumptionDetailTemps
        //        .Where(cdt => cdt.User == usercombo)
        //        .FirstOrDefaultAsync();

        //    if(consumptionDetailTemp == null)
        //    {
        //        consumptionDetailTemp = new ConsumptionDetailTemp
        //        {
        //            User = usercombo
        //        }
        //    }

        //}

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
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Equipment)
                    .OrderByDescending(c => c.ConsumptionDate);
            }

            //Costumer
            return _context.Consumptions
                .Include(c => c.Items)
                .ThenInclude(i => i.Equipment)
                .Where(c => c.User == user)
                .OrderByDescending(c => c.ConsumptionDate);

        }

        public async Task<IQueryable<ConsumptionDetailTemp>> GetConsumptionDetailTempsAsync(string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return null;
            }

            return _context.ConsumptionDetailTemps
                .Include(o => o.Equipment)
                .Where(u => u.User == user);

        }


    }
}
