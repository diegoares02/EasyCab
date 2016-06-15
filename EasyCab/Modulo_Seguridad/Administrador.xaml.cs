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
using System.Collections;
using System.Data;

namespace EasyCab.Modulo_Seguridad
{
    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Window
    {
        public enum Departamentos { LPZ,CBBA,SCZ,OR,POT,TRJ,BN,PND,CHQ}
        public Administrador()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar(dtgPropietario);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var administradores = new Usuario
                    {
                        IdUsuario=Convert.ToInt32(txtCI.Text),
                        Procedencia=cmbProcedencia.Text,
                        Nombre=txtNombre.Text,
                        Paterno=txtPaterno.Text,
                        Materno=txtMaterno.Text,
                        Direccion=txtDireccion.Text,
                        Telefono=Convert.ToInt32(txtTelefono.Text),
                        Celular=Convert.ToInt32(txtCelular.Text),
                        Email=txtEmail.Text,
                        Fech_Nac=dtpFechaNac.SelectedDate.Value,
                        Username=txtUsername.Text,
                        Password=txtPassword.Password,
                        IdPerfil=1,
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
        private void Cargar(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.Usuario
                                 where b.IdPerfil == 1
                                 select new { b.IdUsuario, b.Nombre, b.Paterno, b.Materno, b.Direccion, b.Telefono, b.Celular, b.Email, b.Fech_Nac };
                    dtg.ItemsSource = (IEnumerable)result;                    
                        cmbProcedencia.ItemsSource = (IEnumerable)Enum.GetNames(typeof(Departamentos));
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
                Limpiar();
            }
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
    }
}
