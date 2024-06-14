using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using test02.Pages.Clients;

namespace test02.Pages.Staff
{
    public class CreateModel : PageModel
    {
        public StaffList staff = new StaffList();
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            staff.fname = Request.Form["fname"];
            staff.lname = Request.Form["lname"];
            staff.job = Request.Form["job"];

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                string sql = "use mystore; insert into staff (first_name, last_name, job) values (@fname, @lname, @job)";
            
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("fname", staff.fname);
                        command.Parameters.AddWithValue("lname", staff.lname);
                        command.Parameters.AddWithValue("job", staff.job);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Response.Redirect("/staff/index");
        }
    }
}
