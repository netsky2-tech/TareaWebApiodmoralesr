using AWEntities.Authentication;
using AWEntities.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWDAL.Admon
{
    public class AuthenticationDB
    {
        public static Result Login(user User)
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
                        command.CommandText = "dbo.LoginUser";
                        command.Parameters.AddWithValue("@username", User.username);
                        command.Parameters.AddWithValue("@PasswordHash", User.PasswordHash);
                        command.Parameters.AddWithValue("@PasswordSalt", User.PasswordSalt);

            
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
            catch(Exception ex)
            {
                r.Id = 0;
                r.Outcome = false;
                r.Message = ex.Message;
            }

            return r;
        }
    }
}
