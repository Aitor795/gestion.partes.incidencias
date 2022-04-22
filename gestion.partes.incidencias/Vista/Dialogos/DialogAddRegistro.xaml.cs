using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace gestion.partes.incidencias.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogAddRegistro.xaml
    /// </summary>
    public partial class DialogAddRegistro : MetroWindow
    {
        private registro _registro;
        private MVRegistros _mvRegistros;
        private List<Predicate<motivo_registro>> criterios = new List<Predicate<motivo_registro>>();
        private Predicate<object> filtroMotivoRegistro;

        public DialogAddRegistro(tfgEntities tfgEnt, profesor profesorLogged, registro registro)
        {
            InitializeComponent();
            _registro = registro;
            _mvRegistros = new MVRegistros(tfgEnt, profesorLogged);
            filtroMotivoRegistro = new Predicate<object>(FiltroCombinado);
            DataContext = _mvRegistros;
            _mvRegistros.registroSeleccionado.dni_profesor_registro = profesorLogged.dni;
        }

        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                motivo_registro _motivoRegistro = item as motivo_registro;
                if (criterios.Count() != 0)
                {
                    esta = criterios.TrueForAll(x => x(_motivoRegistro));
                }
            }
            return esta;
        }

        private void comboTipoRegistro_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(comboTipoRegistro.SelectedItem == null || _mvRegistros.registroSeleccionado.tipo_registro.id != 2)
            {
                labelSeleccionado.Visibility = Visibility.Collapsed;
                checkSeleccionado.Visibility = Visibility.Collapsed;
                checkSeleccionado.IsChecked = null;
            } 
            else
            {
                labelSeleccionado.Visibility = Visibility.Visible;
                checkSeleccionado.Visibility = Visibility.Visible;
            }
            criterios.Clear();
            criterios.Add(new Predicate<motivo_registro>(r => {
                if (comboTipoRegistro.SelectedItem != null)
                    return r.tipo_registro.id.Equals(_mvRegistros.registroSeleccionado.tipo_registro.id);
                else
                    return false;
                }
            ));
            comboMotivoRegistro.Items.Filter = filtroMotivoRegistro;
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            comboTipoRegistro_SelectionChanged(sender, null);
        }

        private void btnGuardar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_mvRegistros.guarda())
            {
                MessageBox.Show("Artículo añadido correctamente", "GESTIÓN ARTÍCULOS", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Problemas con la base de datos.\nNo se ha añadido el artículo", "GESTIÓN ARTÍCULOS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogResult = true;
        }
    }
}
