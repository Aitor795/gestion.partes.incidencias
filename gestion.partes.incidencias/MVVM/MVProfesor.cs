using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Data;

namespace gestion.partes.incidencias.MVVM
{
    public class MVProfesor: MVBase
    {
        private ProfesorServicio profesorServicio;
        private GrupoServicio grupoServicio;
        private profesor _profesor;
        private List<grupo> _listaGrupos;
        private ListCollectionView listaProfesores;
        private grupo _grupoSeleccionado;
        private string _textFiltroDni;
        private string _textFiltroNombre;
        private string _textFiltroApellido1;
        private string _textFiltroApellido2;

        public MVProfesor(tfgEntities ent)
        {
            profesorServicio = new ProfesorServicio(ent);
            _grupoSeleccionado = new grupo();
            grupoServicio = new GrupoServicio(ent);
            listaProfesores = new ListCollectionView(profesorServicio.getAll().OrderBy(p => p.dni).ToList());
            _listaGrupos = grupoServicio.getAll().OrderBy(g => g.codigo).ToList();
        }

        public bool login(string dni, string contrasenya)
        {
            return profesorServicio.login(dni, contrasenya);
        }

        public ListCollectionView listaProfesoresTabla
        {
            get
            {
                return listaProfesores;
            }
        }

        public void recargarListaProfesoresTabla()
        {
            listaProfesores = new ListCollectionView(profesorServicio.getAll().OrderBy(p => p.dni).ToList());
        }

        public List<grupo> listaGrupos
        {
            get
            {
                return _listaGrupos;
            }
            set
            {
                _listaGrupos = value;
                OnPropertyChanged("listaGrupo");
            }
        }

        public void recargarListaGrupos()
        {
            listaGrupos = grupoServicio.getAll().OrderBy(g => g.codigo).ToList();
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

        public bool profesorExiste(string dni)
        {
            profesor profesorGuardado = profesorServicio.findByID(dni);

            return profesorGuardado != null;
        }

        public bool guarda()
        {
            bool correcto = true;

            if (profesorExiste(_profesor.dni))
            {
                profesorServicio.edit(_profesor);
            }
            else
            {
                profesorServicio.add(_profesor);
            }

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

        public string textFiltroDni
        {
            get { return _textFiltroDni; }
            set
            {
                _textFiltroDni = value;
                OnPropertyChanged("textFiltroDni");
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
        public profesor profesorSeleccionado
        {
            get
            {
                return _profesor;
            }
            set
            {
                _profesor = value; OnPropertyChanged("profesorSeleccionado");
            }
        }
    }
}
