using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Data.Entities
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Consumidor")]
        public User User { get; set; }

        [Display(Name = "Contador")]
        public Equipment Equipment { get; set; }

        //Data do Registo
        [Display(Name = "Emissão da Fatura")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime InvoiceDate { get; set; }


        public Consumption Consumption { get; set; }

        //public IEnumerable<ConsumptionDetail> Items { get; set; }

        ////Período
        //[Display(Name = "Data de Início")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        //public DateTime? ForBegin { get { return this.Consumption == null ? null : this.Consumption.ForBegin; } }

        ////Período
        //[Display(Name = "Data de Fim")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        //public DateTime? ForEnd { get { return this.Consumption == null ? null : this.Consumption.ForEnd; } }


        ////Consumo em M³
        //[Display(Name = "Consumo em m³")]
        //[DisplayFormat(DataFormatString = "{0:N3}")]
        //public double ConsumptionValue { get { return this.Consumption == null ? 0 : this.Consumption.ConsumptionValue; } }



        [Display(Name = "Total A Pagar")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double? TotalAmount { get; set; }

    }
}
