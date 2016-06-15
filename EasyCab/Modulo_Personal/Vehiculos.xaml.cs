using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace EasyCab.Modulo_Personal
{
    /// <summary>
    /// Lógica de interacción para Vehiculos.xaml
    /// </summary>
    public partial class Vehiculos : Window
    {
        int aux;
        public Vehiculos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var administradores = new Vehiculo
                    {
                        IdVehiculo=Ultimo()+1,
                        IdConductor=BuscarC(cmbConductor.Text),
                        IdDueño=BuscarD(cmbDueño.Text),
                        Marca=txtMarca.Text,
                        Modelo =Convert.ToInt32(txtModelo.Text),
                        NroPlaca=txtPlaca.Text,
                        NroChasis=txtChasis.Text,
                        SOAT=txtSOAT.Text,
                    };
                    db.Vehiculo.InsertOnSubmit(administradores);
                    db.SubmitChanges();
                    Cargar(dtgPropietario);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Vehiculo
                              where b.IdDueño == BuscarD(cmbDueño.Text)
                              select b).First();
                result.IdConductor = BuscarC(cmbConductor.Text);
                db.SubmitChanges();
                Cargar(dtgPropietario);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Vehiculo
                               where b.IdDueño==BuscarD(cmbDueño.Text)
                               select b).First();
                db.Vehiculo.DeleteOnSubmit(result);
                db.SubmitChanges();
                Cargar(dtgPropietario);
            }
        }
        private void Cargar(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.Vehiculo
                                 select new { b.IdVehiculo,b.Marca,b.Modelo,b.IdConductor,b.IdDueño,b.NroPlaca,b.NroChasis};
                   
                    var dueño = from c in db.Dueño
                                select c.Nombre;
                    
                    var conductores = from d in db.Conductor
                                      select d.Nombre;

                    if (result != null && dueño != null && conductores != null)
                    { dtg.ItemsSource = (IEnumerable)result; cmbDueño.ItemsSource = (IEnumerable)dueño; cmbConductor.ItemsSource = (IEnumerable)conductores; }
                    else { MessageBox.Show("Problema de conexion"); }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try { Cargar(dtgPropietario); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private int BuscarC(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from d in db.Conductor
                             where d.Nombre == nombre
                             select d).First();
                return result.IdConductor;
            }
        }
        private int BuscarD(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from d in db.Dueño
                              where d.Nombre == nombre
                              select d).First();
                return result.IdDueño;
            }
        }
        private int Ultimo()
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from d in db.Vehiculo
                              orderby d.IdVehiculo descending
                              select d).FirstOrDefault();
                if (result != null)
                    return result.IdVehiculo;
                else { return 0; }
            }
        }

        private void txtModelo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
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
    }
}
