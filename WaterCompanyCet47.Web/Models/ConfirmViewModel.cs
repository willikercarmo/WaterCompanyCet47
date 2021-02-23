using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Models
{
    public class ConfirmViewModel
    {
        public IEnumerable<Equipment> GetEquipments { get; set; }
        public IEnumerable<User> GetUsers { get; set; }

    }
}
