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
    /// Lógica de interacción para UCListaProfesores.xaml
    /// </summary>
    public partial class UCListaProfesores : UserControl
    {
        private tfgEntities _tfgEnt;
        private profesor _profesorLogged;
        private MVProfesor mvProfesor;
        private List<Predicate<profesor>> criterios = new List<Predicate<profesor>>();
        private Predicate<object> predicadoFiltro;
        public UCListaProfesores(tfgEntities ent, profesor profesorLogged)
        {
            InitializeComponent();
            _tfgEnt = ent;
            _profesorLogged = profesorLogged;
            mvProfesor = new MVProfesor(_tfgEnt);
            DataContext = mvProfesor;
            predicadoFiltro = new Predicate<object>(FiltroCombinado);
        }

        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                profesor _profesor = item as profesor;
                if (criterios.Count() != 0)
                {
                    esta = criterios.TrueForAll(x => x(_profesor));
                }
            }
            return esta;
        }

        private void btnFiltro_Click(object sender, RoutedEventArgs e)
        {
            criterios.Clear();
            if (comboFiltroGrupos.SelectedItem != null)
                criterios.Add(new Predicate<profesor>(p => p.grupo != null && p.grupo.codigo.Equals(mvProfesor.grupoSeleccionado.codigo)));
            if (!string.IsNullOrEmpty(mvProfesor.textFiltroDni))
                criterios.Add(new Predicate<profesor>(p => p.dni.Equals(mvProfesor.textFiltroDni)));
            if (!string.IsNullOrEmpty(mvProfesor.textFiltroNombre))
                criterios.Add(new Predicate<profesor>(p => p.nombre != null && p.nombre.ToUpper().Equals(mvProfesor.textFiltroNombre.ToUpper())));
            if (!string.IsNullOrEmpty(mvProfesor.textFiltroApellido1))
                criterios.Add(new Predicate<profesor>(p => p.apellido1 != null && p.apellido1.ToUpper().Equals(mvProfesor.textFiltroApellido1.ToUpper())));
            if (!string.IsNullOrEmpty(mvProfesor.textFiltroApellido2))
                criterios.Add(new Predicate<profesor>(p => p.apellido2 != null && p.apellido2.ToUpper().Equals(mvProfesor.textFiltroApellido2.ToUpper())));

            dgProfesor.Items.Filter = predicadoFiltro;
        }

        private void btnAnyadirProfesor_Click(object sender, RoutedEventArgs e)
        {
            DialogAddProfesor dialog = new DialogAddProfesor(_tfgEnt, new profesor());
            if (dialog.ShowDialog() == true)
            {
                mvProfesor.recargarListaProfesoresTabla();
                dgProfesor.ItemsSource = mvProfesor.listaProfesoresTabla;
                mvProfesor.recargarListaGrupos();
                comboFiltroGrupos.ItemsSource = mvProfesor.listaGrupos;
            }
        }

        private void btnEditarProfesor_Click(object sender, RoutedEventArgs e)
        {
            if (dgProfesor.SelectedItem != null)
            {
                DialogAddProfesor dialog = new DialogAddProfesor(_tfgEnt, (profesor) dgProfesor.SelectedItem);
                if (dialog.ShowDialog() == true)
                {
                    mvProfesor.recargarListaGrupos();
                    comboFiltroGrupos.ItemsSource = mvProfesor.listaGrupos;
                }
            }
        }

        private void dgProfesor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgProfesor.SelectedItem == null)
            {
                btnEditarProfesor.IsEnabled = false;
            }
            else
            {
                btnEditarProfesor.IsEnabled = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgProfesor.SelectedItem = null;
        }
    }
}