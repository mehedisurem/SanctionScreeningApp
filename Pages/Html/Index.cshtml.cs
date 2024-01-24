using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SanctionScreeningApp.Pages.Html
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> UserList { get; set; }

        public IndexModel()
        {
            UserList = new List<UserInfo>();
        }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM [user]"; // Note: [user] is enclosed in square brackets as it is a reserved keyword in SQL
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo
                                {
                                    Id = reader.GetInt32(0).ToString(),
                                    UserName = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    UserType = reader.GetString(3),
                                    Branch = reader.GetString(4)
                                };
                                UserList.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Branch { get; set; }
    }
}
