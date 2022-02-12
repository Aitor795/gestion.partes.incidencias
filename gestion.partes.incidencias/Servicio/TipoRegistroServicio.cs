using gestion.partes.incidencias.Modelo;
using System.Data.Entity;

namespace gestion.partes.incidencias.Servicio
{
    class TipoRegistroServicio : ServicioGenerico<tipo_registro>
    {
        public TipoRegistroServicio(DbContext context) : base(context)
        {
        }
    }
}
