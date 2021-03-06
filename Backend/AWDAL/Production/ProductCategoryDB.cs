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
    public class ProductCategoryDB
    {
        public static List<ProductCategory> Get()
        {
            List<ProductCategory> categories = new List<ProductCategory>();
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
                        CommandText = "select idcategoria, nombre, descripcion, (case when condicion = 1 then 'Activo' else 'Desactivado' end) as condicion from dbo.categoria WHERE condicion = 1"
                    };
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    var query = from item in dt.AsEnumerable()
                                select new ProductCategory
                                {
                                    IDCategoria = Convert.ToInt32(item["idcategoria"]),
                                    Nombre = Convert.ToString(item["nombre"]),
                                    Descripcion = Convert.ToString(item["descripcion"]),
                                    Condicion = Convert.ToString(item["condicion"])
                                };
                    categories = query.ToList();
                }
            }
            catch(Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return categories;
        }

        public static Result Insert(ProductCategory productCategory)
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
                        command.CommandText = "dbo.InsertCategory";
                        command.Parameters.AddWithValue("@Nombre", productCategory.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", productCategory.Descripcion);
                        command.Parameters.AddWithValue("@Condicion", 1);

                        command.Parameters.Add("@id_categoria", SqlDbType.Int);
                        command.Parameters["@id_categoria"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@id_categoria"].Value);
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

        public static Result Update(ProductCategory productCategory)
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
                        command.CommandText = "dbo.UpdateCategory";

                        command.Parameters.AddWithValue("@id_categoria", productCategory.IDCategoria);
                        command.Parameters.AddWithValue("@Nombre", productCategory.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", productCategory.Descripcion);
                        command.Parameters.AddWithValue("@Condicion", 1);

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@id_categoria"].Value);
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

        public static Result Delete(int IDCategoria)
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
                        command.CommandText = "dbo.DeactivateCategory";

                        command.Parameters.AddWithValue("@id_categoria", IDCategoria);

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@id_categoria"].Value);
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
