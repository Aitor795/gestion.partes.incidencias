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
    /// Lógica de interacción para DialogAddAlumno.xaml
    /// </summary>
    public partial class DialogAddAlumno : MetroWindow
    {
        private MVAlumno mvAlumno;
        public DialogAddAlumno(tfgEntities tfgEnt, alumno alumno)
        {
            InitializeComponent();
            mvAlumno = new MVAlumno(tfgEnt);
            mvAlumno.alumnoSeleccionado = alumno;
            DataContext = mvAlumno;

            if (alumno.nia == 0)
            {
                textNiaAlumno.IsReadOnly = false;
            }
        }

        private void textBoxNumberOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox campo = (TextBox) sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(campo.Text, "[^0-9]"))
            {
                campo.Text = campo.Text.Remove(campo.Text.Length - 1);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCamposObligatorios())
            {
                if (mvAlumno.guarda())
                {
                    MessageBox.Show("Registro añadido correctamente", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Problemas con la base de datos.\nNo se ha añadido el registro", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hay campos obligatorios sin rellenar", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Boolean comprobarCamposObligatorios()
        {
            bool correcto = true;
            if (comboFiltroGrupos.SelectedItem == null)
            {
                correcto = false;
                ValidacionErrores.marcarError(comboFiltroGrupos);
            }
            else
            {
                ValidacionErrores.quitarError(comboFiltroGrupos);
            }

            if (textNombreAlumno.Text == null || textNombreAlumno.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textNombreAlumno);
            }
            else
            {
                ValidacionErrores.quitarError(textNombreAlumno);
            }

            if (textApellido1Alumno.Text == null || textApellido1Alumno.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textApellido1Alumno);
            }
            else
            {
                ValidacionErrores.quitarError(textApellido1Alumno);
            }

            if (textNiaAlumno.Text == null || textNiaAlumno.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textNiaAlumno);
            }
            else
            {
                ValidacionErrores.quitarError(textNiaAlumno);
            }
            return correcto;
        }

        private void textTelefonoAlumno_LostFocus(object sender, RoutedEventArgs e)
        {
            if(textTelefonoAlumno.Text == "")
            {
                mvAlumno.alumnoSeleccionado.telefono = null;
            }
        }

        private void textMovilAlumno_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textMovilAlumno.Text == "")
            {
                mvAlumno.alumnoSeleccionado.movil = null;
            }
        }

        private void textNiaAlumno_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textNiaAlumno.Text != null && textNiaAlumno.Text != "" && textNiaAlumno.IsReadOnly == false && mvAlumno.alumnoExiste(int.Parse(textNiaAlumno.Text)))
            {
                MessageBox.Show("El NIA introducido ya está siendo utilizado, por favor, verifique que el alumno que está intentando crear no existe ya en el sistema", "GESTIÓN ALUMNOS", MessageBoxButton.OK, MessageBoxImage.Error);
                textNiaAlumno.Text = null;
            }
        }
    }
}
