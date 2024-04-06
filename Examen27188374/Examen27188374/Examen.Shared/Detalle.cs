using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Shared
{
    public class ProfesionAfiliado
    {
        [Key]
        public string IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public Double Precio { get; set; }
        public Double Subtotal { get; set; }

        [ForeignKey("IdPedido")]
        public virtual Pedido? IdPeidoNavigation { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Pedido? IdProductoNavigation { get; set; }
        //[ForeignKey("IdProfesion")]
        //public virtual Profesion? IdProfesionNavigation { get; set; }

    }
}
