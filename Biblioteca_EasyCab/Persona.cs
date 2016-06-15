using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Biblioteca_EasyCab
{    
    public class Persona
    {
        public int PersonaId { get; set; }
        public string Procedencia { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Celular { get; set; }
        public string Email { get; set; }
        public string Fecha_Nac { get; set; }
        public byte[] Fotografia { get; set; }
        string cadena = @"Data Source=(LocalDB)\v11.0;" +
                @"AttachDbFilename=D:\Taller de programacion 2\EasyCab\EasyCab\Database\EasyCabBD.mdf;
                Integrated Security=True";
        public Persona()
        { }
        public Persona(int ci, string proc, string nom, string pat, string mat, string dir, int tel, int cel, string email, string fech, byte[] foto)
        {
            this.PersonaId = ci;
            this.Procedencia = proc;
            this.Nombre = nom;
            this.Paterno = pat;
            this.Materno = mat;
            this.Direccion = dir;
            this.Telefono = tel;
            this.Celular = cel;
            this.Email = email;
            this.Fecha_Nac = fech;
            this.Fotografia = null;
        }
        public void Agregar(Persona per)
        {
            try
            {
                string comando = @"INSERT INTO Persona VALUES(" + per.PersonaId.ToString() + ",'" + per.Procedencia + "','" + per.Nombre + "','" + per.Paterno +
                "','" + per.Materno + "','" + per.Direccion + "'," + per.Telefono.ToString() + "," + per.Celular.ToString() + ",'" + per.Email + "','"
                +Convert.ToDateTime(per.Fecha_Nac) + "'," + "NULL" + ")";
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery();  
            }
            catch (Exception ex)
            { throw ex; }            
        }
        public void Modificar(int id,int tel)
        {
            try
            {
                string comando = @"UPDATE Persona SET Telefono ="+tel.ToString()+" WHERE Id_Persona ="+id.ToString();
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void Eliminar(string nom)
        {
            try
            {
                string comando = @"DELETE FROM Persona WHERE Nombre = '"+nom+"'";
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            { throw ex; }
        }
        public System.Data.DataView Cargar(int perfil)
        {
            try
            {
                string comando = @"SELECT * FROM Persona WHERE Id_Persona IN (SELECT Id_Persona FROM Usuario WHERE Id_Perfil ="+perfil.ToString()+")";
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable ds = new System.Data.DataTable();
                da.Fill(ds);
                return ds.DefaultView;
            }
            catch (Exception ex)
            { throw ex; }
        }
        int Ultimo()
        {
            try
            {
                string comando = @"SELECT TOP 1 Id_Persona FROM Persona ORDER BY Id_Persona DESC";
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery();
                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Usuario");
                DataRow dr = ds.Tables["Usuario"].Rows[0];
                return Convert.ToInt32(dr["Id_Usuario"]);
            }
            catch (Exception ex)
            { throw ex; }           
        }
    }
}
