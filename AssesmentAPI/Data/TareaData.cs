using AssesmentAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace AssesmentAPI.Data
{
    public class TareaData
    {
        private readonly string conexion;

        public TareaData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionString");
        }
        public async Task<List<Tarea>> Listar()
        {
            List<Tarea> lista = new List<Tarea>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("ListarTarea", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Tarea
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]),
                            Estado = reader["Estado"].ToString(),
                            Prioridad = reader["Prioridad"].ToString(),
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Tarea> Obtener(int Id)
        {
            Tarea tarea = new();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("ObtenerTarea", con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]),
                            Estado = reader["Estado"].ToString(),
                            Prioridad = reader["Prioridad"].ToString(),
                        };
                    }
                }
            }
            return tarea;
        }

        public async Task<bool> Crear(Tarea tarea)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("InsertarTarea", con);
                cmd.Parameters.AddWithValue("@Nombre", tarea.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
                cmd.Parameters.AddWithValue("@FechaCreacion", tarea.FechaCreacion);
                cmd.Parameters.AddWithValue("@FechaVencimiento", tarea.FechaVencimiento);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado);
                cmd.Parameters.AddWithValue("@Prioridad", tarea.Prioridad);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Actualizar(Tarea tarea)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("ActualizarTarea", con);
                cmd.Parameters.AddWithValue("@Id", tarea.Id);
                cmd.Parameters.AddWithValue("@Nombre", tarea.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
                cmd.Parameters.AddWithValue("@FechaCreacion", tarea.FechaCreacion);
                cmd.Parameters.AddWithValue("@FechaVencimiento", tarea.FechaVencimiento);
                cmd.Parameters.AddWithValue("@Estado", tarea.Estado);
                cmd.Parameters.AddWithValue("@Prioridad", tarea.Prioridad);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("EliminarTarea", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
