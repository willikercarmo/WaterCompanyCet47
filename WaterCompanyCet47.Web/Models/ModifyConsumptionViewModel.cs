using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Data.Entities;

namespace WaterCompanyCet47.Web.Models
{
    public class ModifyConsumptionViewModel : Consumption
    {
        //User
        [Display(Name = "Contador")]
        //[Range(1, int.MaxValue, ErrorMessage = "Você precisa selecionar 1 consumidor.")] // o valor tem que estar entre o valor 1 até o máximo que quiser
        public string EquipmentId { get; set; }

        // lista de users
        public IEnumerable<SelectListItem> Equipments { get; set; }
    }
}
