using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;
using WaterCompanyCet47.Web.Helpers;

namespace WaterCompanyCet47.Web.Data.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public InvoiceRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<Invoice>> GetInvoiceAsync(string username)
        {

            var user = await _userHelper.GetUserByIdAsync(username);
            if (user == null)
            {
                return null;
            }

            //if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            //{
            //    //Admin
            //    return _context.Invoices
            //        .Include(o => o.User)
            //        .Include(c => c.Equipment)
            //        .Include(c => c.Consumption)
            //        .OrderByDescending(c => c.InvoiceDate);
            //}

            //Costumer
            return _context.Invoices
                .Include(i => i.Equipment)
                .Include(c => c.Consumption)
                .Where(c => c.User == user)
                .OrderByDescending(c => c.InvoiceDate);
        }



        //public async Task<Consumption> GetConsumptionAsync(int id)
        //{
        //    //throw new NotImplementedException();

        //    //var consumption = await _context.Consumptions.FirstOrDefault(x => x.Id == id);

        //    //return _context.Consumptions
        //    //    .Include(o => o.Equipment)
        //    //    .Where(u => u.Id == id);

        //}

        //public async Task<IQueryable<Consumption>> GetConsumptionAsync(int id)
        //{
        //    //throw new NotImplementedException();

        //    //var consumption = await _context.Consumptions
        //    //        .Include(o => o.Equipment)
        //    //        .Where(u => u.Id == id).FirstOrDefaultAsync();

        //    //return consumption.ConsumptionValue
        //}


    }
}
