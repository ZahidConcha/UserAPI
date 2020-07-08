using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.DTOs
{
    public class EmpleadosDTO
    {
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
