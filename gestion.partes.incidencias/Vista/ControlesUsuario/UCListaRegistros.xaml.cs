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
        private Predicate<object> predicadoFiltroCombinado;

        public UCListaRegistros(tfgEntities ent)
        {
            InitializeComponent();
            _tfgEnt = ent;
            mvRegistros = new MVRegistros(_tfgEnt, null);
            DataContext = mvRegistros;
            predicadoFiltroCombinado = new Predicate<object>(FiltroCombinado);
        }

        private bool FiltroCombinado(object item)
        {
            bool esta = true;
            if (item != null)
            {
                registro reg = item as registro;
                if (criterios.Count() != 0)
                {
                    esta = criterios.TrueForAll(x => x(reg));
                }
            }
            return esta;
        }
    }
}
