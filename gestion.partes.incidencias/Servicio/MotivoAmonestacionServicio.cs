using gestion.partes.incidencias.Modelo;
using System.Data.Entity;

namespace gestion.partes.incidencias.Servicio
{
    class MotivoAmonestacionServicio : ServicioGenerico<motivo_amonestacion>
    {
        public MotivoAmonestacionServicio(DbContext context) : base(context)
        {
            // Constructor vacío
        }
    }
}