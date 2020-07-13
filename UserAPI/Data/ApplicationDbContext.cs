using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
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
        public DbSet<Departamento> TDepartamentos { get; set; }
        public DbSet<Puesto> TPuestos { get; set; }
        public DbSet<Sitios> TSitios { get; set; }
        public DbSet<Estructura> TEstructura { get; set; }


    }
}
