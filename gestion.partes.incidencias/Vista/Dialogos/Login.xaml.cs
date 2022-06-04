using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
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
        private ProfesorServicio profesorServicio;
        public Login()
        {
            InitializeComponent();
            tfgEnt = new tfgEntities();
            profesorServicio = new ProfesorServicio(tfgEnt);
            SplashScreen bienvenida = new SplashScreen("/Recursos/Imagenes/LogoMatisse.jpg");
            bienvenida.Show(true);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string dni = txtDNI.Text;
            string pass = txtPassword.Password;

            if (profesorServicio.login(dni, pass))
            {
                MainWindow ventanaPrincipal = new MainWindow(tfgEnt, profesorServicio.profesor);
                ventanaPrincipal.Show();
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
