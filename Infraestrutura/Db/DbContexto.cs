using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace MinimalApi.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {
        private readonly IConfiguration _configurationAppSettings;

        public DbContexto(IConfiguration configurationAppSettings)
        {
            _configurationAppSettings = configurationAppSettings;
        }

        public DbSet<Administrador> Administradores { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var stringConfiguration = _configurationAppSettings.GetConnectionString("mysql")?.ToString();
                if (!string.IsNullOrEmpty(stringConfiguration))
                {
                    // Adiciona `charset=utf8mb4` na string de conexão, caso ainda não esteja lá
                    if (!stringConfiguration.Contains("charset=utf8mb4"))
                    {
                        stringConfiguration += ";charset=utf8mb4";
                    }

                    optionsBuilder.UseMySql(
                        stringConfiguration,
                        ServerVersion.AutoDetect(stringConfiguration),
                        mysqlOptions => mysqlOptions.EnableRetryOnFailure() // Opcional: habilita tentativas de reconexão em caso de falhas de conexão
                    );
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>().HasData(
                new Administrador {
                    Id = 1,
                    Email = "administrador@teste.com",
                    Senha = "12345",
                    Perfil = "Adm"
                }
            );
        }

    }
}