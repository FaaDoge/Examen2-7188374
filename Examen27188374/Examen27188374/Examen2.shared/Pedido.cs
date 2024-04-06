using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Shared
{

        public class Pedido
        {
            [Key]
            public int IdPedido { get; set; }
            public int IdCliente { get; set; }
            public DateTime Fecha { get; set; }
            public double Total { get; set; }
            public string? Estado { get; set; }

            [ForeignKey("IdCliente")]
            public virtual Cliente? IdClienteNavigation { get; set; }

            //[InverseProperty("IdPersonaNavigation")]
            //public virtual ICollection<Telefono>? Telefonos { get; set; } = new List<Telefono>();
        }
}
