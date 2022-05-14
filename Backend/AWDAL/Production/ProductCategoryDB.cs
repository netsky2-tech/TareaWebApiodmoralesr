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
                        CommandText = "SELECT * FROM Production.ProductCategory"
                    };
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    var query = from item in dt.AsEnumerable()
                                select new ProductCategory
                                {
                                    ProductCategoryID = Convert.ToInt32(item["ProductCategoryID"]),
                                    Name = Convert.ToString(item["Name"]),
                                    rowguid = new Guid(Convert.ToString(item["rowguid"])),
                                    ModifiedDate = Convert.ToDateTime(item["ModifiedDate"])
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
                        command.CommandText = "Production.InsertProductCategory";
                        command.Parameters.AddWithValue("@Name", productCategory.Name);

                        command.Parameters.Add("@ProductCategoryID", SqlDbType.Int);
                        command.Parameters["@ProductCategoryID"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@ProductCategoryID"].Value);
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
                        command.CommandText = "Production.UpdateProductCategory";

                        command.Parameters.AddWithValue("@ProductCategoryID", productCategory.ProductCategoryID);
                        command.Parameters.AddWithValue("@Name", productCategory.Name);

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@ProductCategoryID"].Value);
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

        public static Result Delete(int ProductCategoryID)
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
                        command.CommandText = "Production.DeleteProductCategory";

                        command.Parameters.AddWithValue("@ProductCategoryID", ProductCategoryID);

                        command.Parameters.Add("@return", SqlDbType.TinyInt);
                        command.Parameters["@return"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@return_message", SqlDbType.VarChar, 500);
                        command.Parameters["@return_message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        r.Id = Convert.ToInt32(command.Parameters["@ProductCategoryID"].Value);
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
