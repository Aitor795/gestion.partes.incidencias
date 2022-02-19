using gestion.partes.incidencias.Modelo;
using MahApps.Metro.Controls;

namespace gestion.partes.incidencias
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private tfgEntities _tfgEnt;
        private profesor _profesor;
        public MainWindow(tfgEntities tfgEnt, profesor profesor)
        {
            InitializeComponent();
            _profesor = profesor;
            _tfgEnt = tfgEnt;
        }
    }
}
