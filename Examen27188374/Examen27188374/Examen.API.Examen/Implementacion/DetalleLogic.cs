using Examen.API.Examen.Contratos;
using Examen.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Examen.Implementacion
{
    public class DetalleLogic : IDetalleLogic
    {
        private readonly Contexto contexto;

        public DetalleLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarDetalle(int id)
        {
            var eliminar = await contexto.Detalles.FindAsync(id);
            if (eliminar == null)
                return false;

            contexto.Detalles.Remove(eliminar);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarDetalle(Detalle insertar)
        {
            bool sw = false;
            contexto.Detalles.Add(insertar);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Detalle>> ListarDetalleTodos()
        {
            var lista = await contexto.Detalles.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarDetalle(Detalle Detalle, int id)
        {
            var editar = await contexto.Detalles.FindAsync(id);
            if (editar == null)
                return false;

            editar.IdPedido = Detalle.IdPedido;
            editar.IdProducto = Detalle.IdProducto;
            editar.Cantidad = Detalle.Cantidad;
            editar.Precio = Detalle.Precio;
            editar.Subtotal = Detalle.Subtotal;


            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<Detalle> ObtenerDetalleById(int id)
        {
            var objeto = await contexto.Detalles.FindAsync(id);
            return objeto;
        }
    }
}
