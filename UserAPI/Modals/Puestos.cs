using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Puestos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int DepartamentosId { get; set; }
        public  Departamentos Departamentos { get; set; }
    }
}
