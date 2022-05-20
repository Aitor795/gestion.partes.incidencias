using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion.partes.incidencias.VO
{
    class GestionPermisoVO : MVBase
    {
        private permiso _permiso;
        private bool _profesor;
        private bool _tutor;
        private bool _directivo;
        private bool _administrador;

        public GestionPermisoVO()
        {

        }

        public permiso permiso
        {
            get
            {
                return _permiso;
            }
            set
            {
                _permiso = value; OnPropertyChanged("permiso");
            }
        }

        public bool profesor
        {
            get
            {
                return _profesor;
            }
            set
            {
                _profesor = value; OnPropertyChanged("profesor");
            }
        }

        public bool tutor
        {
            get
            {
                return _tutor;
            }
            set
            {
                _tutor = value; OnPropertyChanged("tutor");
            }
        }

        public bool directivo
        {
            get
            {
                return _directivo;
            }
            set
            {
                _directivo = value; OnPropertyChanged("directivo");
            }
        }

        public bool administrador
        {
            get
            {
                return _administrador;
            }
            set
            {
                _administrador = value; OnPropertyChanged("administrador");
            }
        }
    }
}
