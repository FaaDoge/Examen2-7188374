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
    public class ProductoLogic : IProductoLogic
    {
        private readonly Contexto contexto;

        public ProductoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarProducto(int id)
        {
            var eliminar = await contexto.Productos.FindAsync(id);
            if (eliminar == null)
                return false;

            contexto.Productos.Remove(eliminar);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarProducto(Producto insertar)
        {
            bool sw = false;
            contexto.Productos.Add(insertar);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Producto>> ListarProductoTodos()
        {
            var lista = await contexto.Productos.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarProducto(Producto Producto, int id)
        {
            var editar = await contexto.Productos.FindAsync(id);
            if (editar == null)
                return false;

            editar.IdProducto = Producto.IdProducto;
            editar.Nombre = Producto.Nombre;


            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<Producto> ObtenerProductoById(int id)
        {
            var objeto = await contexto.Productos.FindAsync(id);
            return objeto;
        }
    }
}
