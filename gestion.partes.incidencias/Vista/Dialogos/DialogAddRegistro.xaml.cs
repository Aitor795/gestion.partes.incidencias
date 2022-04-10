using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.MVVM;
using MahApps.Metro.Controls;

namespace gestion.partes.incidencias.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogAddRegistro.xaml
    /// </summary>
    public partial class DialogAddRegistro : MetroWindow
    {
        private registro _registro;
        private MVRegistros _mvRegistros;

        public DialogAddRegistro(tfgEntities tfgEnt, profesor profesorLogged, registro registro)
        {
            InitializeComponent();
            _registro = registro;
            _mvRegistros = new MVRegistros(tfgEnt, profesorLogged);
            DataContext = _mvRegistros;
        }

    }
}
