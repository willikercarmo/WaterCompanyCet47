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


        [Display(Name = "Contador")]
        public string WaterMetering { get; set; }


        [Display(Name = "Data da Instalação")]
        public DateTime? Installation { get; set; }


        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Display(Name = "Consumidor")]

        public User User { get; set; }

    }
}
