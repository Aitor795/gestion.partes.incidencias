using gestion.partes.incidencias.Modelo;
using System;
using System.Data.Entity;
using System.Linq;

namespace gestion.partes.incidencias.Servicio
{
    class ProfesorServicio : ServicioGenerico<profesor>
    {
        private DbContext contexto;

        public profesor profesor { get; set; }

        public ProfesorServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        /*
         * Método que comprueba las credenciales del usuario en la base de datos
         */
        public bool login(string dni, string contrasenya)
        {
            bool correcto = true;
            try
            {
                profesor = contexto.Set<profesor>().Where(p => p.dni == dni).FirstOrDefault();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
            if (profesor == null)
            {
                correcto = false;
            }
            else if (!profesor.dni.Equals(dni) || !profesor.contrasenya.Equals(contrasenya))
            {
                correcto = false;
            }

            return correcto;
        }

        /*
         * Comprueba si en la base de datos existe un usuario con ese login
         * El login de un usuario debe de ser único
         */
        public Boolean profesorUnico(String dni)
        {
            bool unico = true;
            if (contexto.Set<profesor>().Where(p => p.dni == dni).Count() > 0)
            {
                unico = false;
            }
            return unico;
        }

        /*
         * Devuelve un usuario en función del username pasado
         */
        public profesor getProfesorPorDni(String dni)
        {
            profesor profesor;
            profesor = contexto.Set<profesor>().Where(p => p.dni == dni).FirstOrDefault();
            return profesor;
        }
    }
}
