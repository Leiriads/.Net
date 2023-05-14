using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.dbc;
using MySqlConnector;

namespace MVCIFPR.Pages.User
{
    public class CreateModel : PageModel
    {
        public UserInfo userinfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            userinfo.Nome = Request.Form["Nome"];
            userinfo.Cpf = Request.Form["Cpf"];
            userinfo.TipoUsuario = Request.Form["TipoUsuario"];

            if(userinfo.Nome.Length == 0 || userinfo.Cpf.Length == 0 || userinfo.TipoUsuario.Length==0)
            {
                errorMessage = "Preencha todos os campos";
                return;
            }


            //salvar o novo usuario na database
            try
            {
                var factory = new ConnectionFactory();

                using (var connection = factory.GetConnection())
                {
                    connection.Open();

                    // Execute a inserção no SQL 
                    string query = "INSERT INTO usuario (id, nome, cpf, tipousuario) VALUES (@id, @nome, @cpf, @tipousuario)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", userinfo.Id);
                        command.Parameters.AddWithValue("@nome", userinfo.Nome);
                        command.Parameters.AddWithValue("@cpf", userinfo.Cpf);
                        command.Parameters.AddWithValue("@tipousuario", userinfo.TipoUsuario);

                        command.ExecuteNonQuery();

                        
                    }
                    connection.Close();
                }

            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Usuário " + userinfo.Nome + " Cadastrado!";
            userinfo.Nome = "";userinfo.Cpf = "";userinfo.TipoUsuario = "";
            
            //Response.Redirect("/User/Index");


        }
    }
}
