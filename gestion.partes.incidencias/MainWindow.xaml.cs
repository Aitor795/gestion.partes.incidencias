using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Vista.ControlesUsuario;
using MahApps.Metro.Controls;
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
                    /*
                    case "Usuario":
                        uc = new UCUsuario(invEnt, usuLogin);
                        break;
                    case "Articulos":
                        uc = new UCArticulos(invEnt, usuLogin);
                        break;
                    case "Listar Modelos":
                        uc = new UCListaModelos(invEnt);
                        break;
                    */
                    case "ListarRegistros":
                        uc = new UCListaRegistros(_tfgEnt, _profesorLogged);
                        break;
                    
                    /*
                    case "Listar Usuarios":
                        uc = new UCListaUsuarios(invEnt);
                        break;
                    */
                }
                hm.Tag = uc;
                hamMenuPrincipal.Content = uc;
            }
        }
    }
}
