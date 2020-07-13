using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Departamento
    {
        public int  Id { get; set; }
        public string  Nombre { get; set; }
        public string Clave { get; set; }
        public int SitioId { get; set; }
        [ForeignKey("SitioId")]
        public Sitios Sitios { get; set; }
        public  IList<Puesto> ListPuesto { get; set; }


    }
}
