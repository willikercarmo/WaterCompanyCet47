using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public ConsumptionRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public Task AddItemToConsumptionAsync(AddConsumptionViewModel model, string username)
        {
            throw new NotImplementedException();
        }

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

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                //Admin
                return _context.ConsumptionDetailTemps
                    .Include(o => o.User)
                    .Include(c => c.Equipment)
                    .OrderByDescending(c => c.ForMonth);
            }

            //Costumer
            return _context.ConsumptionDetailTemps
                .Include(c => c.Equipment)
                .Where(c => c.User == user)
                .OrderByDescending(c => c.ForMonth);

        }
    }
}
