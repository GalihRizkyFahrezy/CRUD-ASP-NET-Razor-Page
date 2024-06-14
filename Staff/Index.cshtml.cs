using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using test02.Pages.Clients;

namespace test02.Pages.Staff
{
    public class IndexModel : PageModel
    {
        public List<StaffList> staffList = new List<StaffList>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                string sql = "use mystore; select * from staff";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StaffList staff = new StaffList();

                                staff.id = "" + reader.GetInt32(0);
                                staff.fname = reader.GetString(1);
                                staff.lname = reader.GetString(2);
                                staff.job = reader.GetString(3);
                                staff.hired = "" + reader.GetDateTime(4);

                                staffList.Add(staff);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class StaffList
    {
        public string id;
        public string fname;
        public string lname;
        public string job;
        public string hired;
    }
}
