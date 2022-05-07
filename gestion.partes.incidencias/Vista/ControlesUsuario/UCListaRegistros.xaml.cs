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

            dgRegistros.Items.Filter = predicadoFiltro;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BtnFiltro_Click(sender, e);

            List<roles_profesor> rolesprofesor = _profesorLogged.roles_profesor.ToList();

            for (int i = 0; i < rolesprofesor.Count; i++)
            {
                rol rol = rolesprofesor[i].rol;

                List<permisos_rol> permisosprofesor = rol.permisos_rol.ToList();

                for (int j = 0; j < permisosprofesor.Count; j++)
                {
                    permiso permiso = permisosprofesor[j].permiso;

                    if (permiso.codigo == "ADD_REGISTRO")
                    {
                        btnAnyadirRegistro.IsEnabled = true;
                        return;
                    }
                }
            }
        }

        private void btnAnyadirRegistro_Click(object sender, RoutedEventArgs e)
        {
            DialogAddRegistro dialog = new DialogAddRegistro(_tfgEnt, _profesorLogged, new registro());
            if (dialog.ShowDialog() == true)
            {
                mvRegistros.recargarListaRegistrosTabla();
                dgRegistros.ItemsSource = mvRegistros.listaRegistrosTabla;
            }
        }

        private void btnEditarRegistro_Click(object sender, RoutedEventArgs e)
        {
            if(dgRegistros.SelectedItem != null)
            {
                DialogAddRegistro dialog = new DialogAddRegistro(_tfgEnt, _profesorLogged, (registro) dgRegistros.SelectedItem);
                dialog.ShowDialog();
            }
        }

        private void btnEliminarRegistro_Click(object sender, RoutedEventArgs e)
        {
            if(dgRegistros.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Se dispone a eliminar el registro seleccionado.", "¡ADVERTENCIA!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(MessageBoxResult.Yes == result)
                {
                    MessageBoxResult result2 = MessageBox.Show("¿Serguro que quiere eliminar el registro de forma permanente?", "¡ADVERTENCIA!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if(MessageBoxResult.Yes == result2)
                    {
                        mvRegistros.elimina((registro) dgRegistros.SelectedItem);
                        MessageBox.Show("Registro eliminado correctamente", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Information);
                        mvRegistros.recargarListaRegistrosTabla();
                        dgRegistros.ItemsSource = mvRegistros.listaRegistrosTabla;
                    }
                }
            }
        }

        private void dgRegistros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRegistros.SelectedItem != null)
            {
                registro registro = (registro)dgRegistros.SelectedItem;
                List<roles_profesor> rolesprofesor = _profesorLogged.roles_profesor.ToList();

                for (int i = 0; i < rolesprofesor.Count; i++)
                {
                    rol rol = rolesprofesor[i].rol;

                    if (rol.codigo == "PROFESOR")
                    {
                        if (registro.dni_profesor == _profesorLogged.dni || registro.dni_profesor_registro == _profesorLogged.dni)
                        {
                            btnEditarRegistro.IsEnabled = true;
                            btnEliminarRegistro.IsEnabled = true;
                            break;
                        }
                        else
                        {
                            btnEditarRegistro.IsEnabled = false;
                            btnEliminarRegistro.IsEnabled = false;
                        }
                    }
                    else if (rol.codigo == "TUTOR")
                    {
                        if (registro.alumno.grupo != null && _profesorLogged.grupo != null && registro.alumno.grupo == _profesorLogged.grupo)
                        {
                            btnEditarRegistro.IsEnabled = true;
                            btnEliminarRegistro.IsEnabled = true;
                            break;
                        }
                        else
                        {
                            btnEditarRegistro.IsEnabled = false;
                            btnEliminarRegistro.IsEnabled = false;
                        }
                    }
                    else if (rol.codigo == "ADMIN")
                    {
                        btnEditarRegistro.IsEnabled = true;
                        btnEliminarRegistro.IsEnabled = true;
                        break;
                    }
                    else if (rol.codigo == "DIRECTIVO")
                    {
                        btnEditarRegistro.IsEnabled = true;
                        btnEliminarRegistro.IsEnabled = true;
                        break;
                    }
                    else
                    {
                        btnEditarRegistro.IsEnabled = false;
                        btnEliminarRegistro.IsEnabled = false;
                    }
                }
            }
            else
            {
                btnEditarRegistro.IsEnabled = false;
                btnEliminarRegistro.IsEnabled = false;
            }
        }
    }
}
