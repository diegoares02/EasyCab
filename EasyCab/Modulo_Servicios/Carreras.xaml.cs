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
    /// Lógica de interacción para Carreras.xaml
    /// </summary>
    public partial class Carreras : Window
    {
        public int Id; public int propietario;
        DCEasyCabBDDataContext db = new DCEasyCabBDDataContext();
        public Carreras()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Cargar_Combobox();
                Cargar();
                txtId.Text = Id.ToString();
                if (propietario == 2)
                {
                    btnAgregar.IsEnabled = false;
                    dtgCarreras.IsEnabled = false;
                    btnConfirmar.IsEnabled = false;
                }
                txtTotal.Text = Total().ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (db.DatabaseExists())
                {
                    if (txtNIT.Text != string.Empty)
                    {
                        var cliente = new Cliente { Nombre = txtNombre.Text, Direccion = txtDireccionOrigen.Text, Telefono = Convert.ToInt32(txtTelefono.Text) }; db.Cliente.InsertOnSubmit(cliente);
                        db.SubmitChanges();                        
                    }
                    else
                    {
                        var cliente = new Cliente { Nombre = txtNombre.Text, Direccion = txtDireccionOrigen.Text, Telefono = Convert.ToInt32(txtTelefono.Text) }; db.Cliente.InsertOnSubmit(cliente);
                        db.SubmitChanges();
                    }
                    btnConfirmar.IsEnabled = true;
                    btnBuscar.IsEnabled = true;
                    MessageBox.Show("Cliente agregado correctamente");                    
                }
                else { MessageBox.Show("No se puede acceder a la base de datos"); }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (db.DatabaseExists())
                {
                    var servicio = new Servicio { IdUsuario = Id, IdCliente = BuscarU(txtNombre.Text), IdConductor = BuscarC(cmbConductor.Text), Fecha = DateTime.Today, DireccionOrigen = txtDireccionOrigen.Text, DireccionDestino = txtDireccionDestino.Text, Tarifa = Convert.ToInt32(txtTarifa.Text) };
                    db.Servicio.InsertOnSubmit(servicio);
                    db.SubmitChanges();
                    Cargar();
                    Borrar();
                    txtTotal.Text = Total().ToString();
                }
                else { MessageBox.Show("No se puede acceder a la base de datos"); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                var result = (from b in db.Servicio
                             where b.IdCliente==BuscarU(txtNombre.Text)
                             select b).FirstOrDefault(); 
                if (result != null)
                {
                    txtDireccionOrigen.Text = result.DireccionOrigen;
                    txtDireccionDestino.Text = result.DireccionDestino;
                    txtTarifa.Text = result.Tarifa.ToString();
                }
                else { MessageBox.Show("No se encontro al cliente"); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Cargar()
        {
            try
            {
                var result = from b in db.Servicio
                             select new { b.IdServicio,b.IdConductor,b.IdUsuario,b.IdCliente,b.Fecha,b.DireccionOrigen, b.DireccionDestino,b.Tarifa};
                dtgCarreras.ItemsSource = (IEnumerable)result;
                btnBuscar.IsEnabled = false;
                btnConfirmar.IsEnabled = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Cargar_Combobox()
        {
            try
            {
                var result = from b in db.Conductor
                             select b.Nombre;
                cmbConductor.ItemsSource = (IEnumerable)result;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarU(string Nombre)
        {
            var result = (from b in db.Cliente
                          where b.Nombre == Nombre
                          select b.IdCliente).FirstOrDefault();
            return result;
        }
        private int BuscarC(string nombre)
        {
            var result = (from b in db.Conductor
                          where b.Nombre == nombre
                          select b.IdConductor).FirstOrDefault();
            return result;
        }
        private void Borrar()
        {
            txtNombre.Clear();
            txtDireccionDestino.Clear();
            txtDireccionOrigen.Clear();
            txtTarifa.Clear();
            txtTelefono.Clear();
            txtNIT.Clear();
        }
        private int Total()
        {
            var result = (from b in db.Servicio
                          select b.Tarifa).Sum();
            return (int)result;
        }

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Reportes.ReporteIngresos rep = new Reportes.ReporteIngresos();
                rep.ShowDialog();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void SoloNumeros(TextCompositionEventArgs e)
        {
            //se convierte a Ascci del la tecla presionada
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            //verificamos que se encuentre en ese rango que son entre el 0 y el 9
            if (ascci >= 48 && ascci <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtNIT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }
    }
}
