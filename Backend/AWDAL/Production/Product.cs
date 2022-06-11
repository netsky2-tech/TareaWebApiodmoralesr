using AWEntities.Production;
using AWEntities.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AWDAL.Production
{
    public class ProductDB
    {
        public static List<Product> Get()
        {
            List<Product> products = new List<Product>();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AwConnectionString"].ConnectionString))
                {
                    //connection.Open();
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandText = "SELECT articulo.idarticulo,articulo.codigo,articulo.nombre,articulo.descripcion,articulo.idcategoria,categoria.nombre AS Categoria,(case when dbo.articulo.condicion = 1 then 'Activo' else 'Desactivado' end) as condicion FROM articulo INNER JOIN categoria ON dbo.articulo.idcategoria = dbo.categoria.idcategoria where dbo.articulo.condicion = 1 order by articulo.nombre desc"
                    };
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    var query = from item in dt.AsEnumerable()
                                select new Product
                                {
                                    Idarticulo = Convert.ToInt32(item["idarticulo"]),
                                    Idcategoria = Convert.ToInt32(item["idcategoria"]),
                                    Codigo = Convert.ToString(item["codigo"]),
                                    Nombre = Convert.ToString(item["nombre"]),
                                    Descripcion = Convert.ToString(item["descripcion"]),
                                    Condicion = Convert.ToString(item["condicion"]),
                                    Categoria = Convert.ToString(item["Categoria"])
                                };
                    products = query.ToList();
                }
            }
            catch(Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return products;
        }

        public static Result Insert(Product productCategory)
        {
            Result r = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AwConnectionString"].ConnectionString))
                {
                    //connection.Open();
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "dbo.InsertarProducto";
                        command.Parameters.AddWithValue("@Codigo", productCategory.Codigo);
                        command.Parameters.AddWithValue("@Nombre", productCategory.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", productCategory.Descripcion);
                        command.Parameters.AddWithValue("@idcategoria", productCategory.Idcategoria);
                        command.Parameters.AddWithValue("@Condicion", 1);
                        command.Parameters.AddWithValue("@precio_venta", 1.0);
                        command.Parameters.AddWithValue("@stock", 1);

                        command.Parameters.Add("@idarticulo", SqlDbType.Int);
                        command.Parameters["@idarticulo"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@idcategoria"].Value);
                        r.Outcome = Convert.ToInt32(command.Parameters["@return"].Value) == 0;
                        r.Message = Convert.ToString(command.Parameters["@return_message"].Value);
                    };

                }
            }
            catch (Exception ex)
            {
                r.Id = 0;
                r.Outcome = false;
                r.Message = ex.Message;
            }

            return r;
        }

        public static Result Update(Product productCategory)
        {
            Result r = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AwConnectionString"].ConnectionString))
                {
                    //connection.Open();
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "dbo.ActualizarProducto";

                        command.Parameters.AddWithValue("@idarticulo", productCategory.Idarticulo);
                        command.Parameters.AddWithValue("@Codigo", productCategory.Codigo);
                        command.Parameters.AddWithValue("@Nombre", productCategory.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", productCategory.Descripcion);
                        command.Parameters.AddWithValue("@idcategoria", productCategory.Idcategoria);
                        command.Parameters.AddWithValue("@Condicion", 1);

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@idarticulo"].Value);
                        r.Outcome = Convert.ToInt32(command.Parameters["@return"].Value) == 0;
                        r.Message = Convert.ToString(command.Parameters["@return_message"].Value);
                    };

                }
            }
            catch (Exception ex)
            {
                r.Id = 0;
                r.Outcome = false;
                r.Message = ex.Message;
            }
            
            return r;
        }

        public static Result Delete(int IDarticulo)
        {
            Result r = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AwConnectionString"].ConnectionString))
                {
                    //connection.Open();
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "dbo.DesactivarProducto";

                        command.Parameters.AddWithValue("@idarticulo", IDarticulo);

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@idarticulo"].Value);
                        r.Outcome = Convert.ToInt32(command.Parameters["@return"].Value) == 0;
                        r.Message = Convert.ToString(command.Parameters["@return_message"].Value);
                    };

                }
            }
            catch (Exception ex)
            {
                r.Id = 0;
                r.Outcome = false;
                r.Message = ex.Message;
            }

            return r;
        }
    }
}
