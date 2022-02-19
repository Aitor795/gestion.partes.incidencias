using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using System;
using System.Windows;
using System.Windows.Media;

namespace gestion.partes.incidencias.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private tfgEntities tfgEnt;
        private MVProfesor mvProfesor;
        public Login()
        {
            InitializeComponent();
            tfgEnt = new tfgEntities();
            mvProfesor = new MVProfesor(tfgEnt);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string dni = txtDNI.Text;
            string pass = txtPassword.Password;

            if (mvProfesor.login(dni, pass))
            {
             //   MainWindow ventanaPrincipal = new MainWindow(invEnt, serverUsr.usuLogin);
             //   ventanaPrincipal.Show();
                this.Close();
            }
            else
            {
                txtDNI.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                txtDNI.Foreground = new SolidColorBrush(Colors.Red);
                txtPassword.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
