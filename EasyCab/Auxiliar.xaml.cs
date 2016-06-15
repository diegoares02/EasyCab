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

namespace EasyCab
{
    /// <summary>
    /// Lógica de interacción para Auxiliar.xaml
    /// </summary>
    public partial class Auxiliar : Window
    {
        public Auxiliar()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Seguridad.Menus men = new Modulo_Seguridad.Menus();
            men.ShowDialog();
            men.Close();
        }

        private void btnSubmenu_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Seguridad.Submenus men = new Modulo_Seguridad.Submenus();
            men.ShowDialog();
            men.Close();
        }

        private void btnAdminnistrador_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Seguridad.Administrador ad = new Modulo_Seguridad.Administrador();
            ad.ShowDialog();
            ad.Close();
        }

        private void btnPerfil_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Seguridad.Perfiles per = new Modulo_Seguridad.Perfiles();
            per.ShowDialog();
            per.Close();
        }

        private void btnEmpresas_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Empresa.Empresas emp = new Modulo_Empresa.Empresas();
            emp.ShowDialog();
            emp.Close();
        }

        private void btnPropietarios_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Empresa.Propietarios p = new Modulo_Empresa.Propietarios();
            p.ShowDialog();
        }

        private void btnConductores_Click(object sender, RoutedEventArgs e)
        {
            Modulo_Personal.Conductores con = new Modulo_Personal.Conductores();
            con.ShowDialog();
            con.Close();
        }

        private void btnOperadores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVehiculos_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
