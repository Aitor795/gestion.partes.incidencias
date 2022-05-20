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
    /// Lógica de interacción para DialogAddMotivoRegistro.xaml
    /// </summary>
    public partial class DialogAddMotivoRegistro : MetroWindow
    {
        MVMotivoRegistro mvMotivoRegistro;
        public DialogAddMotivoRegistro(tfgEntities tfgEnt, tipo_registro tipoRegistroSeleccionado)
        {
            InitializeComponent();
            mvMotivoRegistro = new MVMotivoRegistro(tfgEnt);
            mvMotivoRegistro.tipoRegistroSeleccionado = tipoRegistroSeleccionado;
            DataContext = mvMotivoRegistro;
        }

        private Boolean comprobarCamposObligatorios()
        {
            bool correcto = true;
            if (textMotivo.Text == null || textMotivo.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textMotivo);
            }
            else
            {
                ValidacionErrores.quitarError(textMotivo);
            }

            if (textDescripcion.Text == null || textDescripcion.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textDescripcion);
            }
            else
            {
                ValidacionErrores.quitarError(textDescripcion);
            }
            return correcto;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCamposObligatorios())
            {
                if (mvMotivoRegistro.guarda())
                {
                    MessageBox.Show("Motivo de registro añadido correctamente", "MOTIVO DE REGISTRO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Problemas con la base de datos.\nNo se ha añadido el motivo de registro", "MOTIVO DE REGISTRO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hay campos obligatorios sin rellenar", "MOTIVO DE REGISTRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
