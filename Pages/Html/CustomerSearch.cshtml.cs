using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SanctionScreeningApp.Pages.Html
{
    public class CustomerSearchModel : PageModel
    {
        [BindProperty]
        public List<CustomerInfo> SearchResult { get; set; }

        // Add other necessary properties for input fields
        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public string NidNo { get; set; }
        [BindProperty]
        public string FathersName { get; set; }
        [BindProperty]
        public DateTime Dob { get; set; }
        [BindProperty]
        public string MothersName { get; set; }
        [BindProperty]
        public string AccountNo { get; set; }
        [BindProperty]
        public string EtinNo { get; set; }
        [BindProperty]
        public string PassportNo { get; set; }
        [BindProperty]
        public string MobileNo { get; set; }
        [BindProperty]
        public string ScorePercentage { get; set; }

        /*public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                SearchCustomers();
            }
            return Page();
        }*/
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Print input values from the HTML form
                Console.WriteLine($"CustomerName: {CustomerName}, NidNo: {NidNo}, FathersName: {FathersName}, ...");

                // Call the method to search customers
                SearchCustomers();
            }
            return Page();
        }
        public void OnGet()
        {
            // Initialization logic if needed
        }

        public void SearchCustomers()
        {
            SearchResult = new List<CustomerInfo>();

            try
            {
                // Your existing connection string
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=sanctiondb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Construct the SQL query based on the non-empty input fields
                    StringBuilder sqlBuilder = new StringBuilder("SELECT * FROM [customer] WHERE 1 = 1");

                    // Add conditions based on non-empty input fields
                    if (!string.IsNullOrEmpty(CustomerName))
                        sqlBuilder.Append(" AND (FIRST_NAME + ' ' + LAST_NAME) LIKE @CustomerName");

                    // Add other conditions for the remaining input fields
                    if (!string.IsNullOrEmpty(NidNo))
                        sqlBuilder.Append(" AND NID_NO = @NidNo");

                    if (!string.IsNullOrEmpty(FathersName))
                        sqlBuilder.Append(" AND FATHERS_NAME LIKE @FathersName");

                    if (Dob != default)
                        sqlBuilder.Append(" AND DateOfBirth = @Dob");

                    if (!string.IsNullOrEmpty(MothersName))
                        sqlBuilder.Append(" AND MOTHERS_NAME LIKE @MothersName");

                    if (!string.IsNullOrEmpty(AccountNo))
                        sqlBuilder.Append(" AND ACCOUNT_NO = @AccountNo");

                    if (!string.IsNullOrEmpty(EtinNo))
                        sqlBuilder.Append(" AND ETIINNo = @EtinNo");

                    if (!string.IsNullOrEmpty(PassportNo))
                        sqlBuilder.Append(" AND PassportNo = @PassportNo");

                    if (!string.IsNullOrEmpty(MobileNo))
                        sqlBuilder.Append(" AND CONTACT_NO = @MobileNo");

                    if (!string.IsNullOrEmpty(ScorePercentage))
                        sqlBuilder.Append(" AND [YourMatchingField] = @ScorePercentage");

                    using (SqlCommand command = new SqlCommand(sqlBuilder.ToString(), connection))
                    {
                        // Add parameters to prevent SQL injection
                        if (!string.IsNullOrEmpty(CustomerName))
                            command.Parameters.AddWithValue("@CustomerName", "%" + CustomerName + "%");

                        if (!string.IsNullOrEmpty(NidNo))
                            command.Parameters.AddWithValue("@NidNo", NidNo);

                        if (!string.IsNullOrEmpty(FathersName))
                            command.Parameters.AddWithValue("@FathersName", "%" + FathersName + "%");

                        if (Dob != default)
                            command.Parameters.AddWithValue("@Dob", Dob);

                        if (!string.IsNullOrEmpty(MothersName))
                            command.Parameters.AddWithValue("@MothersName", "%" + MothersName + "%");

                        if (!string.IsNullOrEmpty(AccountNo))
                            command.Parameters.AddWithValue("@AccountNo", AccountNo);

                        if (!string.IsNullOrEmpty(EtinNo))
                            command.Parameters.AddWithValue("@EtinNo", EtinNo);

                        if (!string.IsNullOrEmpty(PassportNo))
                            command.Parameters.AddWithValue("@PassportNo", PassportNo);

                        if (!string.IsNullOrEmpty(MobileNo))
                            command.Parameters.AddWithValue("@MobileNo", MobileNo);

                        if (!string.IsNullOrEmpty(ScorePercentage))
                            command.Parameters.AddWithValue("@ScorePercentage", ScorePercentage);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo
                                {
                                    CustomerName = reader["FIRST_NAME"].ToString() + " " + reader["LAST_NAME"].ToString(),
                                    BranchName = reader["branch"].ToString(),
                                    ScorePercentage = reader["YourMatchingField"].ToString(),
                                    // ... map other properties based on your database columns
                                };

                                SearchResult.Add(customerInfo);
                            }
                        }
                    }
                }
                // Print SearchResult to the console
                foreach (var result in SearchResult)
                {
                    Console.WriteLine($"CustomerName: {result.CustomerName}, BranchName: {result.BranchName}, ScorePercentage: {result.ScorePercentage}, ...");
                    // Include other properties as needed
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
        }

        public class CustomerInfo
        {
            public string CustomerName { get; set; }
            public string NidNo { get; set; }
            public string FathersName { get; set; }
            public DateTime Dob { get; set; }
            public string MothersName { get; set; }
            public string AccountNo { get; set; }
            public string EtinNo { get; set; }
            public string PassportNo { get; set; }
            public string MobileNo { get; set; }
            public string ScorePercentage { get; set; }
            public string BranchName { get; set; }
        }
    }
}
