using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carros.Api.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]

        public string Email {get; set;}

        [Required]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres",
        MinimumLength = 10)]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais, estas senhas não conferem")]

        public string ConfirmPassword { get; set; }
    }
}