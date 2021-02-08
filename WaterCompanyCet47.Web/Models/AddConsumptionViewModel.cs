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
        //User
        [Display(Name = "Consumidor")]
        [Range(1, int.MaxValue, ErrorMessage = "Você precisa selecionar 1 consumidor.")] // o valor tem que estar entre o valor 1 até o máximo que quiser
        public int UserId { get; set; }

        //User
        [Display(Name = "Contador")]
        [Range(1, int.MaxValue, ErrorMessage = "Você precisa selecionar 1 contador.")] // o valor tem que estar entre o valor 1 até o máximo que quiser
        public int EquipmentId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "A quantidade deve ser um número maior que zero.")]
        [Display(Name = "Consumo em m³")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public double ConsumptionValue { get; set; }

        //Período
        [Display(Name = "Data de Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ForBegin { get; set; }

        //Período
        [Display(Name = "Data de Fim")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ForEnd { get; set; }

        //Data do Registo
        [Display(Name = "Data do Registo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime ConsumptionDate { get; set; }


        // lista de users
        public IEnumerable<SelectListItem> Users { get; set; }

        // lista de equipamentos
        public IEnumerable<SelectListItem> Equipments { get; set; } 

       

    }
}
