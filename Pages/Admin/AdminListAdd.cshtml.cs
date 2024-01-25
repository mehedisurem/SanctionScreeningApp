using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace SanctionScreeningApp.Pages.Admin
{
    public class AdminListModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AdminListModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string Type { get; set; }

        [BindProperty]
        public string Programs { get; set; }

        public void OnGet()
        {
            // Initialization logic if needed
        }
        public IActionResult OnGetLogout()
        {
            // Implement any necessary logic to clear or sign out the user session
            // For example, you can use the following line to sign out the user:
            // HttpContext.SignOutAsync();

            // Redirect to the login page or any other page after logout
            return RedirectToPage("/Login");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Save data to the database
                try
                {
                    string connectionString = _configuration.GetConnectionString("DefaultConnection");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string sql = "INSERT INTO SanctionInfo (Name, Address, Type, Programs, Country) VALUES (@Name, @Address, @Type, @Programs, @Country)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@Address", Address);
                            command.Parameters.AddWithValue("@Type", Type);
                            command.Parameters.AddWithValue("@Programs", Programs);
                            command.Parameters.AddWithValue("@Country", "Bangladesh"); // Assuming Country is always Bangladesh

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Data inserted successfully
                                Console.WriteLine("Data inserted successfully!");
                            }
                            else
                            {
                                // Handle the case where no rows were affected (insertion failed)
                                Console.WriteLine("Data insertion failed!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return Page();
        }
    }
}
