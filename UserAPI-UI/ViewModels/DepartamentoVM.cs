using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI_UI.ViewModels
{
    public class DepartamentoVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int SitiosId { get; set; }
        public IList<PuestoVM> ListPuesto { get; set; }
    }
}
