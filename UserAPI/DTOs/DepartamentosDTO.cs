using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.DTOs
{
    public class DepartamentosDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
     
        public int SitioId { get; set; }

    }

    public class DepartamentosCreateDTO
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int SitioId { get; set; }

    }
}
