using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Modals
{
    public class Departamentos
    {
        public int  Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string  Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Clave { get; set; }
        //[Required]
        //[MaxLength(100)]
        //public int SitiosId { get; set; }
        //public virtual IList<Puestos> ListPuesto { get; set; }
    }
}
