using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Helpers;

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
    }
}
