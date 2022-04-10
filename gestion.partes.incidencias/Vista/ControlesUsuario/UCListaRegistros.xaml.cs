using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using gestion.partes.incidencias.Vista.Dialogos;
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
    /// Lógica de interacción para UCListaRegistros.xaml
    /// </summary>
    public partial class UCListaRegistros : UserControl
    {
        private tfgEntities _tfgEnt;
        private profesor _profesorLogged;
        private MVRegistros mvRegistros;
        private List<Predicate<registro>> criterios = new List<Predicate<registro>>();
        private Predicate<object> predicadoFiltro;

        public UCListaRegistros(tfgEntities ent, profesor profesorLogged)
        {
            InitializeComponent();
            _tfgEnt = ent;
            _profesorLogged = profesorLogged;
            mvRegistros = new MVRegistros(_tfgEnt, _profesorLogged);
            DataContext = mvRegistros;
            predicadoFiltro = new Predicate<object>(FiltroCombinado);
        }
        
        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                registro _registro = item as registro;
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
                criterios.Add(new Predicate<registro>(r => r.tipo_registro.id.Equals(mvRegistros.tipoRegistroSeleccionado.id)));
            if (datePickerFechaDesde.SelectedDate != null)
                criterios.Add(new Predicate<registro>(r => r.fecha_suceso >= mvRegistros.fechaDesde));
            if (datePickerFechaHasta.SelectedDate != null)
                criterios.Add(new Predicate<registro>(r => r.fecha_suceso < mvRegistros.fechaHasta.AddDays(1)));
            if (!string.IsNullOrEmpty(mvRegistros.textFiltroNia))
                criterios.Add(new Predicate<registro>(r => (r.nia_alumno != null) && r.nia_alumno.Equals(int.Parse(mvRegistros.textFiltroNia))));

            dgArticulos.Items.Filter = predicadoFiltro;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BtnFiltro_Click(sender, e);
        }

        private void btnAnyadirEditarRegistro_Click(object sender, RoutedEventArgs e)
        {
            DialogAddRegistro dialog = new DialogAddRegistro(_tfgEnt, _profesorLogged, new registro());
            dialog.ShowDialog();
        }
    }
}
