namespace MVC.Repositorios;

using Microsoft.Data.Sqlite;
using ClaseMVC.Models;
using SQLitePCL;
using MVC.Interfaces;
public class PresupuestosRepository : IPresupuestosRepository
{
    private string cadenaConexion = "Data Source= Db/Tienda.db";

    public void InsertPresupuesto(Presupuestos presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombreDestinatario, @fechaCreacion)";
        using var comando = new SqliteCommand(query, conexion);

        comando.Parameters.Add(new SqliteParameter("@nombreDestinatario", presupuesto.nombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@fechaCreacion", presupuesto.FechaCreacion));

        comando.ExecuteNonQuery();
    }
    
    public List<Presupuestos> GetAllPresupuestos()
    {
        List<Presupuestos> presupuestos = new List<Presupuestos>();
        string query = "SELECT * FROM Presupuestos WHERE Activo = 1";
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        var comdPre = new SqliteCommand(query, conexion);
        using (SqliteDataReader reader = comdPre.ExecuteReader())
        {
            while (reader.Read())
            {
                var presupuesto = new Presupuestos
                {
                    IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                    nombreDestinatario = reader["NombreDestinatario"].ToString(),
                    FechaCreacion = reader["FechaCreacion"].ToString(),
                    detalle = new List<PresupuestoDetalle>()
                };
                presupuestos.Add(presupuesto);
            }
        }

        return presupuestos;
    }
    
    public Presupuestos GetbyIdPresupuesto(int id)
    {
        Presupuestos presupuesto = null;
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string query = "SELECT p.NombreDestinatario AS NombreDestinatario, p.FechaCreacion AS FechaCreacion, pr.Descripcion AS Descripcion, d.Cantidad AS Cantidad, pr.Precio AS Precio, pr.idProducto AS idProducto FROM Presupuestos p LEFT JOIN PresupuestosDetalle d ON p.idPresupuesto = d.idPresupuesto LEFT JOIN Productos pr ON d.idProducto = pr.idProducto WHERE p.idPresupuesto = @id";

        using var comando = new SqliteCommand(query, conexion);

        comando.Parameters.Add(new SqliteParameter("@id", id));

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            if (presupuesto == null)
            {
                presupuesto = new Presupuestos();
                presupuesto.IdPresupuesto = id;
                presupuesto.nombreDestinatario = reader["NombreDestinatario"].ToString();
                presupuesto.FechaCreacion = reader["FechaCreacion"].ToString();
                presupuesto.detalle = new List<PresupuestoDetalle>();
            }
            
            if (reader["idProducto"] == DBNull.Value)
            continue;

            var producto = new Productos
            {
                idProducto = Convert.ToInt32(reader["idProducto"]),
                descripcion = reader["Descripcion"].ToString(),
                precio = Convert.ToInt32(reader["Precio"])
            };
            var detalle = new PresupuestoDetalle
            {
                producto = producto,
                cantidad = Convert.ToInt32(reader["Cantidad"])
            };
            presupuesto.detalle.Add(detalle);

        }

        return presupuesto;
    }
    public void DeletePresupuesto(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "UPDATE Presupuestos SET Activo = 0 WHERE idPresupuesto = @id";

        using (var comando = new SqliteCommand(query, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();
        }

    }
    public void UpdatePresupuesto(Presupuestos presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string query = "UPDATE Presupuestos SET NombreDestinatario = @nombre , FechaCreacion = @fecha WHERE idPresupuesto = @idPresupuesto";

        using var command = new SqliteCommand(query, conexion);
        command.Parameters.Add(new SqliteParameter("@idPresupuesto", presupuesto.IdPresupuesto));
        command.Parameters.Add(new SqliteParameter("@nombre", presupuesto.nombreDestinatario));
        command.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion)); 
        command.ExecuteNonQuery();
    }
    public void UpdateDetalles(int idPresupuesto, int idProducto, int cantidad)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "UPDATE PresupuestosDetalle SET Cantidad = @cantidad WHERE idPresupuesto = @idPresupuesto AND idProducto = @idProducto";
        using var command = new SqliteCommand(query, conexion);
        command.Parameters.Add(new SqliteParameter("@cantidad", cantidad));
        command.Parameters.Add(new SqliteParameter("idPresupuesto", idPresupuesto));
        command.Parameters.Add(new SqliteParameter("idProducto", idProducto));
        command.ExecuteNonQuery();
    }
    public void DeleteDetails(int idPresupuesto, int idProducto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string query = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @idPresupuesto AND idProducto = @idProducto";

        using var command = new SqliteCommand(query, conexion);
        command.Parameters.Add(new SqliteParameter("idPresupuesto", idPresupuesto));
        command.Parameters.Add(new SqliteParameter("idProducto", idProducto));
        command.ExecuteNonQuery();
    }
    public void AddDetalle (int idPresupuesto, int idProducto, int cantidad)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @Cantidad)";
        using var comando = new SqliteCommand(query, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
        comando.Parameters.Add(new SqliteParameter("@Cantidad", cantidad));

        comando.ExecuteNonQuery();
    }
}