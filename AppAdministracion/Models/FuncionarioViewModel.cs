using Identity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Models
{
    public class FuncionarioViewModel : Usuario
    {
        [Required]
        public string Rol { get; set; }

        public FuncionarioViewModel()
        {

        }

        public FuncionarioViewModel(Usuario funcionario)
        {
            this.Documento = funcionario.Documento;
            this.UserName = funcionario.UserName;
            this.Email = funcionario.Email;
            this.PhoneNumber = funcionario.PhoneNumber;
            this.Id = funcionario.Id;
        }
    }
}
