using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using gestion.partes.incidencias.Validacion;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace gestion.partes.incidencias.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogAddRegistro.xaml
    /// </summary>
    public partial class DialogAddRegistro : MetroWindow
    {
        private MVRegistros _mvRegistros;
        private List<Predicate<motivo_registro>> criterios = new List<Predicate<motivo_registro>>();
        private Predicate<object> filtroMotivoRegistro;
        private tfgEntities _tfgEnt;

        public DialogAddRegistro(tfgEntities tfgEnt, profesor profesorLogged, registro registro)
        {
            InitializeComponent();
            _tfgEnt = tfgEnt;
            _mvRegistros = new MVRegistros(tfgEnt, profesorLogged);
            _mvRegistros.setRegistro(registro);
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
            if(comboTipoRegistro.SelectedItem == null)
            {
                btnCrearMotivoRegistro.IsEnabled = false;
                labelSeleccionado.Visibility = Visibility.Collapsed;
                checkSeleccionado.Visibility = Visibility.Collapsed;
                checkSeleccionado.IsChecked = null;
            }
            else if (_mvRegistros.registroSeleccionado.tipo_registro.id != 2)
            {
                btnCrearMotivoRegistro.IsEnabled = true;
                labelSeleccionado.Visibility = Visibility.Collapsed;
                checkSeleccionado.Visibility = Visibility.Collapsed;
                checkSeleccionado.IsChecked = null;
            }
            else
            {
                btnCrearMotivoRegistro.IsEnabled = true;
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

        private void textAlumnoSeleccionado_LostFocus(object sender, RoutedEventArgs e)
        {
            if(textAlumnoSeleccionado.Text != null && textAlumnoSeleccionado.Text != "")
            {
                alumno alumno = _mvRegistros.buscarAlumno(int.Parse(textAlumnoSeleccionado.Text));
                if (alumno != null)
                {
                    textNombreAlumno.Text = alumno.nombre + " " + alumno.apellido1 + " " + alumno.apellido2;
                    ValidacionErrores.quitarError(textAlumnoSeleccionado);
                } 
                else
                {
                    textNombreAlumno.Clear();
                    ValidacionErrores.marcarError(textAlumnoSeleccionado);
                }
            }
            else
            {
                textNombreAlumno.Clear();
            }
        }

        private void textProfesorPresencia_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textProfesorPresencia.Text != null && textProfesorPresencia.Text != "")
            {
                profesor profesor = _mvRegistros.buscarProfesor(textProfesorPresencia.Text);
                if (profesor != null)
                {
                    textNombreProfesor.Text = profesor.nombre + " " + profesor.apellido1 + " " + profesor.apellido2;
                    ValidacionErrores.quitarError(textProfesorPresencia);
                }
                else
                {
                    textNombreProfesor.Clear();
                    ValidacionErrores.marcarError(textProfesorPresencia);
                }
            }
            else
            {
                textNombreProfesor.Clear();
            }
        }

        private Boolean comprobarCamposObligatorios()
        {
            bool correcto = true;
            if(comboTipoRegistro.SelectedItem == null)
            {
                correcto = false;
                ValidacionErrores.marcarError(comboTipoRegistro);
            } 
            else
            {
                ValidacionErrores.quitarError(comboTipoRegistro);
            }

            if (dateFechaSuceso.SelectedDateTime == null)
            {
                correcto = false;
                ValidacionErrores.marcarError(dateFechaSuceso);
            }
            else
            {
                ValidacionErrores.quitarError(dateFechaSuceso);
            }

            if (comboMotivoRegistro.SelectedItem == null)
            {
                correcto = false;
                ValidacionErrores.marcarError(comboMotivoRegistro);
            }
            else
            {
                ValidacionErrores.quitarError(comboMotivoRegistro);
            }

            if (textAlumnoSeleccionado.Text == null || textAlumnoSeleccionado.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textAlumnoSeleccionado);
            }
            else
            {
                ValidacionErrores.quitarError(textAlumnoSeleccionado);
            }

            if (textProfesorPresencia.Text == null || textProfesorPresencia.Text == "")
            {
                correcto = false;
                ValidacionErrores.marcarError(textProfesorPresencia);
            }
            else
            {
                ValidacionErrores.quitarError(textProfesorPresencia);
            }
            return correcto;
        }

        private void textBoxNiaAlumno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textAlumnoSeleccionado.Text, "[^0-9]"))
            {
                textAlumnoSeleccionado.Text = textAlumnoSeleccionado.Text.Remove(textAlumnoSeleccionado.Text.Length - 1);
            }
        }

        private void btnGuardar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (comprobarCamposObligatorios())
            {
                if (_mvRegistros.guarda())
                {
                    MessageBox.Show("Registro añadido correctamente", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Problemas con la base de datos.\nNo se ha añadido el registro", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hay campos obligatorios sin rellenar", "GESTIÓN REGISTROS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCrearMotivoRegistro_Click(object sender, RoutedEventArgs e)
        {
            DialogAddMotivoRegistro dialog = new DialogAddMotivoRegistro(_tfgEnt, (tipo_registro) comboTipoRegistro.SelectedItem);
            if (dialog.ShowDialog() == true)
            {
                comboMotivoRegistro.ItemsSource = _mvRegistros.recargarListaMotivoRegistro();
                comboTipoRegistro_SelectionChanged(null, null);
            }
        }
    }
}
