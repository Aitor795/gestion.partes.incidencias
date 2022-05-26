using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
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
    /// Lógica de interacción para User.xaml
    /// </summary>
    public partial class User : MetroWindow
    {
        private MVProfesor mvProfesor;
        private profesor _profesorLogged;
        private tfgEntities _tfgEnt;

        public User(tfgEntities tfgEnt, profesor profesorLogged)
        {
            InitializeComponent();
            mvProfesor = new MVProfesor(tfgEnt);
            _profesorLogged = profesorLogged;
            mvProfesor.profesorSeleccionado = profesorLogged;
            _tfgEnt = tfgEnt;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            titulo.Text = _profesorLogged.nombre + " " + _profesorLogged.apellido1 + " " + _profesorLogged.apellido2;
        }

        private void btnCambiarContrasenya_Click(object sender, RoutedEventArgs e)
        {
            DialogNewPassword dialog = new DialogNewPassword(_tfgEnt, _profesorLogged, _profesorLogged);
            dialog.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
