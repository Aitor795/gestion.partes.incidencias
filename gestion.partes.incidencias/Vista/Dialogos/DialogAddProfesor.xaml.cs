using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using gestion.partes.incidencias.Validacion;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace gestion.partes.incidencias.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogAddProfesor.xaml
    /// </summary>
    public partial class DialogAddProfesor : MetroWindow
    {
        private MVProfesor mvProfesor;
        private tfgEntities _tfgEnt;
        private profesor _profesor;
        private profesor _profesorLogged;
        public DialogAddProfesor(tfgEntities tfgEnt, profesor profesorLogged, profesor profesor)
        {
            InitializeComponent();
            _tfgEnt = tfgEnt;
            mvProfesor = new MVProfesor(tfgEnt);
            mvProfesor.profesorSeleccionado = profesor;
            _profesor = profesor;
            _profesorLogged = profesorLogged;
            DataContext = mvProfesor;

            if (profesor.dni == null || profesor.dni == "")
            {
                textDni.IsReadOnly = false;
            }
        }

        private void textDni_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textDni.Text != null && textDni.Text != "" && textDni.IsReadOnly == false && mvProfesor.profesorExiste(textDni.Text))
            {
                MessageBox.Show("El DNI introducido ya está siendo utilizado, por favor, verifique que el profesor que está intentando crear no existe ya en el sistema", "GESTIÓN PROFESORES", MessageBoxButton.OK, MessageBoxImage.Error);
                textDni.Text = null;
            }
        }

        private Boolean comprobarCamposObligatorios()
        {
            bool correcto = true;
            if (textNombre.Text == null || textNombre.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textNombre);
            }
            else
            {
                ValidacionErrores.quitarError(textNombre);
            }

            if (textApellido1.Text == null || textApellido1.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textApellido1);
            }
            else
            {
                ValidacionErrores.quitarError(textApellido1);
            }

            if (textDni.Text == null || textDni.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textDni);
            }
            else
            {
                ValidacionErrores.quitarError(textDni);
            }
            return correcto;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCamposObligatorios())
            {
                if (mvProfesor.guarda())
                {
                    MessageBox.Show("Profesor añadido correctamente", "GESTIÓN PROFESORES", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Problemas con la base de datos.\nNo se ha añadido el profesor", "GESTIÓN PROFESORES", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hay campos obligatorios sin rellenar", "GESTIÓN PROFESORES", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCrearGrupo_Click(object sender, RoutedEventArgs e)
        {
            DialogAddGrupo dialog = new DialogAddGrupo(_tfgEnt);
            if (dialog.ShowDialog() == true)
            {
                mvProfesor.recargarListaGrupos();
                comboFiltroGrupos.ItemsSource = mvProfesor.listaGrupos;
            }
        }

        private void btnCambiarContrasenya_Click(object sender, RoutedEventArgs e)
        {
            DialogNewPassword dialog = new DialogNewPassword(_tfgEnt, _profesorLogged, _profesor);
            dialog.ShowDialog();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<roles_profesor> rolesprofesor = _profesorLogged.roles_profesor.ToList();

            for (int i = 0; i < rolesprofesor.Count; i++)
            {
                rol rol = rolesprofesor[i].rol;

                if (rol.codigo == "ADMIN")
                {
                    btnCambiarContrasenya.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
