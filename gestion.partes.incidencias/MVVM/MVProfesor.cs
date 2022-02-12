using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System.Data.Entity.Infrastructure;

namespace gestion.partes.incidencias.MVVM
{
    public class MVProfesor: MVBase
    {
        private ProfesorServicio profesorServicio;
        private profesor profesorNuevo;

        public MVProfesor(tfgEntities ent)
        {
            profesorServicio = new ProfesorServicio(ent);
            profesorNuevo = new profesor();
        }

        public bool login(string dni, string contrasenya)
        {
            return profesorServicio.login(dni, contrasenya);
        }

        public bool guardar()
        {
            bool correcto = true;
            profesorServicio.add(profesorNuevo);
            try
            {
                profesorServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                System.Console.WriteLine(dbex.StackTrace);
            }
            return correcto;
        }
    }
}
