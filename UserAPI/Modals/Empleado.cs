using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Data.Migrations;

namespace UserAPI.Modals
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; }
        [Required]
        [MaxLength(20)]
        public string RFC { get; set; }
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public int EstructuraId { get; set; }
        public virtual List<Estructura> EstructurasList { get; set; }
        //public virtual List<EmpleadoEstructura> ListEmpleadoEstructuras { get; set; }
    }
}


