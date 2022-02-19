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
            String usr = txtUser.Text;
            String pass = txtPassword.Password;

            if (mvProfesor.login(usr, pass))
            {
             //   MainWindow ventanaPrincipal = new MainWindow(invEnt, serverUsr.usuLogin);
             //   ventanaPrincipal.Show();
                this.Close();
            }
            else
            {
                txtUser.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUser.Foreground = new SolidColorBrush(Colors.Red);
                txtPassword.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
