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
    /// Lógica de interacción para Operadores.xaml
    /// </summary>
    public partial class Operadores : Window
    {
        public enum Departamentos { LPZ, CBBA, SCZ, OR, POT, TRJ, BN, PND, CHQ }
        public Operadores()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
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
                        IdPerfil=3,
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Usuario
                              where b.IdUsuario == Convert.ToInt32(txtCI.Text)
                              select b).First();
                db.Usuario.DeleteOnSubmit(result);
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
                    var result = from b in db.Usuario
                                 where b.IdPerfil == 3
                                 select new { b.Nombre, b.Paterno, b.Materno, b.Direccion, b.Telefono, b.Celular, b.Email, b.Fech_Nac };
                    dtg.ItemsSource = (IEnumerable)result;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar(dtgPropietario);
            Cargar_Combobox();
        }
        private void Cargar_Combobox()
        {
            try
            {
                cmbProcedencia.ItemsSource = (IEnumerable)Enum.GetNames(typeof(Departamentos));
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
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
