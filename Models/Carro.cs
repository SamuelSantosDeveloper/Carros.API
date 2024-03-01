using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Carros.Api.Models
{
    public class Carro
    {
       [Key]

       public int Id { get; set; }

       [Required(ErrorMessage = "É necessário informar a marca do carro!")]
       [DisplayName("Marca")]

       public string? Marca { get; set; }

       [Required(ErrorMessage = "É necessário informar a placa do carro!")]
       [DisplayName("Placa")]

       public string? Placa { get; set; }

       [Required(ErrorMessage = "É necessário informar o modelo do carro!")]
       [DisplayName("Modelo")]

       public string? Modelo { get; set; }

       [Required(ErrorMessage = "É necessário informar a cor do carro!")]
       [DisplayName("Cor")]

       public string? Cor { get; set; }

       [Required(ErrorMessage = "É necessário informar o ano do carro!")]
       [DisplayName("Ano")]

       public int? Ano { get; set; }


    }
}