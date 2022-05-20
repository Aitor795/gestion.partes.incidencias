using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using gestion.partes.incidencias.VO;
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
    /// Lógica de interacción para UCGestionPermisos.xaml
    /// </summary>
    public partial class UCGestionPermisos : UserControl
    {
        private MVRol mvRol;
        public UCGestionPermisos(tfgEntities ent)
        {
            InitializeComponent();
            mvRol = new MVRol(ent);
            DataContext = mvRol;
        }

        private void cb_Checked(object sender, RoutedEventArgs e)
        {
            permisos_rol permisosRol = new permisos_rol();

            permisosRol.codigo_rol = ((CheckBox)sender).Tag.ToString();
            permisosRol.codigo_permiso = ((GestionPermisoVO)dgPermiso.SelectedItem).permiso.codigo;

            mvRol.guardarPermisoRol(permisosRol);
        }

        private void cb_Unchecked(object sender, RoutedEventArgs e)
        {
            string codigoRol = ((CheckBox)sender).Tag.ToString();
            string codigoPermiso = ((GestionPermisoVO)dgPermiso.SelectedItem).permiso.codigo;

            mvRol.eliminaPermisoRol(codigoRol, codigoPermiso);
        }
    }
}