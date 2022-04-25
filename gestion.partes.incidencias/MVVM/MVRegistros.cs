using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        private MotivoRegistroServicio _motivoRegistroServicio;
        private AlumnoServicio _alumnoServicio;
        private ProfesorServicio _profesorServicio;
        private tipo_registro _tipoRegistroSeleccionado;
        private registro _registro;
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
            _motivoRegistroServicio = new MotivoRegistroServicio(ent);
            _alumnoServicio = new AlumnoServicio(ent);
            _profesorServicio = new ProfesorServicio(ent);
            _tipoRegistroSeleccionado = new tipo_registro();
            _fechaDesde = DateTime.Today;
            _fechaHasta = DateTime.Today;
            listaRegistros = new ListCollectionView(_registroServicio.getAll().OrderByDescending(r => r.fecha_suceso).ToList());
        }

        public void setRegistro (registro registro)
        {
            _registro = registro;
        }


        public ListCollectionView listaRegistrosTabla
        {
            get
            {
                return listaRegistros;
            }
        }

        public void recargarListaRegistrosTabla()
        {
            listaRegistros = new ListCollectionView(_registroServicio.getAll().OrderByDescending(r => r.fecha_suceso).ToList());
        }

        public List<tipo_registro> listaTipoRegistros
        {
            get
            {
                return _tipoRegistroServicio.getAll().ToList();
            }
        }
        public List<motivo_registro> listaMotivoRegistro
        {
            get
            {
                return _motivoRegistroServicio.getAll().ToList();
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
        public registro registroSeleccionado
        {
            get
            {
                return _registro;
            }
            set
            {
                _registro = value; OnPropertyChanged("registroSeleccionado");
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

        public bool guarda()
        {
            bool correcto = true;

            if(_registro.id == null)
            {
                _registroServicio.add(_registro);
            }
            else
            {
                _registroServicio.edit(_registro);
            }

            try
            {
                _registroServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                System.Console.WriteLine(dbex.StackTrace);
            }
            return correcto;
        }
        
        public void elimina(registro registro)
        {
            _registroServicio.delete(registro);

            try
            {
                _registroServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                System.Console.WriteLine(dbex.StackTrace);
            }
        }

        public alumno buscarAlumno (int nia)
        {
            return _alumnoServicio.findByID(nia);
        }

        public profesor buscarProfesor (string dni)
        {
            return _profesorServicio.findByID(dni);
        }
    }
}
