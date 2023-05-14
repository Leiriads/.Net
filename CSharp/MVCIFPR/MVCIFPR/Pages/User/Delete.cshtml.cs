using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.dbc;
using MySqlConnector;

namespace MVCIFPR.Pages.User
{
    public class DeleteModel : PageModel
    {
        public UserInfo userinfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];



            try
            {
                var factory = new ConnectionFactory();

                using (var connection = factory.GetConnection())
                {
                    connection.Open();

                    // Execute a inserção no SQL 
                    string query = "SELECT * FROM usuario WHERE id=@id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userinfo.Id = reader.GetInt32(0);
                                userinfo.Nome = reader.GetString(1);
                                userinfo.Cpf = reader.GetString(2);
                                userinfo.TipoUsuario = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            String id = Request.Query["id"];
            int idInt = Convert.ToInt32(id);
            // userinfo.Id já contém o valor do ID do usuário
            userinfo.Id = idInt;

            //Deletar o Usuário do database
            try
            {
                var factory = new ConnectionFactory();

                using (var connection = factory.GetConnection())
                {
                    connection.Open();

                    // Execute o update no SQL 
                    string query = "Delete FROM usuario WHERE id = @id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", userinfo.Id);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            successMessage = "Usuário " + userinfo.Nome + " Deletado!";
            //userinfo.Nome = ""; userinfo.Cpf = ""; userinfo.TipoUsuario = "";



            //Response.Redirect("/User/Index");
            // Adicione o Sweet Alert aqui



        }


    }
}
