using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.DAL
{
    public class UsuarioRepository
    {
        private string connectionString = "Data Source=FRANK\\SQLEXPRESS;Initial Catalog=SistemaUsuariosDB;User ID=sa;Password=123456;";

        public void AgregarUsuario(Usuario usuario)
        {
            EjecutarSP("INSERTAR", usuario);
        }

        public void ModificarUsuario(Usuario usuario)
        {
            EjecutarSP("MODIFICAR", usuario);
        }

        public void EliminarUsuario(int id)
        {
            EjecutarSP("ELIMINAR", new Usuario { Id = id });
        }

        public List<Usuario> ObtenerUsuarios()
        {
            var lista = new List<Usuario>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Usuario_CRUD", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "CONSULTAR");

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            Sexo = reader["Sexo"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        private void EjecutarSP(string accion, Usuario usuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Usuario_CRUD", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento == default ? (object)DBNull.Value : usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo ?? (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
