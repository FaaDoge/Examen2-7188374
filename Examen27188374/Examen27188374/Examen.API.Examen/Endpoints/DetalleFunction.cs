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
    public class DetalleFunction
    {
        private readonly ILogger<DetalleFunction> _logger;
        private readonly IDetalleLogic DetalleLogic;

        public DetalleFunction(ILogger<DetalleFunction> logger, IDetalleLogic DetalleLogic)
        {
            _logger = logger;
            this.DetalleLogic = DetalleLogic;
        }

        [Function("ListarDetalles")]
        public async Task<HttpResponseData> ListarDetalles([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarDetalles")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para listar Detalles.");
            try
            {
                var listaDetalles = DetalleLogic.ListarDetalleTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaDetalles.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("ListarDetalleId")]
        public async Task<HttpResponseData> ListarDetalleId([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarDetalles/{id:int}")] HttpRequestData req, int id)
        {
            _logger.LogInformation("Ejecutando azure function para listar un Detalle por ID.");
            try
            {
                var DetalleId = DetalleLogic.ObtenerDetalleById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(DetalleId.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarDetalle")]
        public async Task<HttpResponseData> InsertarDetalle([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarDetalle")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para insertar Detalles.");
            try
            {
                var afi = await req.ReadFromJsonAsync<Detalle>() ?? throw new Exception("Debe ingresar un Detalle con todos sus datos");
                bool seGuardo = await DetalleLogic.InsertarDetalle(afi);
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

        [Function("ModificarDetalle")]
        public async Task<HttpResponseData> ModificarDetalle(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarDetalle/{id}")] HttpRequestData req,
        int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar Detalle con Id: {id}.");
            try
            {
                var Detalle = await req.ReadFromJsonAsync<Detalle>() ?? throw new Exception("Debe ingresar un Detalle con todos sus datos");
                bool modificado = await DetalleLogic.ModificarDetalle(Detalle, id);

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

    }
}
