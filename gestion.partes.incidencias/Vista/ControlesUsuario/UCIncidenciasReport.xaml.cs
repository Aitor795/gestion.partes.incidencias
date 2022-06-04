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
        private List<Predicate<RegistrosReportVO>> criterios = new List<Predicate<RegistrosReportVO>>();
        private Predicate<object> predicadoFiltro;
        private ListCollectionView listaRegistros;
        private ReportDataSource reportDataSource1;

        public UCIncidenciasReport(tfgEntities tfgEnt, profesor profesorLogged)
        {
            InitializeComponent();
            mvRegistros = new MVRegistros(tfgEnt, profesorLogged);
            DataContext = mvRegistros;
            predicadoFiltro = new Predicate<object>(FiltroCombinado);
            mvRegistros.fechaDesde = new DateTime(mvRegistros.fechaDesde.Year, 1, 1);
        }

        //TODO Intentar poner las subentidades en el Report

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listaRegistros = obtenerDatos();
            reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSetRegistros";
            reportDataSource1.Value = listaRegistros.Cast<RegistrosReportVO>().ToList();
            rvRegistro.LocalReport.DataSources.Add(reportDataSource1);
            rvRegistro.LocalReport.ReportPath = "../../Informes/RegistrosReport.rdlc";
            rvRegistro.RefreshReport();
        }

        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                RegistrosReportVO _registro = item as RegistrosReportVO;
                if (criterios.Count() != 0)
                {
                    esta = criterios.TrueForAll(x => x(_registro));
                }
            }
            return esta;
        }

        private void textBoxNiaAlumno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(mvRegistros.textFiltroNia, "[^0-9]"))
            {
                mvRegistros.textFiltroNia = mvRegistros.textFiltroNia.Remove(mvRegistros.textFiltroNia.Length - 1);
            }
        }

        private void BtnFiltro_Click(object sender, RoutedEventArgs e)
        {
            criterios.Clear();
            
            if (comboFiltroTipoRegistros.SelectedItem != null)
                criterios.Add(new Predicate<RegistrosReportVO>(r => r.idTipoRegistro.Equals(mvRegistros.tipoRegistroSeleccionado.id)));
            if (comboFiltroGrupos.SelectedItem != null)
                criterios.Add(new Predicate<RegistrosReportVO>(r => r.codigoGrupoAlumno != null && r.codigoGrupoAlumno.Equals(mvRegistros.grupoSeleccionado.codigo)));
            if (!string.IsNullOrEmpty(mvRegistros.textFiltroDni))
                criterios.Add(new Predicate<RegistrosReportVO>(r => r.dniProfesor.Equals(mvRegistros.textFiltroDni)));
            if (datePickerFechaDesde.SelectedDate != null)
                criterios.Add(new Predicate<RegistrosReportVO>(r => r.fechaSuceso >= mvRegistros.fechaDesde));
            if (datePickerFechaHasta.SelectedDate != null)
                criterios.Add(new Predicate<RegistrosReportVO>(r => r.fechaSuceso < mvRegistros.fechaHasta.AddDays(1)));
            if (!string.IsNullOrEmpty(mvRegistros.textFiltroNia))
                criterios.Add(new Predicate<RegistrosReportVO>(r => r.niaAlumno.Equals(int.Parse(mvRegistros.textFiltroNia))));
            listaRegistros.Filter = predicadoFiltro;
            reportDataSource1.Value = listaRegistros.Cast<RegistrosReportVO>().ToList();
            rvRegistro.RefreshReport();
        }

        private ListCollectionView obtenerDatos()
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

            return new ListCollectionView(listaDatos);
        }

    }
}
