using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carros.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite o email")]
        [EmailAddress(ErrorMessage ="Email inválido")]

        public string Email {get; set;}

        [Required(ErrorMessage = "Digite a senha")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres",
        MinimumLength = 10)]
        [DataType(DataType.Password)]

        public string Password { get; set; }

    }
}