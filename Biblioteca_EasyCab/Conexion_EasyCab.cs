using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Biblioteca_EasyCab
{
    public class Conexion_EasyCab
    {
        string cadena = @"Data Source=(LocalDB)\v11.0;" +
               @"AttachDbFilename=|DataDirectory|\EasyCabBD.mdf;
                Integrated Security=True";
        public SqlConnection Conexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
