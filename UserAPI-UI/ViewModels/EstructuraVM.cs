using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI_UI.ViewModels
{
    public class EstructuraVM
    {
        public int Id { get; set; }
       
        public string Nombre { get; set; }

       
        public int SitioId { get; set; }
        public virtual SitiosVM Sitio { get; set; }

        
        public int DepartamentoId { get; set; }
        public virtual DepartamentoVM Departamento { get; set; }

        
        public int PuestoId { get; set; }
        public virtual PuestoVM Puesto { get; set; }


        
        public int EmpleadoId { get; set; }
        public virtual EmpleadosVM Empleado { get; set; }
        //public virtual List<EmpleadoEstructura> ListEmpleadoEstructuras { get; set; }

        public bool Active { get; set; }
    }
    public class GetEstrucutraDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int SitioId { get; set; }

        public int DepartamentoId { get; set; }

        public int PuestoId { get; set; }
      
        public int EmpleadoId { get; set; }
     
        //public virtual List<EmpleadoEstructura> ListEmpleadoEstructuras { get; set; }

        public bool Active { get; set; }
    }
}
