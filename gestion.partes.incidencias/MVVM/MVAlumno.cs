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
        private alumno _alumno;

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

        public bool guarda()
        {
            bool correcto = true;

            if (alumnoExiste(_alumno.nia))
            {
                alumnoServicio.edit(_alumno);
            }
            else
            {
                alumnoServicio.add(_alumno);
            }

            try
            {
                alumnoServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                System.Console.WriteLine(dbex.StackTrace);
            }
            return correcto;
        }

        public void elimina(alumno alumno)
        {
            alumnoServicio.delete(alumno);

            try
            {
                alumnoServicio.save();
            }
            catch (DbUpdateException dbex)
            {
                System.Console.WriteLine(dbex.StackTrace);
            }
        }

        public bool alumnoExiste(int nia)
        {
            alumno alumnoGuardado = alumnoServicio.findByID(nia);

            return alumnoGuardado != null;
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
        public alumno alumnoSeleccionado
        {
            get
            {
                return _alumno;
            }
            set
            {
                _alumno = value; OnPropertyChanged("alumnoSeleccionado");
            }
        }
    }
}