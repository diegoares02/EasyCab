using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DCEasyCabBDDataContext())
                {
                    var result = (from b in db.Usuario
                                 where b.Username == txtUsername.Text && b.Password == txtPassword.Password
                                 select b.IdPerfil).FirstOrDefault();
                    if (result == 1)
                    {
                        MainWindow admin = new MainWindow();
                        this.Hide();
                        admin.ShowDialog();
                        this.Close();                        
                    }
                    if (result == 2)
                    {
                        MainPropietario propi = new MainPropietario();
                        this.Hide();
                        propi.id = BuscarU(txtUsername.Text);
                        propi.ShowDialog();
                        this.Close();                        
                    }
                    if (result == 3)
                    {
                        MainOperador op = new MainOperador();
                        this.Hide();
                        op.Id = BuscarU(txtUsername.Text);
                        op.ShowDialog();
                        this.Close();                        
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarU(string user)
        {
            using (var db =new DCEasyCabBDDataContext())
            {
                var result = (from b in db.Usuario
                             where b.Username == user
                              select b.IdUsuario).FirstOrDefault();
                return result;
            }
        }
    }
}
