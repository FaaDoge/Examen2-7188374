using Examen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Examen.Contratos
{
    public interface IDetalleLogic
    {
        public Task<bool> InsertarDetalle(Detalle detalle);
        public Task<bool> ModificarDetalle(Detalle detalle, int id);
        public Task<bool> EliminarDetalle(int id);
        public Task<List<Detalle>> ListarDetalleTodos();
        public Task<Detalle> ObtenerDetalleById(int id);
    }
}
