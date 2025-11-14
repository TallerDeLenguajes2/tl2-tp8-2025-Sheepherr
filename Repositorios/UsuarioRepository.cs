namespace MVC.Repositorios;

using ClaseMVC.Models;
using Microsoft.Data.Sqlite;
using MVC.Interfaces;
using SQLitePCL;
public class UsuarioRepository : IUserRepository
{
    private string cadenaConexion = "Data Source= Db/Tienda.db";
    public Usuario GetUser(string usuario, string contraseña)
    {
       
        Usuario user = null;
        const string sql = @"
                            SELECT Id,Nombre,User,Pass,Rol
                            FROM Usuarios
                            WHERE User = @Usuario AND Pass = @Contraseña";
                        
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        using var comando = new SqliteCommand(sql,conexion);

        comando.Parameters.AddWithValue("@Usuario", usuario);
        comando.Parameters.AddWithValue("@Contraseña", contraseña);

        using var reader = comando.ExecuteReader();

        if (reader.Read())
        {
        // Si el lector encuentra una fila, el usuario existe y las credenciales son correctas
            user = new Usuario
            {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            User = reader.GetString(2),
            Pass = reader.GetString(3),
            Rol = reader.GetString(4)
            };
        }
        return user;
                    
    }
}