using Examen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Examen.Contratos
{
    public interface IProductoLogic
    {
        public Task<bool> InsertarProducto(Producto producto);
        public Task<bool> ModificarProducto(Producto producto, int id);
        public Task<bool> EliminarProducto(int id);
        public Task<List<Producto>> ListarProductoTodos();
        public Task<Producto> ObtenerProductoById(int id);
    }
}
