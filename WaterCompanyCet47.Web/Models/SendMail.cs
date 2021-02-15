using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterCompanyCet47.Web.Models
{
    public class SendMail
    {
        [Required(ErrorMessage = "'Nome' é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "'Email' é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "'Assunto' é obrigatório")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "'Mensagem' é obrigatória")]
        public string Message { get; set; }

       

    }
}
