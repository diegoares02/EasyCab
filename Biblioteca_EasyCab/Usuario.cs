using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Biblioteca_EasyCab
{
    public class Usuario:Persona
    {
        public int Id_Usuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id_Perfil { get; set; }
        string cadena = @"Data Source=(LocalDB)\v11.0;" +
                @"AttachDbFilename=D:\Taller de programacion 2\EasyCab\EasyCab\Database\EasyCabBD.mdf;
                Integrated Security=True";
        public Usuario()
        { }
        public Usuario(string user,string pass,int idperfil,int idpersona)
        {
            this.Id_Usuario = Ultimo()+1;
            this.Username = user;
            this.Password = pass;
            this.Id_Perfil = idperfil;
            this.PersonaId = idpersona;
        }
        public void Agregar(Usuario per)
        {
            try
            {
                string comando = @"INSERT INTO Usuario VALUES(" + per.Id_Usuario.ToString() + ",'" + per.Username + "','" + per.Password + "',"+per.Id_Perfil.ToString()+","+per.PersonaId.ToString()+")";
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void Modificar(int id, string pass)
        {
            try
            {
                string comando = @"UPDATE Usuario SET Password =" + pass + " WHERE Id_Usuario =" + id.ToString();
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
                string comando = @"DELETE FROM Usuario WHERE Username = '" + nom + "'";
                SqlConnection con = new SqlConnection(cadena);
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { throw ex; }
        }
        int Ultimo()
        {
            try
            {
                string comando = @"SELECT TOP 1 Id_Usuario FROM Usuario ORDER BY Id_Usuario DESC";
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
