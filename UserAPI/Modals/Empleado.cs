using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Empleado
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string RFC { get; set; }
        public string Email { get; set; }
    }
}
