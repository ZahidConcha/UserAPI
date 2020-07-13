using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Puesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int DepartamentosId { get; set; }
        
    }
}
