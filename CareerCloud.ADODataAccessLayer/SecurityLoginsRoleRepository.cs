using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                                   ([ID],[Login],[Role])
                         VALUES
                             (@Id,@Login,@Role)";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Role", item.Role);
                    
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id]
                ,[Login],[Role],[Time_Stamp]
                From [dbo].[Security_Logins_Roles]";

                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginsRolePoco[] appPocos = new SecurityLoginsRolePoco[5000];
                while (rdr.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.Role = rdr.GetGuid(2);
                    poco.TimeStamp = (byte[])(rdr.IsDBNull(3) ? null : rdr[3]);



                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = $"DELETE from Security_Logins_Roles WHERE ID= @ID";
                    cmd.Parameters.AddWithValue("ID", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                       SET [Login]= @Login
                            ,[Role]= @Role
                        WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Role", item.Role);
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }

        }
    }
 }

