using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
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
    /// Lógica de interacción para UCListaRegistros.xaml
    /// </summary>
    public partial class UCListaRegistros : UserControl
    {
        private tfgEntities _tfgEnt;
        private MVRegistros mvRegistros;
        private List<Predicate<registro>> criterios = new List<Predicate<registro>>();
        private Predicate<object> predicadoFiltro;

        public UCListaRegistros(tfgEntities ent)
        {
            InitializeComponent();
            _tfgEnt = ent;
            mvRegistros = new MVRegistros(_tfgEnt, null);
            DataContext = mvRegistros;
            predicadoFiltro = new Predicate<object>(FiltroCombinado);
        }

        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                registro _registro = item as registro;
                if (criterios.Count() != 0)
                {
                    esta = criterios.TrueForAll(x => x(_registro));
                }
            }
            return esta;
        }

        private void BtnFiltro_Click(object sender, RoutedEventArgs e)
        {
            criterios.Clear();
            if (comboFiltroTipoRegistros.SelectedItem != null)
                criterios.Add(new Predicate<registro>(r => r.tipo_registro.id.Equals(mvRegistros.tipoRegistroSeleccionado.id)));
            if (datePickerFechaDesde.SelectedDate != null)
                criterios.Add(new Predicate<registro>(r => r.fecha_suceso >= mvRegistros.fechaDesde));
            /*
            if (!string.IsNullOrEmpty(mvArt.textoFiltroNumSerie))
                criterios.Add(new Predicate<articulo>(a => (a.numserie != null) && a.numserie.Contains(mvArt.textoFiltroNumSerie)));
            */

            dgArticulos.Items.Filter = predicadoFiltro;
        }
    }
}
