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
    /// Lógica de interacción para Empresas.xaml
    /// </summary>
    public partial class Empresas : Window
    {
        public Empresas()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    if (db.DatabaseExists())
                    {
                        var empresas = new Empresa
                            {
                                IdEmpresa = 0,
                                Nombre = txtNombre.Text,
                                IdUsuario = BuscarPropietario(cmbPropietario.Text),
                                CodParada = txtCodParada.Text,
                                NIT = Convert.ToInt32(txtNIT.Text),
                                CodATT = txtCodATT.Text,
                                Frecuencia = Convert.ToSingle(txtFrecuencia.Text)
                            };
                        db.Empresa.InsertOnSubmit(empresas);
                        db.SubmitChanges();
                        Cargar();
                    }
                    else { MessageBox.Show("No puedo acceder a la base de datos"); }
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
                    var result = (from b in db.Empresa
                                  where b.Nombre == txtNombre.Text
                                  select b).First();
                    result.Frecuencia = Convert.ToDouble(txtFrecuencia.Text);
                    db.SubmitChanges();
                    Cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Empresa
                                  where b.Nombre == txtNombre.Text
                                  select b).First();
                    db.Empresa.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    Cargar();
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
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.Empresa
                                 select new { b.IdEmpresa,b.IdUsuario,b.Nombre,b.CodParada,b.CodATT,b.Frecuencia};
                    if (result != null)
                        dtgEmpresas.ItemsSource = (IEnumerable)result;
                    else { MessageBox.Show("No se puede cargar"); }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void Cargar_Combobox()
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.Usuario
                                 where b.IdPerfil == 2
                                 select b.Nombre;
                    cmbPropietario.ItemsSource = (IEnumerable)result;
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarPropietario(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Usuario
                             where (b.Nombre == nombre) && (b.IdPerfil == 2)
                             select new { b.IdUsuario }).First();
                return result.IdUsuario;
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Empresa
                                  where b.Nombre == txtNombre.Text
                                  select b).FirstOrDefault();
                txtCodParada.Text = result.CodParada;
                txtCodATT.Text = result.CodATT;
                txtFrecuencia.Text = result.Frecuencia.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar();
            Cargar_Combobox();
        }
        private void Limpiar()
        {
            txtCodATT.Clear();
            txtCodParada.Clear();
            txtFrecuencia.Clear();
            txtNIT.Clear();
            txtNombre.Clear();
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

        private void txtNIT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtFrecuencia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }
    }
}
