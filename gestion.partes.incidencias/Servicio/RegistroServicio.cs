using gestion.partes.incidencias.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion.partes.incidencias.Servicio
{
    class RegistroServicio : ServicioGenerico<registro>
    {
        private DbContext contexto;

        public RegistroServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        
    }
}
