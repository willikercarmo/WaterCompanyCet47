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
        public DateTime ConsumptionDate { get; set; }

        //Contador
        [Display(Name = "Contador")]
        [Required]
        public Equipment Equipment { get; set; }

        //public IEnumerable<ConsumptionDetail> Items { get; set; }

        public ConsumptionDetail Items { get; set; }

        //Período
        [Display(Name = "Período Mensal")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ForMonth { get { return this.Items == null ? null : this.Items.ForMonth; } }


        //Consumo em M³
        [Display(Name = "Consumo em m³")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public double ConsumptionValue { get { return this.Items == null ? 0 : this.Items.ConsumptionValue; } }


        //Valor a Pagar
        //TODO: Fazer um AddMigrations
        [Display(Name = "Valor a Pagar")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? Value { get; set; }


    }
}
