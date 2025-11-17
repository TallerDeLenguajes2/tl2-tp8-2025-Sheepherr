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
    public void InsertPresupuestoDetalle(int id, PresupuestoDetalle presupuestoDetalle)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @Cantidad)";
        using var comando = new SqliteCommand(query, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", id));
        comando.Parameters.Add(new SqliteParameter("@idProducto", presupuestoDetalle.producto.idProducto));
        comando.Parameters.Add(new SqliteParameter("@Cantidad", presupuestoDetalle.cantidad));

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

        /*query = "SELECT p.idProducto AS idProducto, p.Descripcion AS Descripcion, p.Precio AS Precio, d.Cantidad AS Cantidad FROM PresupuestosDetalle d INNER JOIN Productos p ON d.idProducto = p.idProducto WHERE d.idPresupuesto = @id";
        foreach (var pres in presupuestos)
        {
            using var comanDet = new SqliteCommand(query, conexion);

            comanDet.Parameters.Add(new SqliteParameter("@id", pres.IdPresupuesto));

            using (SqliteDataReader reader = comanDet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var producto = new Productos
                        {
                            idProducto = Convert.ToInt32(reader["idProducto"]),
                            descripcion = reader["Descripcion"].ToString(),
                            precio = Convert.ToInt32(reader["Precio"])
                        };

                        var pDetalle = new PresupuestoDetalle
                        {
                            producto = producto,
                            cantidad = Convert.ToInt32(reader["Cantidad"])
                        };

                        pres.detalle.Add(pDetalle);
                    }
                }
        }*/

        return presupuestos;
    }
    /*public List<Presupuestos> GetAllPresupuestos1 ()
    {
        var presupuestos = new List<Presupuestos>();

        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string query = "SELECT p.idPresupuesto AS idPresupuesto, p.NombreDestinatario AS NombreDestinatario, p.FechaCreacion AS FechaCreacion, d.idProducto AS idProducto, pr.Descripcion AS Descripcion, pr.Precio AS Precio, d.Cantidad AS Cantidad FROM Presupuestos p LEFT JOIN PresupuestosDetalle d ON p.idPresupuesto = d.idPresupuesto LEFT JOIN Productos pr ON d.idProducto = pr.idProducto ORDER BY p.idPresupuesto";
        
        using var comando = new SqliteCommand(query, conexion);
        using var reader = comando.ExecuteReader();

        Presupuestos presupuesto = null;
        int?presupuestoId = null;

        while (reader.Read())
        {
            int idPres = reader["idPresupuesto"] != DBNull.Value ? Convert.ToInt32(reader["idPresupuesto"]) : 0;
            
            if (presupuesto == null || presupuestoId != idPres)
            {
                presupuesto = new Presupuestos
                {
                    IdPresupuesto = idPres,
                    nombreDestinatario = reader["NombreDestinatario"]?.ToString(),
                    FechaCreacion = reader["FechaCreacion"]?.ToString(),
                    detalle = new List<PresupuestoDetalle>()
                };
                presupuestos.Add(presupuesto);
                presupuestoId = idPres;
            }
            if (reader["idProducto"] != DBNull.Value)
            {
                var producto = new Productos
                {
                    idProducto = Convert.ToInt32(reader["idProducto"]),
                    descripcion = reader["Descripcion"]?.ToString(),
                    precio = Convert.ToInt32(reader["Precio"])
                };
                var detalle = new PresupuestoDetalle
                {
                    producto = producto,
                    cantidad = Convert.ToInt32(reader["Cantidad"])
                };
                presupuesto.detalle.Add(detalle);
            }
            
        }
        return presupuestos;
    }*/
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

}