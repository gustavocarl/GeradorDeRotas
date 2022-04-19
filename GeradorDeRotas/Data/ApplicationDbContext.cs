using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeradorDeRotas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<Cidade> Cidade { get; set; }

        public DbSet<Equipe> Equipe { get; set; }

        public DbSet<Rotas> Rotas { get; set; }

    }
}
