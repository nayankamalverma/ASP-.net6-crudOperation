using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crudDemo2.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listUser = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=MR-LAPPU;Initial Catalog=crudDemo2;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Console.WriteLine(connectionString);
                    connection.Open();
                    String query = "select * from [user]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo user = new UserInfo();
                                user.userId = "" + reader.GetInt32(0);
                                user.name = reader.GetString(1);
                                user.email = reader.GetString(2);
                                user.phone = reader.GetString(3);
                                user.address = reader.GetString(4);
                                user.created_at = reader.GetDateTime(5).ToString();
                                listUser.Add(user);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public class UserInfo
        {
            public string userId;
            public string name;
            public string email;
            public string phone;
            public string address;
            public string created_at;
        }
    }
}
