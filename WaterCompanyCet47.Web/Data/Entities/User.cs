using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Data.Entities
{
    public class User : IdentityUser
    {
        //Primeiro Nome
        [Display(Name = "Primeiro Nome")]
        public string FirstName { get; set; }

        //Apelido
        [Display(Name = "Apelido")]
        public string LastName { get; set; }

        //Cliente
        [Display(Name = "Nome Completo")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        public List<Equipment> Equipments { get; set; }

    }
}
