using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Data.Repositories
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {
        public RateRepository(DataContext context) : base(context)
        {

        }

    }
}
