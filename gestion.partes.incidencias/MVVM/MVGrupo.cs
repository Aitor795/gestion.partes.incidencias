using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace gestion.partes.incidencias.MVVM
{
    class MVGrupo : MVBase
    {
        private GrupoServicio grupoServicio;
        private grupo _grupoNuevo;

        public MVGrupo(tfgEntities ent)
        {
            grupoServicio = new GrupoServicio(ent);
            _grupoNuevo = new grupo();
        }

        public List<grupo> listaGrupo
        {
            get
            {
                return grupoServicio.getAll().ToList();
            }
        }

        public grupo grupoNuevo
        {
            get
            {
                return _grupoNuevo;
            }

            set
            {
                _grupoNuevo = value;
                OnPropertyChanged("grupoNuevo");
            }
        }
        public bool grupoExiste(string codigo)
        {
            grupo grupoGuardado = grupoServicio.findByID(codigo);

            return grupoGuardado != null;
        }

        public bool guarda()
        {
            bool correcto = true;
            grupoServicio.add(grupoNuevo);

            try
            {
                grupoServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
            }
            return correcto;
        }

        public bool elimina(grupo entity)
        {
            // FIXME Se debe comprobar primero que el grupo no contenga ningún alumno ni un profesor sea tutor de este grupo

            grupoServicio.delete(entity);

            return true;
        }
    }
}
