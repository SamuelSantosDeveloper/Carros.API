using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carros.Api.Models;

namespace Carros.Api.Services
{
    public interface ICarroService
    {
        Task<IEnumerable<Carro>> GetCarros();

         Task<Carro> GetCarro(int id);

          Task<IEnumerable<Carro>> GetCarrosByMarca(string marca);

          Task CreateCarro(Carro carro);

          Task UpdateCarro(Carro carro);

          Task DeleteCarro(Carro carro);
    }
}