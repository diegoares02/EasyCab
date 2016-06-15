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

namespace EasyCab
{
    /// <summary>
    /// Lógica de interacción para MainOperador.xaml
    /// </summary>
    public partial class MainOperador : Window
    {
        public int Id;
        public MainOperador()
        {
            InitializeComponent();
        }

        private void menuCarreras_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Servicios.Carreras car = new Modulo_Servicios.Carreras(); car.Owner = this; car.Id = Id; car.ShowDialog(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void menuUbicacion_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Servicios.Ubicacion ub = new Modulo_Servicios.Ubicacion(); ub.Owner = this; ub.ShowDialog(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
