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
    /// Lógica de interacción para DialogAddGrupo.xaml
    /// </summary>
    public partial class DialogAddGrupo : MetroWindow
    {
        MVGrupo mvGrupo;
        public DialogAddGrupo(tfgEntities tfgEnt)
        {
            InitializeComponent();
            mvGrupo = new MVGrupo(tfgEnt);
            DataContext = mvGrupo;
        }

        private void textCodigo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textCodigo.Text != null && textCodigo.Text != "" && mvGrupo.grupoExiste(textCodigo.Text))
            {
                MessageBox.Show("El Código de grupo introducido ya está siendo utilizado, por favor, verifique que el grupo que está intentando crear no existe ya en el sistema", "GESTIÓN GRUPOS", MessageBoxButton.OK, MessageBoxImage.Error);
                textCodigo.Text = null;
            }
        }

        private Boolean comprobarCamposObligatorios()
        {
            bool correcto = true;
            if (textCodigo.Text == null || textCodigo.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textCodigo);
            }
            else
            {
                ValidacionErrores.quitarError(textCodigo);
            }

            if (textNombre.Text == null || textNombre.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textNombre);
            }
            else
            {
                ValidacionErrores.quitarError(textNombre);
            }
            return correcto;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCamposObligatorios())
            {
                if (mvGrupo.guarda())
                {
                    MessageBox.Show("Grupo añadido correctamente", "GESTIÓN GRUPOS", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Problemas con la base de datos.\nNo se ha añadido el grupo", "GESTIÓN GRUPOS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hay campos obligatorios sin rellenar", "GESTIÓN GRUPOS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
