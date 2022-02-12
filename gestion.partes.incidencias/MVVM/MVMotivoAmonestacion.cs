using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace gestion.partes.incidencias.MVVM
{
    class MVMotivoAmonestacion : MVBase
    {
        private MotivoAmonestacionServicio motivoAmonestacionServicio;
        private motivo_amonestacion _motivoAmonestacionNuevo;

        public MVMotivoAmonestacion(tfgEntities ent)
        {
            motivoAmonestacionServicio = new MotivoAmonestacionServicio(ent);
            motivoAmonestacionNuevo = new motivo_amonestacion();
        }

        public List<motivo_amonestacion> listaMotivoAmonestacion
        {
            get
            {
                return motivoAmonestacionServicio.getAll().ToList();
            }
        }

        public motivo_amonestacion motivoAmonestacionNuevo
        {
            get
            {
                return _motivoAmonestacionNuevo;
            }

            set
            {
                _motivoAmonestacionNuevo = value;
                OnPropertyChanged("motivoAmonestacionNuevo");
            }
        }

        public bool guarda()
        {
            bool correcto = true;
            motivoAmonestacionServicio.add(motivoAmonestacionNuevo);

            try
            {
                motivoAmonestacionServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
            }
            return correcto;
        }

        public bool elimina(motivo_amonestacion entity)
        {
            // FIXME Se debe comprobar primero que el motivo de amonestacion no esté siendo usado en ningún registro

            motivoAmonestacionServicio.delete(entity);

            return true;
        }
    }
}
