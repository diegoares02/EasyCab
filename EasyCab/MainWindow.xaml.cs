using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Markup;

namespace EasyCab
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }     

        private void menuEmpresa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modulo_Empresa.Empresas emp = new Modulo_Empresa.Empresas();
                emp.Owner = this;
                emp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuPropietario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modulo_Empresa.Propietarios propi = new Modulo_Empresa.Propietarios();
                propi.Owner = this;
                propi.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuSucursal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modulo_Empresa.Sucursales sucu = new Modulo_Empresa.Sucursales();
                sucu.Owner = this;
                sucu.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuAdministrador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modulo_Seguridad.Administrador admin = new Modulo_Seguridad.Administrador();
                admin.Owner = this;
                admin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuPerfil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modulo_Seguridad.Perfiles per = new Modulo_Seguridad.Perfiles();
                per.Owner = this;
                per.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
