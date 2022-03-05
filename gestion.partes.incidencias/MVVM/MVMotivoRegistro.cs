using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace gestion.partes.incidencias.MVVM
{
    class MVMotivoRegistro : MVBase
    {
        private MotivoRegistroServicio motivoRegistroServicio;
        private motivo_registro _motivoRegistroNuevo;

        public MVMotivoRegistro(tfgEntities ent)
        {
            motivoRegistroServicio = new MotivoRegistroServicio(ent);
            motivoRegistroNuevo = new motivo_registro();
        }

        public List<motivo_registro> listaMotivoRegistro
        {
            get
            {
                return motivoRegistroServicio.getAll().ToList();
            }
        }

        public motivo_registro motivoRegistroNuevo
        {
            get
            {
                return _motivoRegistroNuevo;
            }

            set
            {
                _motivoRegistroNuevo = value;
                OnPropertyChanged("motivoRegistroNuevo");
            }
        }

        public bool guarda()
        {
            bool correcto = true;
            motivoRegistroServicio.add(motivoRegistroNuevo);

            try
            {
                motivoRegistroServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
            }
            return correcto;
        }

        public bool elimina(motivo_registro entity)
        {
            // FIXME Se debe comprobar primero que el motivo de registro no esté siendo usado en ningún registro

            motivoRegistroServicio.delete(entity);

            return true;
        }
    }
}
