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

        public class PedidoLogic : IPedidoLogic
        {
            private readonly Contexto contexto;

            public PedidoLogic(Contexto contexto)
            {
                this.contexto = contexto;
            }
            public async Task<bool> EliminarPedido(int id)
            {
                var eliminar = await contexto.Pedidos.FindAsync(id);
                if (eliminar == null)
                    return false;

                contexto.Pedidos.Remove(eliminar);
                await contexto.SaveChangesAsync();
                return true;
            }

            public async Task<bool> InsertarPedido(Pedido insertar)
            {
                bool sw = false;
                contexto.Pedidos.Add(insertar);
                int response = await contexto.SaveChangesAsync();
                if (response == 1)
                {
                    sw = true;
                }
                return sw;
            }

            public async Task<List<Pedido>> ListarPedidoTodos()
            {
                var lista = await contexto.Pedidos.ToListAsync();
                return lista;
            }

            public async Task<bool> ModificarPedido(Pedido Pedido, int id)
            {
                var editar = await contexto.Pedidos.FindAsync(id);
                if (editar == null)
                    return false;

                editar.IdPedido = Pedido.IdPedido;
                editar.IdCliente = Pedido.IdCliente;
                editar.Fecha = Pedido.Fecha;
                editar.Total = Pedido.Total;
                editar.Estado = Pedido.Estado;

                await contexto.SaveChangesAsync();
                return true;
            }

            public async Task<Pedido> ObtenerPedidoById(int id)
            {
                var objeto = await contexto.Pedidos.FindAsync(id);
                return objeto;
            }
        }
    }

