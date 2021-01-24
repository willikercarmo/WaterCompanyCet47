using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Models
{
    public class AddConsumptionViewModel
    {

        [Display(Name = "Contador")]
        [Range(1, int.MaxValue, ErrorMessage = "Você precisa selecionar 1 contador.")] // o valor tem que estar entre o valor 1 até o máximo que quiser
        public string ContadorId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "A quantidade deve ser um número maior que zero.")]
        [Display(Name = "Consumo em M³")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Consumption { get; set; }

        [Display(Name = "Período")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public DateTime ForPeriod { get; set; }

        public IEnumerable<SelectListItem> Equipments { get; set; } // lista com equipamentos

       

    }
}
