using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Sitios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        //public virtual IList<Departamentos> ListDepartamentos { get; set; }
    }
}
