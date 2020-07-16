using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI_UI.ViewModels
{
    public class EmpleadosVM
    {
        public int Id { get; set; }
      
        public string NombreCompleto { get; set; }
      
        public string RFC { get; set; }
      
        public string Email { get; set; }
        public virtual List<EstructuraVM> EstructurasList { get; set; }
    }
}
