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

namespace EasyCab.Modulo_Personal
{
    /// <summary>
    /// Lógica de interacción para Dueños.xaml
    /// </summary>
    public partial class Dueños : Window
    {
        DCEasyCabBDDataContext db = new DCEasyCabBDDataContext();
        public Dueños()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (db.DatabaseExists())
                {
                    var du = new Dueño { IdDueño = Convert.ToInt32(txtCI.Text), Nombre = txtNombre.Text, Paterno = txtPaterno.Text, Materno = txtMaterno.Text, Direccion = txtDireccion.Text, Telefono = Convert.ToInt32(txtTelefono.Text) };
                    db.Dueño.InsertOnSubmit(du);
                    db.SubmitChanges();
                    MessageBox.Show("Agregado correctamente");
                    Cargar();
                }
                else { MessageBox.Show("No se puede conectar"); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (db.DatabaseExists())
                {
                    var result = (from b in db.Dueño
                                  where b.IdDueño == Convert.ToInt32(txtCI.Text)
                                  select b).FirstOrDefault();
                    result.Telefono = Convert.ToInt32(txtTelefono.Text);
                    db.SubmitChanges();
                    MessageBox.Show("Modificado correctamente");
                    Cargar();
                }
                else { MessageBox.Show("No se puede conectar"); }
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
                    var result = (from b in db.Dueño
                                  where b.IdDueño == Convert.ToInt32(txtCI.Text)
                                  select b).FirstOrDefault();
                    db.Dueño.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    MessageBox.Show("Eliminado correctamente");
                    Cargar();
                }
                else { MessageBox.Show("No se puede conectar"); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { Cargar(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCI.Text != string.Empty)
                {
                    var result = (from b in db.Dueño
                                  where b.IdDueño == Convert.ToInt32(txtCI.Text)
                                  select b).FirstOrDefault();
                    txtNombre.Text = result.Nombre;
                    txtPaterno.Text = result.Paterno;
                    txtMaterno.Text = result.Materno;
                    txtDireccion.Text = result.Direccion;
                    txtTelefono.Text = result.Telefono.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void Cargar()
        {
            try
            {
                var result = from b in db.Dueño
                             select new { b.IdDueño,b.Nombre,b.Paterno,b.Materno,b.Direccion,b.Telefono};
                dtgPropietario.ItemsSource = (IEnumerable)result;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
