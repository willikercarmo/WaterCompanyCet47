using Microsoft.AspNetCore.Mvc.Rendering;
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

        //Contador
        [Display(Name = "Contador")]
        public string WaterMetering { get; set; }

        //Data da Instalação
        [Display(Name = "Data da Instalação")]
        public DateTime? Installation { get; set; }

        //Morada
        [Display(Name = "Morada")]
        public string Address { get; set; }

        //Consumidor
        [Display(Name = "Consumidor")]
        [Required]
        public User User { get; set; }
              
    }
}
