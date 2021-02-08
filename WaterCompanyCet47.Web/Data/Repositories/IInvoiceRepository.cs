using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Data.Repositories
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {

        Task<IQueryable<Invoice>> GetInvoiceAsync(string username);

       
    }
}
