using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAPI.Modals;

namespace UserAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Empleado> TEmpleados { get; set; }
        public DbSet<Departamentos> TDepartamentos { get; set; }
        public DbSet<Puestos> TPuestos { get; set; }
        public DbSet<Estructura> TEstructuras { get; set; }
        public DbSet<Sitios> TSitios { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<EmpleadoEstructura>().HasKey(x => new { x.EstructuraId, x.EmpleadoId });
        //}


    }
}
