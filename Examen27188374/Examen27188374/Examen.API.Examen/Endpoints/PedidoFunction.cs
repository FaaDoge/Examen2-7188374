using Examen.API.Examen.Contratos;
using Examen.Shared;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Examen.Endpoints
{
    public class PedidoFunction
    {
        private readonly ILogger<PedidoFunction> _logger;
        private readonly IPedidoLogic PedidoLogic;

        public PedidoFunction(ILogger<PedidoFunction> logger, IPedidoLogic PedidoLogic)
        {
            _logger = logger;
            this.PedidoLogic = PedidoLogic;
        }

        [Function("ListarPedidos")]
        public async Task<HttpResponseData> ListarPedidos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarPedidos")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para listar Pedidos.");
            try
            {
                var listaPedidos = PedidoLogic.ListarPedidoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaPedidos.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarPedidoId")]
        public async Task<HttpResponseData> ListarPedidoId([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarPedidos/{id:int}")] HttpRequestData req, int id)
        {
            _logger.LogInformation("Ejecutando azure function para listar un Pedido por ID.");
            try
            {
                var PedidoId = PedidoLogic.ObtenerPedidoById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(PedidoId.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarPedido")]
        public async Task<HttpResponseData> InsertarPedido([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarPedido")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar Pedidos.");
            try
            {
                var afi = await req.ReadFromJsonAsync<Pedido>() ?? throw new Exception("Debe ingresar un Pedido con todos sus datos");
                bool seGuardo = await PedidoLogic.InsertarPedido(afi);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarPedido")]
        public async Task<HttpResponseData> ModificarPedido(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarPedido/{id}")] HttpRequestData req,
        int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar Pedido con Id: {id}.");
            try
            {
                var Pedido = await req.ReadFromJsonAsync<Pedido>() ?? throw new Exception("Debe ingresar un Pedido con todos sus datos");
                bool modificado = await PedidoLogic.ModificarPedido(Pedido, id);

                if (modificado)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }

                return req.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }


        [Function("EliminarPedido")]
        public async Task<HttpResponseData> EliminarPedido(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarPedido/{id}")] HttpRequestData req,
        int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar Pedido con Id: {id}.");
            try
            {
                bool eliminado = await PedidoLogic.EliminarPedido(id);

                if (eliminado)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }

                return req.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }

    }
}
