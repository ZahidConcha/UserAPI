using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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
        public DbSet<Departamentos> TEstructuras { get; set; }
        public DbSet<Sitios> TSitios { get; set; }
    }
}
