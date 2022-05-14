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
    /// Lógica de interacción para UCListaAlumnos.xaml
    /// </summary>
    public partial class UCListaAlumnos : UserControl
    {
        private tfgEntities _tfgEnt;
        private profesor _profesorLogged;
        private MVAlumno mvAlumno;
        private List<Predicate<alumno>> criterios = new List<Predicate<alumno>>();
        private Predicate<object> predicadoFiltro;
        public UCListaAlumnos(tfgEntities ent, profesor profesorLogged)
        {
            InitializeComponent();
            _tfgEnt = ent;
            _profesorLogged = profesorLogged;
            mvAlumno = new MVAlumno(_tfgEnt);
            DataContext = mvAlumno;
            predicadoFiltro = new Predicate<object>(FiltroCombinado);
        }

        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                alumno _alumno = item as alumno;
                if (criterios.Count() != 0)
                {
                    esta = criterios.TrueForAll(x => x(_alumno));
                }
            }
            return esta;
        }

        private void btnFiltro_Click(object sender, RoutedEventArgs e)
        {
            criterios.Clear();
            if (comboFiltroGrupos.SelectedItem != null)
                criterios.Add(new Predicate<alumno>(a => a.grupo != null && a.grupo.codigo.Equals(mvAlumno.grupoSeleccionado.codigo)));
            if (!string.IsNullOrEmpty(mvAlumno.textFiltroNia))
                criterios.Add(new Predicate<alumno>(a => a.nia.Equals(int.Parse(mvAlumno.textFiltroNia))));
            if (!string.IsNullOrEmpty(mvAlumno.textFiltroNombre))
                criterios.Add(new Predicate<alumno>(a => a.nombre != null && a.nombre.ToUpper().Equals(mvAlumno.textFiltroNombre.ToUpper())));
            if (!string.IsNullOrEmpty(mvAlumno.textFiltroApellido1))
                criterios.Add(new Predicate<alumno>(a => a.apellido1 != null && a.apellido1.ToUpper().Equals(mvAlumno.textFiltroApellido1.ToUpper())));
            if (!string.IsNullOrEmpty(mvAlumno.textFiltroApellido2))
                criterios.Add(new Predicate<alumno>(a => a.apellido2 != null && a.apellido2.ToUpper().Equals(mvAlumno.textFiltroApellido2.ToUpper()))); 

            dgAlumnos.Items.Filter = predicadoFiltro;
        }

        private void dgAlumnos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBoxNiaAlumno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(mvAlumno.textFiltroNia, "[^0-9]"))
            {
                mvAlumno.textFiltroNia = mvAlumno.textFiltroNia.Remove(mvAlumno.textFiltroNia.Length - 1);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnyadirAlumno_Click(object sender, RoutedEventArgs e)
        {
            DialogAddAlumno dialog = new DialogAddAlumno(_tfgEnt, new alumno());
            if (dialog.ShowDialog() == true)
            {
                mvAlumno.recargarListaAlumnosTabla();
                dgAlumnos.ItemsSource = mvAlumno.listaAlumnosTabla;
            }
        }

        private void btnEditarAlumno_Click(object sender, RoutedEventArgs e)
        {
            if (dgAlumnos.SelectedItem != null)
            {
                DialogAddAlumno dialog = new DialogAddAlumno(_tfgEnt, (alumno) dgAlumnos.SelectedItem);
                dialog.ShowDialog();
            }
        }

        private void btnEliminarAlumno_Click(object sender, RoutedEventArgs e)
        {
            if (dgAlumnos.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Se dispone a eliminar el alumno seleccionado.", "¡ADVERTENCIA!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (MessageBoxResult.Yes == result)
                {
                    // TODO comprobar que el alumno no tenga registros

                    MessageBoxResult result2 = MessageBox.Show("¿Serguro que quiere eliminar el alumno de forma permanente?", "¡ADVERTENCIA!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (MessageBoxResult.Yes == result2)
                    {
                        mvAlumno.elimina((alumno) dgAlumnos.SelectedItem);
                        MessageBox.Show("Alumno eliminado correctamente", "GESTIÓN DE ALUMNOS", MessageBoxButton.OK, MessageBoxImage.Information);
                        mvAlumno.recargarListaAlumnosTabla();
                        dgAlumnos.ItemsSource = mvAlumno.listaAlumnosTabla;
                    }
                }
            }
        }
    }
}
