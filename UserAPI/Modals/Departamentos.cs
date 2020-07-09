using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Departamentos
    {
        public int  Id { get; set; }
        public string  Nombre { get; set; }
        public string Clave { get; set; }
        public  IList<Puestos> ListPuesto { get; set; }
        public int SitiosId { get; set; }
        public Sitios Sitios{ get; set; }
    }
}
