using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test02.Pages.Staff
{
    public class EditModel : PageModel
    {
        public StaffList staff = new StaffList();
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void OnGet()
        {
            string id = Request.Query["id"];

            string sql = "use mystore; select * from staff where id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {

                        if(reader.Read())
                        {
                            staff.id = "" + reader.GetInt32(0);
                            staff.fname = reader.GetString(1);
                            staff.lname = reader.GetString(2);
                            staff.job = reader.GetString(3);
                        }
                    }
                }
            }
        }

        public void OnPost() 
        {
            staff.id = Request.Form["id"];
            staff.fname = Request.Form["fname"];
            staff.lname = Request.Form["lname"];
            staff.job = Request.Form["job"];


            try
            {
                

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "use mystore; update staff " +
                        "set first_name=@fname, last_name=@lname, job=@job " +
                        "where id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", staff.id);
                        command.Parameters.AddWithValue("@fname", staff.fname);
                        command.Parameters.AddWithValue("@lname", staff.lname);
                        command.Parameters.AddWithValue("@job", staff.job);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Response.Redirect("/staff/index");
        }
    }
}
