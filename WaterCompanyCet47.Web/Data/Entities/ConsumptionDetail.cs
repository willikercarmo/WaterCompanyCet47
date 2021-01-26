using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Data.Entities
{
    public class ConsumptionDetail : IEntity
    {

        public int Id { get; set; }

        //Contador
        [Display(Name = "Contador")]
        [Required]
        public Equipment Equipment { get; set; }

        //Período
        [Display(Name = "Período Mensal")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ForMonth { get; set; }

        //Consumo em m³
        [Display(Name = "Consumo em M³")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public double ConsumptionValue { get; set; }

    }
}
