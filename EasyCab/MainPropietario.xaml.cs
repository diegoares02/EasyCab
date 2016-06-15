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
    /// Lógica de interacción para MainPropietario.xaml
    /// </summary>
    public partial class MainPropietario : Window
    {
        public int id;
        public MainPropietario()
        {
            InitializeComponent();
        }

        private void menuConductor_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Personal.Conductores con = new Modulo_Personal.Conductores();con.Owner = this; con.ShowDialog(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void menuVehiculo_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Personal.Vehiculos ve = new Modulo_Personal.Vehiculos(); ve.Owner = this; ve.ShowDialog(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void menuOperador_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Personal.Operadores op = new Modulo_Personal.Operadores(); op.Owner = this; op.ShowDialog(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void menuDueño_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Personal.Dueños du = new Modulo_Personal.Dueños(); du.Owner = this; du.ShowDialog(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void menuCarreras_Click(object sender, RoutedEventArgs e)
        {
            try
            { Modulo_Servicios.Carreras car = new Modulo_Servicios.Carreras(); car.Owner = this; car.propietario = 2; car.Id = id;car.ShowDialog(); }
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
