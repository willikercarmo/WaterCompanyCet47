using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Data.Entities
{
    public class Equipment : IEntity
    {
        public int Id { get; set; } // Já assume que é a Chave Primária

        
        [Display(Name = "Water Metering Number")]
        public string WaterMetering { get; set; }
        
        public DateTime? Installation { get; set; }
       
        public string Address { get; set; }

    }
}
