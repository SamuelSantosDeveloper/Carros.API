using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carros.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Carros.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}       

        public virtual DbSet<Carro> Carros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Carro>().HasData(
                new Carro
                {
                    Id = 1,
                    Marca = "Volkswagen",
                    Placa = "ABT-6532",
                    Modelo = "Gol",
                    Cor = "Azul",
                    Ano = 2002
                },
                new Carro
                {
                    Id = 2,
                    Marca = "General Motors",
                    Placa = "RFT-8765",
                    Modelo = "Cobalt",
                    Cor = "Prata",
                    Ano = 2006   

                }

            );
        }
    }
}