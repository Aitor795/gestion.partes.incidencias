using gestion.partes.incidencias.Modelo;
using System.Data.Entity;
namespace gestion.partes.incidencias.Servicio
{
    class GrupoServicio : ServicioGenerico<grupo>
    {
        public GrupoServicio(DbContext context) : base(context)
        {
            // Constructor vacío
        }
    }
}
