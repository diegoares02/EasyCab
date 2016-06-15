using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections;
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

namespace EasyCab.Modulo_Servicios
{
    /// <summary>
    /// Lógica de interacción para Ubicacion.xaml
    /// </summary>
    public partial class Ubicacion : Window
    {
        public Ubicacion()
        {
            InitializeComponent();
        }

        private void btnObtener_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double lat = -16.5155739, lon = -68.0915129;
                DateTime fecha=DateTime.Today;
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from d in db.Ubicacion
                                 where d.IdConductor == BuscarC(cmbConductor.Text)
                                 select new { d.Latitud, d.Longitud,d.Fecha };
                    foreach (var item in result)
                    {
                        lat = item.Latitud;
                        lon = item.Longitud;
                        fecha = item.Fecha;
                    }
                    txtLongitud.Text = lon.ToString();
                    txtLatitud.Text = lat.ToString();
                    txtFecha.Text = fecha.ToString();
                    Point punto = new Point(lat,lon);
                    Location loc = myMap.ViewportPointToLocation(punto);
                    Pushpin pin = new Pushpin();
                    pin.Location = loc;
                    myMap.Children.Add(pin);                    
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar();
        }
        private void Cargar()
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from d in db.Conductor
                                 select d.Nombre;
                    cmbConductor.ItemsSource = (IEnumerable)result;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarC(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from d in db.Conductor
                              where d.Nombre==nombre
                             select d.IdConductor).FirstOrDefault();
                return result;
            }
        }
    }
}
