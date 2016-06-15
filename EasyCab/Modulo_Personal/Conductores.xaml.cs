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
    /// Lógica de interacción para Conductores.xaml
    /// </summary>
    public partial class Conductores : Window
    {
        DCEasyCabBDDataContext db = new DCEasyCabBDDataContext();
        public enum Departamentos { LPZ, CBBA, SCZ, OR, POT, TRJ, BN, PND, CHQ }
        public enum Categoria { A, B, C}
        public Conductores()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {                    
                    var conductores = new Conductor 
                    {
                        IdConductor = Convert.ToInt32(txtCI.Text),
                        Procedencia = cmbProcedencia.Text,
                        Nombre = txtNombre.Text,
                        Paterno = txtPaterno.Text,
                        Materno = txtMaterno.Text,
                        Direccion = txtDireccion.Text,
                        Telefono = Convert.ToInt32(txtTelefono.Text),
                        Celular = Convert.ToInt32(txtCelular.Text),
                        Categoria=cmbCategoria.Text,
                        Fecha_Nac = dtpFechaNac.SelectedDate.Value,
                        Fecha_Exp=dtpFechaExp.SelectedDate.Value,
                        Comision=Convert.ToSingle(txtComision.Text)
                    };
                    db.Conductor.InsertOnSubmit(conductores);
                    db.SubmitChanges();
                    Cargar();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTelefono.Text != string.Empty)
                {
                    using (var db = new DCEasyCabBDDataContext())
                    {
                        var result = (from b in db.Conductor
                                      where b.IdConductor == Convert.ToInt32(txtCI.Text)
                                      select b).First();
                        result.Telefono = Convert.ToInt32(txtTelefono.Text);
                        db.SubmitChanges();
                        Cargar();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Conductor
                                  where b.IdConductor == Convert.ToInt32(txtCI.Text)
                                  select b).First();
                    db.Conductor.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    Cargar();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Cargar()
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = from b in db.Conductor
                             select b;
               dtgPropietario.ItemsSource = (IEnumerable)result;                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar();
            Cargar_Combobox();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCI.Text != string.Empty)
                {
                    var result = (from b in db.Conductor
                                  where b.IdConductor == Convert.ToInt32(txtCI.Text)
                                  select b).FirstOrDefault();
                    txtNombre.Text = result.Nombre;
                    txtPaterno.Text = result.Paterno;
                    txtMaterno.Text = result.Materno;
                    txtDireccion.Text = result.Direccion;
                    txtTelefono.Text = result.Telefono.ToString();
                    txtTotal.Text = Convert.ToDouble(Total(Convert.ToInt32(txtCI.Text)) * (Convert.ToDouble(result.Comision) / 100)).ToString();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Cargar_Combobox()
        {
            try
            {
                cmbProcedencia.ItemsSource = (IEnumerable)Enum.GetNames(typeof(Departamentos));
                cmbCategoria.ItemsSource = (IEnumerable)Enum.GetNames(typeof(Categoria));
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int Total(int id)
        {
            var result = (from b in db.Servicio
                          where b.IdConductor==id
                          select b.Tarifa).Sum();
            if (result != null)
                return (int)result;
            else { return 0; }
        }

        private void txtCI_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtComision_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
