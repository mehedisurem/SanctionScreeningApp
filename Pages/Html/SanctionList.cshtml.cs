using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Data;

namespace SanctionScreeningApp.Pages.Html
{
    public class SanctionListModel : PageModel
    {
        [BindProperty]
        public string Country { get; set; }

        public IActionResult OnGet()
        {
            // You can perform any necessary initialization logic here
            return Page();
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
            try
            {
                Console.WriteLine("OnPost method is called.");  // Add this line for testing

                Console.WriteLine($"Country: {Country}");

                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM SanctionInfo WHERE Country = @Country";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Country", Country);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Pass the DataTable to the view
                            ViewData["SanctionData"] = dataTable;

                            return Page();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return StatusCode(500); // Internal Server Error
            }
        }
    }
}
