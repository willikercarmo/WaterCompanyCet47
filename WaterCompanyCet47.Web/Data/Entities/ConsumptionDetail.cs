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

        //Mês
        [Display(Name = "Mês")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = false)]
        public string ForMonth { get; set; }

        //Ano
        [Display(Name = "Ano")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = false)]
        public string ForYear { get; set; }

        //Consumo em m³
        [Display(Name = "Consumo em M³")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public double ConsumptionValue { get; set; }

    }
}
