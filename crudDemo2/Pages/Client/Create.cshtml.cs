using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using static crudDemo2.Pages.Client.IndexModel;

namespace crudDemo2.Pages.Client
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public string msg = "";
        public void OnGet()
        {
        }

        public void OnPost() {
        
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];

            if(userInfo.name.Length == 0) {
                msg = "failed";
                return;
            }

            //saqve new user data
            try
            {
                String connectionString = "Data Source=MR-LAPPU;Initial Catalog=crudDemo2;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "insert into [user] (name,email,phone,address) values (@name,@email,@phone,@address);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            userInfo.name = "";userInfo.email = ""; userInfo.phone=""; userInfo.address = "";
            msg = "sucess";
            Response.Redirect("Index");

        }
    }
}
