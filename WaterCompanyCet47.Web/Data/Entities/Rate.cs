using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Data.Entities
{
    public class Rate : IEntity
    {
        public int Id { get; set; }

        public int RateScale { get; set; }

        public double UnitPrice { get; set; }
    }
}
