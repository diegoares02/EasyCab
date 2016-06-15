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

namespace EasyCab.Modulo_Empresa
{
    /// <summary>
    /// Lógica de interacción para Sucursales.xaml
    /// </summary>
    public partial class Sucursales : Window
    {
        int aux;
        DCEasyCabBDDataContext db = new DCEasyCabBDDataContext();
        public Sucursales()
        {
            InitializeComponent();
        }       

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Sucursal
                                  where b.IdSucursal == aux
                                  select b).First();
                    db.Sucursal.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    Cargar(dtgSucursal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Sucursal
                                  where b.IdSucursal == aux
                                  select b).First();
                    result.Direccion = txtDireccion.Text;
                    db.SubmitChanges();
                    Cargar(dtgSucursal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var empresas = new Sucursal
                    {
                        Direccion=txtDireccion.Text,
                        Telefono=Convert.ToInt32(txtTelefono.Text),
                        IdEmpresa=BuscarE(cmbEmpresa.Text),
                    };
                    db.Sucursal.InsertOnSubmit(empresas);
                    db.SubmitChanges();
                    Cargar(dtgSucursal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Cargar(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var emp = from b in db.Sucursal
                              select new { b.IdSucursal,b.IdEmpresa,b.Direccion,b.Telefono};
                    dtg.ItemsSource = (IEnumerable)emp;
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarE(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Empresa
                              where b.Nombre == nombre
                              select b).First();
                return result.IdEmpresa;
            }
        }
        private void Cargar_Combobox()
        {
            try
            {
                var result = from b in db.Empresa
                             select b.Nombre;
                cmbEmpresa.ItemsSource = (IEnumerable)result;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar(dtgSucursal);
            Cargar_Combobox();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            var result = (from d in db.Sucursal
                          where d.IdEmpresa == BuscarE(cmbEmpresa.Text)
                          select d).FirstOrDefault();
            txtDireccion.Text = result.Direccion;
            txtTelefono.Text = result.Telefono.ToString();
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
