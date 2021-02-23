using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Data.Entities
{
    public class Consumption : IEntity
    {

        public int Id { get; set; }     

        //Consumidor
        [Required]
        public User User { get; set; }

        //Data do Registo
        [Display(Name = "Data do Registo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? ConsumptionDate { get; set; }

        //Contador
        [Display(Name = "Contador")]
        [Required]
        public Equipment Equipment { get; set; }

        //Período
        [Display(Name = "Data de Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ForBegin { get; set; }

        //Período
        [Display(Name = "Data de Fim")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ForEnd { get; set; }


        //Consumo em M³
        [Display(Name = "Consumo em m³")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public double ConsumptionValue { get; set; }

        //Fatura emitida
        [Display(Name = "Fatura emitida")]
        public bool InvoiceIssued { get; set; }




    }
}
