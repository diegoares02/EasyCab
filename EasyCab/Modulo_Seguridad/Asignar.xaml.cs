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

namespace EasyCab.Modulo_Seguridad
{
    /// <summary>
    /// Lógica de interacción para Asignar.xaml
    /// </summary>
    public partial class Asignar : Window
    {
        public Asignar()
        {
            InitializeComponent();
        }

        private void btnAceptarMenus_Click(object sender, RoutedEventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnAceptarPermisos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var permisos = new Permiso
                        {
                            IdUsuario=BuscarU(cmbUsuario.Text),
                            Agregar=chkAgregar.IsChecked,
                            Modificar=chkModificar.IsChecked,
                            Eliminar=chkEliminar.IsChecked,
                            Reporte=chkReporte.IsChecked
                        };
                    db.Permiso.InsertOnSubmit(permisos);
                    db.SubmitChanges();
                    CargarPU(dtgPU);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarMP(dtgMP);
            CargarPU(dtgPU);
        }
        private void CargarMP(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.SubmenuPerfil
                                 from c in db.Perfil
                                 from t in db.Submenu
                                 where b.IdPerfil == c.IdPerfil && b.IdSubmenu == t.IdSubmenu                                 
                                 select c.Nombre;
                    

                    dtg.ItemsSource = (IEnumerable)result;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void CargarPU(DataGrid dtg)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = from b in db.Permiso
                                 select b;
                    dtg.ItemsSource = (IEnumerable)result;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarP(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Perfil
                              where b.Nombre == nombre
                              select b).First();
                return result.IdPerfil;
            }            
        }
        private int BuscarS(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Submenu
                              where b.Nombre == nombre
                              select b).First();
                return result.IdSubmenu;
            }            
        }
        private int BuscarU(string nombre)
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Usuario
                              where b.Nombre == nombre
                              select b).First();
                return result.IdUsuario;
            }            
        }
    }
}
