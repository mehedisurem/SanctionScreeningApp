using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SanctionScreeningApp.Pages
{
    public class IndexModel : PageModel
    {
        /*[BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM [User] WHERE UserName = @UserName AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // User authenticated successfully
                                // You can set any necessary session or authentication information here
                                return RedirectToPage("/Html/Dashboard"); // Redirect to the Dashboard page
                            }
                            else
                            {
                                // Invalid credentials
                                ErrorMessage = "Invalid UserName/Password!!";
                                return Page(); // Stay on the login page with an error message
                            }
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
    }*/
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            try
            {
                Console.WriteLine("OnPost method is called.");  // Add this line for testing

                Console.WriteLine($"UserName: {UserName}, Password: {Password}");

                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM [User] WHERE UserName = @UserName AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // User authenticated successfully
                                string userType = reader["UserType"].ToString();

                                // Redirect based on UserType
                                if (userType == "BranchUser")
                                {
                                    return RedirectToPage("/Html/Index");
                                }
                                else if (userType == "Admin")
                                {
                                    return RedirectToPage("/Admin/AdminHome");
                                }
                                else if (userType == "BAMLCO")
                                {
                                    return RedirectToPage("/Manager/BMLDashboard");
                                }
                                else
                                {
                                    // Handle other user types as needed
                                    ErrorMessage = "Invalid UserType";
                                    return Page();
                                }
                            }
                            else
                            {
                                // Invalid credentials
                                ErrorMessage = "Invalid UserName/Password!!";
                                return Page(); // Stay on the login page with an error message
                            }
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
