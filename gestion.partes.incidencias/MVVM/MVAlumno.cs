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

    public class MVAlumno : MVBase
    {
        private AlumnoServicio alumnoServicio;
        private GrupoServicio grupoServicio;
        private ListCollectionView listaAlumnos;
        private grupo _grupoSeleccionado;
        private string _textFiltroNia;
        private string _textFiltroNombre;
        private string _textFiltroApellido1;
        private string _textFiltroApellido2;

        public MVAlumno(tfgEntities ent)
        {
            alumnoServicio = new AlumnoServicio(ent);
            grupoServicio = new GrupoServicio(ent);
            _grupoSeleccionado = new grupo();
            listaAlumnos = new ListCollectionView(alumnoServicio.getAll().OrderBy(a => a.nia).ToList());
        }

        public ListCollectionView listaAlumnosTabla
        {
            get
            {
                return listaAlumnos;
            }
        }

        public void recargarListaAlumnosTabla()
        {
            listaAlumnos = new ListCollectionView(alumnoServicio.getAll().OrderBy(a => a.nia).ToList());
        }

        public List<grupo> listaGrupos
        {
            get
            {
                return grupoServicio.getAll().ToList();
            }
        }

        public grupo grupoSeleccionado
        {
            get
            {
                return _grupoSeleccionado;
            }
            set
            {
                _grupoSeleccionado = value; OnPropertyChanged("grupoSeleccionado");
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
        public string textFiltroNombre
        {
            get { return _textFiltroNombre; }
            set
            {
                _textFiltroNombre = value;
                OnPropertyChanged("textFiltroNombre");
            }
        }

        public string textFiltroApellido1
        {
            get { return _textFiltroApellido1; }
            set
            {
                _textFiltroApellido1 = value;
                OnPropertyChanged("textFiltroApellido1");
            }
        }
        public string textFiltroApellido2
        {
            get { return _textFiltroApellido2; }
            set
            {
                _textFiltroApellido2 = value;
                OnPropertyChanged("textFiltroApellido2");
            }
        }
    }
}