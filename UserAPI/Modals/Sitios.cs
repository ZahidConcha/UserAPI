using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public IList<Departamento> ListDepartamentos { get; set; }
        
    }
}
