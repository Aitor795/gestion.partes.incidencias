using gestion.partes.incidencias.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion.partes.incidencias.Servicio
{
    class PermisosRolServicio : ServicioGenerico<permisos_rol>
    {
        private DbContext contexto;
        public PermisosRolServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        public permisos_rol buscarPermisosRol (string codigoRol, string codigoPermiso)
        {
            permisos_rol permisosRol = null;

            try
            {
                permisosRol = contexto.Set<permisos_rol>().Where(p => p.codigo_rol == codigoRol && p.codigo_permiso == codigoPermiso).FirstOrDefault();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }

            return permisosRol;
        }
    }
}
