using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
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

namespace EasyCab.Modulo_Seguridad
{
    /// <summary>
    /// Lógica de interacción para Perfiles.xaml
    /// </summary>
    public partial class Perfiles : Window
    {
        string id;
        public Perfiles()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    Perfil administradores = new Perfil();
                    administradores.IdPerfil=Ultimo()+1;
                    administradores.Nombre=txtNombrePerfil.Text;

                    db.Perfil.InsertOnSubmit(administradores);
                    db.SubmitChanges();
                    Cargar(dtgPerfil);
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
                    if (Convert.ToInt32( id)!= 0)
                    {
                        var result = (from b in db.Perfil
                                      where b.IdPerfil == Convert.ToInt32(id)
                                      select b).First();
                        result.Nombre = txtNombrePerfil.Text;
                        db.SubmitChanges();
                        Cargar(dtgPerfil);
                    }
                    else { MessageBox.Show("Perfil inexistente"); }
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
                    var result = (from b in db.Perfil
                                  where b.Nombre == txtNombrePerfil.Text
                                  select b).First();
                    db.Perfil.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    Cargar(dtgPerfil);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar(dtgPerfil);
        }
        private void Cargar(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.Perfil
                                 select new { b.IdPerfil,b.Nombre};
                    dtg.ItemsSource = (IEnumerable)result;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int Ultimo()
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Perfil
                              select b.IdPerfil).Max();
                if (result != 0)
                    return result;
                else return 0;
            }
        }

        private void dtgPerfil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                id = ((TextBlock)dtgPerfil.CurrentColumn.GetCellContent(dtgPerfil.SelectedItem)).Text;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            id = Buscar(txtNombrePerfil.Text).ToString();
        }
        private int Buscar(string nombre)
        {
            if (txtNombrePerfil.Text != string.Empty)
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Perfil
                                  where b.Nombre == txtNombrePerfil.Text
                                  select b).First();
                    return result.IdPerfil;
                }
            }
            else { return 0; }
        }
    }
}
