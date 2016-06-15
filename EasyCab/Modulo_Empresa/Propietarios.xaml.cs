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
    /// Lógica de interacción para Propietarios.xaml
    /// </summary>
    public partial class Propietarios : Window
    {
        public enum Departamentos { LPZ, CBBA, SCZ, OR, POT, TRJ, BN, PND, CHQ }
        public Propietarios()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {//agrega propietario
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var administradores = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(txtCI.Text),
                        Procedencia = cmbProcedencia.Text,
                        Nombre = txtNombre.Text,
                        Paterno = txtPaterno.Text,
                        Materno = txtMaterno.Text,
                        Direccion = txtDireccion.Text,
                        Telefono = Convert.ToInt32(txtTelefono.Text),
                        Celular = Convert.ToInt32(txtCelular.Text),
                        Email = txtEmail.Text,
                        Fech_Nac = dtpFechaNac.SelectedDate.Value,
                        Username = txtUsername.Text,
                        Password = txtPassword.Password,
                        IdPerfil = 2
                    };
                    db.Usuario.InsertOnSubmit(administradores);
                    db.SubmitChanges();
                    Cargar(dtgPropietario);
                    Limpiar();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Usuario
                                  where b.IdUsuario == Convert.ToInt32(txtCI.Text)
                                  select b).First();
                    result.Telefono = Convert.ToInt32(txtTelefono.Text);
                    db.SubmitChanges();
                    Cargar(dtgPropietario);
                    Limpiar();
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
                    var result = (from b in db.Usuario
                                  where b.IdUsuario == Convert.ToInt32(txtCI.Text)
                                  select b).First();
                    db.Usuario.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    Cargar(dtgPropietario);
                    Limpiar();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Cargar(DataGrid dtg)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = from b in db.Usuario
                             where b.IdPerfil == 2
                             select new { b.IdUsuario, b.Nombre, b.Paterno, b.Materno, b.Direccion, b.Telefono, b.Celular, b.Email, b.Fech_Nac };
                dtg.ItemsSource = (IEnumerable)result;
                cmbProcedencia.ItemsSource = (IEnumerable)Enum.GetNames(typeof(Departamentos));
            }
        }

        private void dtgPropietario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //txtCI.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["IdUsuario"].ToString();
                txtNombre.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Nombre"].ToString();
                //txtPaterno.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Paterno"].ToString();
                //txtMaterno.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Materno"].ToString();
                //txtDireccion.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Direccion"].ToString();
                //txtTelefono.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Telefono"].ToString();
                //txtCelular.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Celular"].ToString();
                //txtEmail.Text = ((DataRowView)dtgPropietario.SelectedItem).Row["Email"].ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar(dtgPropietario);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCI.Text != string.Empty)
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Usuario
                                  where b.IdUsuario == Convert.ToInt32(txtCI.Text)
                                  select b).First();
                    txtNombre.Text = result.Nombre;
                    txtPaterno.Text = result.Paterno;
                    txtMaterno.Text = result.Materno;
                    txtDireccion.Text = result.Direccion;
                    txtTelefono.Text = result.Telefono.ToString();
                }
            }
        }
        private void Limpiar()
        {
            txtCI.Clear();
            txtNombre.Clear();
            txtPaterno.Clear();
            txtMaterno.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
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

        private void txtCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtCI_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }
    }
}
