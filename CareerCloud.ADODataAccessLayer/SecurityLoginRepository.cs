using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
         public void Add(params SecurityLoginPoco[] items)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (SecurityLoginPoco item in items)
                    {
                        cmd.CommandText = @"Insert into [dbo].[Security_Logins]
                    ([Id],[Login],[Password],[Created_Date],[Password_Update_Date]
                        ,[Agreement_Accepted_Date],[Is_Locked],[Is_Inactive]
                        ,[Email_Address],[Phone_Number],[Full_Name]
                        ,[Force_Change_Password],[Prefferred_Language])

                    Values (@Id,@Login,@Password,@Created_Date,@Password_Update_Date,
                    @Agreement_Accepted_Date,@Is_Locked,@Is_Inactive,@Email_Address,@Phone_Number,
                    @Full_Name,@Force_Change_Password,@Prefferred_Language)";

                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Login", item.Login);
                        cmd.Parameters.AddWithValue("@Password", item.Password);
                        cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                        cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                        cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                        cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                        cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                        cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                        cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                        cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id],
                [Login],[Password],[Created_Date],
                [Password_Update_Date],[Agreement_Accepted_Date],[Is_Locked],[Is_Inactive],
                [Email_Address],[Phone_Number],[Full_Name],[Force_Change_Password],
                [Prefferred_Language],[Time_Stamp]
                From [dbo].[Security_Logins]";

                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginPoco[] appPocos = new SecurityLoginPoco[5000];
                while (rdr.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetString(1);
                    poco.Password = rdr.GetString(2);
                    poco.Created = rdr.GetDateTime(3);
                    poco.PasswordUpdate = (DateTime?)(rdr.IsDBNull(4) ? null : rdr[4]);
                    poco.AgreementAccepted = (DateTime?)(rdr.IsDBNull(5) ? null : rdr[5]);
                    poco.IsLocked = rdr.GetBoolean(6);
                    poco.IsInactive = rdr.GetBoolean(7);
                    poco.EmailAddress = (String)(rdr.IsDBNull(8) ? null : rdr[8]);
                    poco.PhoneNumber = (String)(rdr.IsDBNull(9) ? null : rdr[9]);
                    poco.FullName = (String)(rdr.IsDBNull(10) ? null : rdr[10]);
                    poco.ForceChangePassword = rdr.GetBoolean(11);
                    poco.PrefferredLanguage = (String)(rdr.IsDBNull(12) ? null : rdr[12]);
                    poco.TimeStamp = (byte[])rdr[13];

                     appPocos[x] = poco;
                     x++;
                 }
                 return appPocos.Where(a => a != null).ToList();
             }

                }

                public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = $"Delete From Security_Logins where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"Update [dbo].[Security_Logins] 
                    set [Login] = @Login,
                    [Password] = @Password,
                    [Created_Date] = @Created_Date,
                    [Password_Update_Date] = @Password_Update_Date,
                    [Agreement_Accepted_Date] = @Agreement_Accepted_Date,
                    [Is_Locked] = @Is_Locked,
                    [Is_Inactive] = @Is_Inactive,
                    [Email_Address] = @Email_Address,
                    [Phone_Number] = @Phone_Number,
                    [Full_Name] = @Full_Name,
                    [Force_Change_Password] = @Force_Change_Password,
                    [Prefferred_Language] = @Prefferred_Language
                    where [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}
