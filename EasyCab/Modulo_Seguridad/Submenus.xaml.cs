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
    /// Lógica de interacción para Submenus.xaml
    /// </summary>
    public partial class Submenus : Window
    {
        public Submenus()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var administradores = new Submenu
                    {
                        IdSubmenu=Ultimo()+1,
                        Nombre = txtNombreMenu.Text,
                        IdMenu=BuscarM(cmbMenus.Text),
                    };
                    db.Submenu.InsertOnSubmit(administradores);
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
                    var result = (from b in db.Submenu
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
                    var result = (from b in db.Submenu
                                  where b.Nombre == txtNombreMenu.Text
                                  select b).First();
                    db.Submenu.DeleteOnSubmit(result);
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
                    var result = from b in db.Submenu
                                 select b;
                    dtg.ItemsSource = (IEnumerable)result;
                    var result1 = from b in db.MenusUsuario
                                 select b;
                    cmbMenus.ItemsSource = (IEnumerable)result1;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarM(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Submenu
                              where b.Nombre==nombre
                             select b).First();
                return result.IdMenu;
            }
        }
        private int Ultimo()
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Submenu
                              orderby b.IdSubmenu ascending
                              select b).First();
                return result.IdSubmenu;
            }
        }

        private void dtgMenus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                txtNombreMenu.Text=((DataRowView)dtgMenus.SelectedItem).Row["Nombre"].ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
