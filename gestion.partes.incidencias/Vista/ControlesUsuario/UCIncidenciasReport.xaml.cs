using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using gestion.partes.incidencias.Servicio;
using gestion.partes.incidencias.VO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gestion.partes.incidencias.Vista.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCIncidenciasReport.xaml
    /// </summary>
    public partial class UCIncidenciasReport : UserControl
    {
        private MVRegistros mvRegistros;

        public UCIncidenciasReport(tfgEntities tfgEnt, profesor profesorLogged)
        {
            InitializeComponent();
            mvRegistros = new MVRegistros(tfgEnt, profesorLogged);
            DataContext = mvRegistros;
        }

        //TODO Intentar poner las subentidades en el Report

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var data = obtenerDatos();
            var reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSetRegistros";
            reportDataSource1.Value = data;
            rvRegistro.LocalReport.DataSources.Add(reportDataSource1);
            rvRegistro.LocalReport.ReportPath = "../../Informes/RegistrosReport.rdlc";
            rvRegistro.RefreshReport();
        }

        private List<RegistrosReportVO> obtenerDatos()
        {
            List<RegistrosReportVO> listaDatos = new List<RegistrosReportVO>();

            foreach (registro registro in mvRegistros.listaRegistrosReport)
            {
                RegistrosReportVO registrosReportVO = new RegistrosReportVO();
                registrosReportVO.idTipoRegistro = registro.tipo_registro.id;
                registrosReportVO.descTipoRegistro = registro.tipo_registro.descripcion;
                registrosReportVO.idMotivoRegistro = registro.motivo_registro.id;
                registrosReportVO.motivoRegistro = registro.motivo_registro.motivo;
                registrosReportVO.descMotivoRegistro = registro.motivo_registro.descripcion;
                registrosReportVO.niaAlumno = registro.alumno.nia;
                registrosReportVO.nombreAlumno = registro.alumno.nombre;
                registrosReportVO.apellido1Alumno = registro.alumno.apellido1;
                registrosReportVO.apellido2Alumno = registro.alumno.apellido2;

                if(registro.alumno.grupo != null)
                {
                    registrosReportVO.codigoGrupoAlumno = registro.alumno.grupo.codigo;
                    registrosReportVO.nombreGrupoAlumno = registro.alumno.grupo.nombre;
                }
               
                registrosReportVO.fechaSuceso = registro.fecha_suceso;
                registrosReportVO.dniProfesor = registro.profesor1.dni;
                registrosReportVO.nombreProfesor = registro.profesor1.nombre;
                registrosReportVO.apellido1Profesor = registro.profesor1.apellido1;
                registrosReportVO.apellido2Profesor = registro.profesor1.apellido2;

                if (registro.sancionado != null)
                {
                    if (registro.sancionado == true)
                    {
                        registrosReportVO.sancionado = "Sí";
                    }
                    else
                    {
                        registrosReportVO.sancionado = "No";
                    }
                }

                listaDatos.Add(registrosReportVO);
            }

            return listaDatos;
        }
    }
}
