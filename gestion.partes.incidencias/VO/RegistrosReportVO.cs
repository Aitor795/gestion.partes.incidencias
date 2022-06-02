using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion.partes.incidencias.VO
{

    class RegistrosReportVO
    {
        public int idTipoRegistro { get; set; }
        public string descTipoRegistro { get; set; }

        public int idMotivoRegistro { get; set; }
        public string motivoRegistro { get; set; }
        public string descMotivoRegistro { get; set; }

        public int niaAlumno { get; set; }
        public string nombreAlumno { get; set; }
        public string apellido1Alumno { get; set; }
        public string apellido2Alumno { get; set; }
        public string codigoGrupoAlumno { get; set; }
        public string nombreGrupoAlumno { get; set; }

        public Nullable<System.DateTime> fechaSuceso { get; set; }

        public string dniProfesor { get; set; }
        public string nombreProfesor { get; set; }
        public string apellido1Profesor { get; set; }
        public string apellido2Profesor { get; set; }

        public string sancionado { get; set; }
    }

}
