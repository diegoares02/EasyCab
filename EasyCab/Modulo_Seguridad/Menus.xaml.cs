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

namespace EasyCab.Modulo_Seguridad
{
    /// <summary>
    /// Lógica de interacción para Menus.xaml
    /// </summary>
    public partial class Menus : Window
    {
        public Menus()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var administradores = new MenusUsuario
                    {
                        IdMenu=Ultimo()+1,
                        Nombre=txtNombreMenu.Text,
                    };
                    db.MenusUsuario.InsertOnSubmit(administradores);
                    db.SubmitChanges();
                    Cargar(dtgMenus);
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
                    var result = (from b in db.MenusUsuario
                                  where b.Nombre == txtNombreMenu.Text
                                  select b).First();
                    result.Nombre = txtNombreMenu.Text;
                    db.SubmitChanges();
                    Cargar(dtgMenus);
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
                    var result = (from b in db.MenusUsuario
                                  where b.Nombre == txtNombreMenu.Text
                                  select b).First();
                    db.MenusUsuario.DeleteOnSubmit(result);
                    db.SubmitChanges();
                    Cargar(dtgMenus);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargar(dtgMenus);
        }
        private void Cargar(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.MenusUsuario
                                 select b;
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
                var result = (from b in db.MenusUsuario
                              orderby b.IdMenu descending
                              select b).First();
                return result.IdMenu;
            }
        }

        private void dtgMenus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                txtNombreMenu.Text = ((DataRowView)dtgMenus.SelectedItem).Row["Nombre"].ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
