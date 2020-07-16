using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Estructura
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Nombre { get; set; }

        [Required]
        public int SitioId { get; set; }
        public virtual Sitios Sitio { get; set; }

        [Required]
        public int DepartamentoId { get; set; }
        public virtual Departamentos Departamento { get; set; }

        [Required]
        public int PuestoId { get; set; }
        public virtual Puestos Puesto { get; set; }


        [Required]
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }
        //public virtual List<EmpleadoEstructura> ListEmpleadoEstructuras { get; set; }

        public bool Active { get; set; }

    }
}
