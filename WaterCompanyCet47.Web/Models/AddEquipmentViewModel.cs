using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Models
{
    public class AddEquipmentViewModel
    {

        //Contador
        [Display(Name = "Contador")]
        public string WaterMetering { get; set; }

        //Data da Instalação
        [Display(Name = "Data da Instalação")]
        public DateTime? Installation { get; set; }

        //Morada
        [Display(Name = "Morada")]
        public string Address { get; set; }


        //User
        [Display(Name = "Consumidor")]
        //[Range(1, int.MaxValue, ErrorMessage = "Você precisa selecionar 1 consumidor.")] // o valor tem que estar entre o valor 1 até o máximo que quiser
        public string UserId { get; set; }

        // lista de users
        public IEnumerable<SelectListItem> Users { get; set; }

      


    }
}
