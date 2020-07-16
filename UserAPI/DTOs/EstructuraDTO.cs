using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.DTOs
{
    public class EstructuraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int SitioId { get; set; }
        public int DepartamentoId { get; set; }
        public int PuestoId { get; set; }
        public int EmpleadoId { get; set; }
    }

   public class EstucturaCreateDTO
    {
     
        public string Nombre { get; set; }
        public int SitioId { get; set; }
        public int DepartamentoId { get; set; }
        public int PuestoId { get; set; }
        public int EmpleadoId { get; set; }
        public bool Active { get; set; }

    }
}
