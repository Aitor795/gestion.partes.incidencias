using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gestion.partes.incidencias.MVVM
{
    class MVRegistros : MVBase
    {
        private tfgEntities _tfgEntities;
        private RegistroServicio _registroServicio;
        private profesor _profesorLog;
        private ListCollectionView listaRegistros;

        public MVRegistros (tfgEntities ent, profesor profesorLog)
        {
            _tfgEntities = ent;
            _profesorLog = profesorLog;
            _registroServicio = new RegistroServicio(ent);
            listaRegistros = new ListCollectionView(_registroServicio.getAll().ToList());
        }


        public ListCollectionView listaRegistrosTabla
        {
            get
            {
                return listaRegistros;
            }
        }
    }
}
