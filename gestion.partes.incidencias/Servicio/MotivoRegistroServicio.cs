using gestion.partes.incidencias.Modelo;
using System.Data.Entity;

namespace gestion.partes.incidencias.Servicio
{
    class MotivoRegistroServicio : ServicioGenerico<motivo_registro>
    {
        public MotivoRegistroServicio(DbContext context) : base(context)
        {
            // Constructor vacío
        }
    }
}