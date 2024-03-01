using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carros.Api.Data;
using Carros.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Carros.Api.Services
{
    public class CarrosService : ICarroService
    {
        private readonly ApplicationDbContext _context;

        public CarrosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Carro>> GetCarros()
        {
            try
            {
               return await _context.Carros.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Carro>> GetCarrosByMarca(string marca)
        {
            IEnumerable<Carro> carros;
            if(!string.IsNullOrWhiteSpace(marca))
            {
                carros = await _context.Carros.Where(x => x.Marca.Contains(marca)).ToListAsync();
            }
            else
            {
                carros = await GetCarros();
            }

            return carros;
        }

        public async Task<Carro> GetCarro(int id)
        {
           var carro = await _context.Carros.FindAsync(id);
           return carro;
        }
        public async Task CreateCarro(Carro carro)
        {
            _context.Carros.Add(carro);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCarro(Carro carro)
        {
           _context.Entry(carro).State = EntityState.Modified;
           await _context.SaveChangesAsync();
        }
        public async Task DeleteCarro(Carro carro)
        {
            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();
        }
    }
}