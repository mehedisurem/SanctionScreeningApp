using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SanctionScreeningApp.Pages.Html
{
    public class AccountModel : PageModel
    {
        public List<CustomerInfo> CustomerList { get; set; }

        public AccountModel()
        {
            CustomerList = new List<CustomerInfo>();
        }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo
                                {
                                    AccountNo = reader.GetInt64(0),
                                    EmailAddress = reader.GetString(1),
                                    FirstName = reader.GetString(2),
                                    LastName = reader.GetString(3),
                                    ContactNo = reader.GetString(4),
                                    NIDNo = reader.GetString(5),
                                    FathersName = reader.GetString(6),
                                    MothersName = reader.GetString(7),
                                    Address = reader.GetString(8),
                                    City = reader.GetString(9),
                                    Country = reader.GetString(10),
                                    PostalCode = reader.GetString(11),
                                    Gender = reader.GetString(12)
                                };
                                CustomerList.Add(customerInfo);
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

    public class CustomerInfo
    {
        public long AccountNo { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string NIDNo { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Gender { get; set; }
    }
}
