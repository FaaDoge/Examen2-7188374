using Examen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Examen.Contratos
{
    public interface IClienteLogic
    {
        public Task<bool> InsertarCliente(Cliente cliente);
        public Task<bool> ModificarCliente(Cliente cliente, int id);
        public Task<bool> EliminarCliente(int id);
        public Task<List<Cliente>> ListarClienteTodos();
        public Task<Cliente> ObtenerClienteById(int id);
    }
}
