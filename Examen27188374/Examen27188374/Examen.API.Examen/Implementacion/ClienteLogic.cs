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
    public class ClienteLogic : IClienteLogic
    {
        private readonly Contexto contexto;

        public ClienteLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarCliente(int id)
        {
            var eliminar = await contexto.Clientes.FindAsync(id);
            if (eliminar == null)
                return false;

            contexto.Clientes.Remove(eliminar);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarCliente(Cliente insertar)
        {
            bool sw = false;
            contexto.Clientes.Add(insertar);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Cliente>> ListarClienteTodos()
        {
            var lista = await contexto.Clientes.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarCliente(Cliente Cliente, int id)
        {
            var editar = await contexto.Clientes.FindAsync(id);
            if (editar == null)
                return false;

            editar.IdCliente = Cliente.IdCliente;
            editar.Nombre = Cliente.Nombre;
            editar.Apellido = Cliente.Apellido;


            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<Cliente> ObtenerClienteById(int id)
        {
            var objeto = await contexto.Clientes.FindAsync(id);
            return objeto;
        }
    }
}
