using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test02.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string successMassage = "";
        public string errorMassage = "";
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMassage = "seluruh field harus diisi";
                return;
            }

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                String sql = "use mystore;" +
                        "INSERT INTO clients (name, email, phone, address) VALUES" +
                        "(@name,@email,@phone,@address)";
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);
                        command.Parameters.AddWithValue("email", clientInfo.email);
                        command.Parameters.AddWithValue("phone", clientInfo.phone);
                        command.Parameters.AddWithValue("address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMassage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.phone = ""; clientInfo.email = ""; clientInfo.address = "";
            successMassage = "data berhasil ditambahkan";
            Response.Redirect("/clients/index");
        }
    }
}
