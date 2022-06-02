using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Vista.ControlesUsuario;
using gestion.partes.incidencias.Vista.Dialogos;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace gestion.partes.incidencias
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private tfgEntities _tfgEnt;
        private profesor _profesorLogged;
        public MainWindow(tfgEntities tfgEnt, profesor profesorLogged)
        {
            InitializeComponent();
            _profesorLogged = profesorLogged;
            _tfgEnt = tfgEnt;
        }

        private void HamburgerMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            HamburgerMenuGlyphItem hm = e.ClickedItem as HamburgerMenuGlyphItem;
            if (hm != null)
            {
                UserControl uc = new UserControl();
                switch (hm.Tag)
                {
                    case "ListaRegistros":
                        uc = new UCListaRegistros(_tfgEnt, _profesorLogged);
                        break;

                    case "ListaAlumnos":
                        uc = new UCListaAlumnos(_tfgEnt, _profesorLogged);
                        break;

                    case "ListaProfesores":
                        uc = new UCListaProfesores(_tfgEnt, _profesorLogged);
                        break;

                    case "GestionPermisos":
                        uc = new UCGestionPermisos(_tfgEnt);
                        break;

                    case "InformeRegistros":
                        uc = new UCIncidenciasReport(_tfgEnt, _profesorLogged);
                        break;
                }
                hamMenuPrincipal.Content = uc;
            }
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            userBox.Text = _profesorLogged.nombre + " " + _profesorLogged.apellido1 + " " + _profesorLogged.apellido2;

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
                        listaRegistroUC.IsVisible = true;
                    }
                    else if (permiso.codigo == "MOD_ROL")
                    {
                        gestionPermisosUC.IsVisible = true;
                    }
                    else if (permiso.codigo == "REPORT_INCID")
                    {
                        informeRegistrosUC.IsVisible = true;
                    }
                }
            }
        }

        private void btnUsuario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            User dialog = new User(_tfgEnt, _profesorLogged);

            if (dialog.ShowDialog() == true)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }
    }
}
