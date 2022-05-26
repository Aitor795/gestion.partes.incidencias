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
    /// Lógica de interacción para DialogNewPassword.xaml
    /// </summary>
    public partial class DialogNewPassword : MetroWindow
    {
        private MVProfesor mvProfesor;
        private profesor _profesor;
        private profesor _profesorLogged;

        public DialogNewPassword(tfgEntities tfgEnt, profesor profesorLogged, profesor profesor)
        {
            InitializeComponent();
            mvProfesor = new MVProfesor(tfgEnt);
            _profesorLogged = profesorLogged;
            _profesor = profesor;
            mvProfesor.profesorSeleccionado = profesor;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<roles_profesor> rolesprofesor = _profesorLogged.roles_profesor.ToList();

            for (int i = 0; i < rolesprofesor.Count; i++)
            {
                rol rol = rolesprofesor[i].rol;

                if (rol.codigo == "ADMIN")
                {
                    titleActual.Visibility = Visibility.Collapsed;
                    textActual.Visibility = Visibility.Collapsed;
                }
            }
        }

        private Boolean comprobarCamposObligatorios()
        {
            bool correcto = true;
            bool camposObligatorios = true;

            if (textActual.Visibility == Visibility.Visible && (textActual.Password == null || textActual.Password == ""))
            {
                correcto = false;
                camposObligatorios = false;
                ValidacionErrores.marcarError(textNueva1);
            }
            else
            {
                if (_profesor.contrasenya != textActual.Password)
                {
                    MessageBox.Show("La contraseña actual no es correcta", "GESTIÓN CONTRASEÑA", MessageBoxButton.OK, MessageBoxImage.Error);
                    ValidacionErrores.marcarError(textActual);
                    correcto = false;
                }
                else
                {
                    ValidacionErrores.quitarError(textActual);
                }
            }

            if (textNueva1.Password == null || textNueva1.Password == "")
            {
                correcto = false;
                camposObligatorios = false;
                ValidacionErrores.marcarError(textNueva1);
            }
            else
            {
                ValidacionErrores.quitarError(textNueva1);
            }

            if (textNueva2.Password == null || textNueva2.Password == "")
            {
                correcto = false;
                camposObligatorios = false;
                ValidacionErrores.marcarError(textNueva2);
            }
            else
            {
                ValidacionErrores.quitarError(textNueva2);
            }

            if(textNueva1.Password != textNueva2.Password)
            {
                correcto = false;
                ValidacionErrores.marcarError(textNueva1);
                ValidacionErrores.marcarError(textNueva2);
                MessageBox.Show("La contraseña y la confirmación no coinciden, asegurese de que sean iguales", "GESTIÓN CONTRASEÑA", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ValidacionErrores.quitarError(textNueva1);
                ValidacionErrores.quitarError(textNueva2);
            }

            if (camposObligatorios == false)
            {
                MessageBox.Show("Hay campos obligatorios sin rellenar", "GESTIÓN CONTRASEÑA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return correcto;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCamposObligatorios())
            {
                _profesor.contrasenya = textNueva1.Password;
                if (mvProfesor.guarda())
                {
                    MessageBox.Show("Contrraseña modificada correctamente", "GESTIÓN CONTRASEÑA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Problemas con la base de datos.\nNo se ha modificado la contraseña", "GESTIÓN CONTRASEÑA", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
        }
    }
}
