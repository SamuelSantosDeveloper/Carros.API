using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carros.Api.Models;
using Carros.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CarrosController : ControllerBase
    {
        private ICarroService _carroService;

        public CarrosController(ICarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpGet]

        public async Task<ActionResult<IAsyncEnumerable<Carro>>> GetCarros()
        {
            try
            {
               var carros = await _carroService.GetCarros();
               return Ok(carros);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter a lista de carros");
            }

        }

        [HttpGet("CarroPorMarca")]

        public async Task<ActionResult<IAsyncEnumerable<Carro>>> GetCarrosByMarca([FromQuery] string marca)
        {
            try
            {
               var carros = await _carroService.GetCarrosByMarca(marca);

               if(carros.Count() == 0)
               {
                return NotFound($"Não existem carros com a marca {marca}");
               }

               return Ok(carros);
            }
            catch
            {
                return BadRequest("Request inválido, erro ao buscar os carros");
            }

        }

        [HttpGet("{id:int}", Name="GetCarro")]

        public async Task<ActionResult<Carro>> GetCarro(int id)
        {
            try
            {
               var carro = await _carroService.GetCarro(id);

               if(carro == null)
               {
                return NotFound($"Não existe carro com o id = {id}");
               }

               return Ok(carro);
            }
            catch
            {
                return BadRequest("Request inválido, erro ao buscar o carro");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create(Carro carro)
        {
            try
            {
               await _carroService.CreateCarro(carro);

               return CreatedAtRoute(nameof(GetCarro), new {id = carro.Id}, carro);

            }
            catch
            {
                return BadRequest("Request inválido,erro ao adicionar o carro");
            }
        }

        [HttpPut("{id:int})")]

         public async Task<ActionResult> Update(int id, [FromBody] Carro carro)
        {
            try
            {
                if(carro.Id == id){
                    await _carroService.UpdateCarro(carro);
                    return Ok($"O carro com o id = {id} foi atualizado com sucesso!");
                }
                else{
                    return BadRequest("Os dados estão incorretos!");
                }
            }
            catch
            {
                return BadRequest("Request inválido, erro ao atualizar o carro");
            }
        }

        [HttpDelete("{id:int})")]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
              var carro = await _carroService.GetCarro(id);

              if(carro != null){
               await _carroService.DeleteCarro(carro);
               return Ok($"O carro de id={id} foi excluído com sucesso!");
              }
              else{
                return NotFound($"O carro com id = {id} não foi encontrado");
              }
            }
            catch
            {
                return BadRequest("Request inválido, erro ao deletar o carro");
            }
        }


    }
}