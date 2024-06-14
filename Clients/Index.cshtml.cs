using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test02.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "use mystore; SELECT * FROM clients;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo ClientInfo1 = new ClientInfo();
                                ClientInfo1.id = reader.GetInt32(0).ToString();
                                ClientInfo1.name = reader.GetString(1);
                                ClientInfo1.email = reader.GetString(2);
                                ClientInfo1.phone = reader.GetString(3);
                                ClientInfo1.address = reader.GetString(4);
                                ClientInfo1.created_at = "" + reader.GetDateTime(5);

                                listClients.Add(ClientInfo1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("error " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}
