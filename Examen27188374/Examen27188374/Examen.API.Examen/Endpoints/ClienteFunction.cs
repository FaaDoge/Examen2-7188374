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
    public class ClienteFunction
    {
        private readonly ILogger<ClienteFunction> _logger;
        private readonly IClienteLogic ClienteLogic;

        public ClienteFunction(ILogger<ClienteFunction> logger, IClienteLogic ClienteLogic)
        {
            _logger = logger;
            this.ClienteLogic = ClienteLogic;
        }

        [Function("EliminarCliente")]
        public async Task<HttpResponseData> EliminarCliente(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarCliente/{id}")] HttpRequestData req,
        int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar Cliente con Id: {id}.");
            try
            {
                bool eliminado = await ClienteLogic.EliminarCliente(id);

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
