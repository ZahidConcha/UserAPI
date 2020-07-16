using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.DTOs
{
    public class SitiosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        
    }
    public class SitiosCreateDTO
    {
       
        public string Nombre { get; set; }
        public string Clave { get; set; }

    }
}
