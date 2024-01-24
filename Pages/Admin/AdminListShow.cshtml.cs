using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SanctionScreeningApp.Pages.Admin
{
    public class AdminListShowModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AdminListShowModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Add a property to hold the data
        public List<SanctionInfoLists> SanctionInfos { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                // Fetch data from the database and assign it to SanctionInfos
                SanctionInfos = GetDataFromDatabase();
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return StatusCode(500); // Internal Server Error
            }
        }

        private List<SanctionInfoLists> GetDataFromDatabase()
        {
            List<SanctionInfoLists> sanctionInfos = new List<SanctionInfoLists>();

            // Your database connection and query logic here
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM SanctionInfo", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Populate your model with data from the database
                            SanctionInfoLists sanctionInfo = new SanctionInfoLists
                            {
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Type = reader["Type"].ToString(),
                                Programs = reader["Programs"].ToString(),
                                Country = reader["Country"].ToString(),
                            };

                            sanctionInfos.Add(sanctionInfo);
                        }
                    }
                }
            }

            return sanctionInfos;
        }
    }

    // Add this class
    public class SanctionInfoLists
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Programs { get; set; }
        public string Country { get; set; }
    }
}
