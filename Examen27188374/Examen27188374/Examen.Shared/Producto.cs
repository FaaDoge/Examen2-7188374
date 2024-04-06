using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Shared
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        /*[InverseProperty("IdPersonaNavigation")]
        public virtual ICollection<Telefono>? Telefonos { get; set; } = new List<Telefono>();*/
    }
}
