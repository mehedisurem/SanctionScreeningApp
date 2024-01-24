using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Formats.Asn1.AsnWriter;

namespace SanctionScreeningApp.Pages.Html
{
    public class CustomerModel : PageModel
    {

            public string Cname { get; set; }
            public string Nid { get; set; }
            public string fname { get; set; }
            public DateTime Dob { get; set; }
            public string Mname { get; set; }
            public string AccountNo { get; set; }
            public string Etin { get; set; }
            public string PassportNo { get; set; }
            public string MobNo { get; set; }
            public string ScoreP { get; set; }


        // Existing model properties...

        // Add a property to hold search results
        public List<CustomerInfo> SearchResult { get; set; }
        [BindProperty]
        public CustomerInfo Customer { get; set; }

        // Your existing methods...

        public IActionResult OnPost()
        {
            // Access the form fields using Customer properties
            var customerName = Customer.CustomerName;
            var nidNo = Customer.NidNo;
            // ... other properties ...

            // Perform search logic using the entered values
            // ...

            return Page();
        }
        public void OnGet()
        {
            // Initialization logic if needed
        }

       /* public void OnPost()
        {
            // Call the search logic
            OnPostSearch();
        }*/

        public void OnPostSearch()
        {
            SearchResult = new List<CustomerInfo>();

            try
            {
                // Your existing connection string
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Construct the SQL query based on the non-empty input fields
                    string sql = "SELECT * FROM customer WHERE 1 = 1"; // Start with a true condition

                    // Add conditions based on non-empty input fields
                    if (!string.IsNullOrEmpty(Cname))
                        sql += " AND (FIRST_NAME + ' ' + LAST_NAME) LIKE @Cname";

                    if (!string.IsNullOrEmpty(Nid))
                        sql += " AND NID_NO = @Nid";

                    if (!string.IsNullOrEmpty(fname))
                        sql += " AND FATHERS_NAME = @fname";

                    if (Dob != default)
                        sql += " AND DateOfBirth = @Dob";

                    if (!string.IsNullOrEmpty(Mname))
                        sql += " AND MOTHERS_NAME = @Mname";

                    if (!string.IsNullOrEmpty(AccountNo))
                        sql += " AND ACCOUNT_NO = @AccountNo";

                    if (!string.IsNullOrEmpty(Etin))
                        sql += " AND ETIINNo = @Etin";

                    if (!string.IsNullOrEmpty(PassportNo))
                        sql += " AND PassportNo = @PassportNo";

                    if (!string.IsNullOrEmpty(MobNo))
                        sql += " AND CONTACT_NO = @MobNo";

                    // Note: Adjust the ScoreP field based on your actual matching criteria
                    if (!string.IsNullOrEmpty(ScoreP))
                        sql += " AND [YourMatchingField] = @ScoreP";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Cname", "%" + Cname + "%");
                        command.Parameters.AddWithValue("@Nid", Nid);
                        command.Parameters.AddWithValue("@fname", fname);
                        command.Parameters.AddWithValue("@Dob", Dob);
                        command.Parameters.AddWithValue("@Mname", Mname);
                        command.Parameters.AddWithValue("@AccountNo", AccountNo);
                        command.Parameters.AddWithValue("@Etin", Etin);
                        command.Parameters.AddWithValue("@PassportNo", PassportNo);
                        command.Parameters.AddWithValue("@MobNo", MobNo);
                        command.Parameters.AddWithValue("@ScoreP", ScoreP);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo
                                {
                                    // Populate the CustomerInfo object with the data from the database
                                    // Adjust the property names based on your database schema
                                };

                                SearchResult.Add(customerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
        }

        public class CustomerInfo
        {

            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? EmailAddress { get; set; }
            public string? ContactNo { get; set; }
            public string? NIDNo { get; set; }
            public string? FathersName { get; set; }
            public string? MothersName { get; set; }
            public string? NidNo { get; set; }
            public string? AccountNo { get; set; }
            public string? EtinNo { get; set; }
            public string? PassportNo { get; set; }
            public string? MobileNo { get; set; }
            public decimal? ScorePercentage { get; set; }
            public string? CustomerName { get; set; }
        }
    }
}
