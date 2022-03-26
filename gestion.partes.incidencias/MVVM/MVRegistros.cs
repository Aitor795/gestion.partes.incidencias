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
        private TipoRegistroServicio _tipoRegistroServicio;
        private tipo_registro _tipoRegistroSeleccionado;
        private profesor _profesorLog;
        private ListCollectionView listaRegistros;
        private DateTime _fechaDesde;
        private DateTime _fechaHasta;
        private string _textFiltroNia;

        public MVRegistros (tfgEntities ent, profesor profesorLog)
        {
            _tfgEntities = ent;
            _profesorLog = profesorLog;
            _registroServicio = new RegistroServicio(ent);
            _tipoRegistroServicio = new TipoRegistroServicio(ent);
            _tipoRegistroSeleccionado = new tipo_registro();
            _fechaDesde = DateTime.Today;
            _fechaHasta = DateTime.Today;
            listaRegistros = new ListCollectionView(_registroServicio.getAll().OrderByDescending(r => r.fecha_suceso).ToList());
        }


        public ListCollectionView listaRegistrosTabla
        {
            get
            {
                return listaRegistros;
            }
        }

        public List<tipo_registro> listaTipoRegistros
        {
            get
            {
                return _tipoRegistroServicio.getAll().ToList();
            }
        }
        public tipo_registro tipoRegistroSeleccionado
        {
            get
            {
                return _tipoRegistroSeleccionado;
            }
            set
            {
                _tipoRegistroSeleccionado = value; OnPropertyChanged("tipoRegistroSeleccionado");
            }
        }

        public DateTime fechaDesde
        {
            get
            {
                return _fechaDesde;
            }
            set
            {
                _fechaDesde = value; OnPropertyChanged("fechaDesde");
            }
        }

        public DateTime fechaHasta
        {
            get
            {
                return _fechaHasta;
            }
            set
            {
                _fechaHasta = value; OnPropertyChanged("fechaHasta");
            }
        }

        public string textFiltroNia
        {
            get { return _textFiltroNia; }
            set
            {
                _textFiltroNia = value;
                OnPropertyChanged("textFiltroNia");
            }
        }
    }
}
