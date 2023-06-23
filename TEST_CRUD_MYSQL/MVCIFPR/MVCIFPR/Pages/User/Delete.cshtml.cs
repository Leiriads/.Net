using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.dbc;
using MySqlConnector;

namespace MVCIFPR.Pages.User
{
    public class DeleteModel : PageModel
    {
        public Questao questao = new Questao();
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
                    string query = "SELECT * FROM questao WHERE id=@id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                questao.Id = reader.GetInt32(0);
                                questao.Pergunta = reader.GetString(1);
                                questao.OpcA = reader.GetString(2);
                                questao.OpcB = reader.GetString(3);
                                questao.OpcC = reader.GetString(4);
                                questao.OpcD = reader.GetString(5);
                                questao.OpcE = reader.GetString(6);
                                questao.RespostaCorreta = reader.GetString(7);
                                
                                

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
            questao.Id = idInt;

            //Deletar o Usuário do database
            try
            {
                var factory = new ConnectionFactory();

                using (var connection = factory.GetConnection())
                {
                    connection.Open();

                    // Execute o update no SQL 
                    string query = "Delete FROM questao WHERE id = @id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", questao.Id);
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


            successMessage = " Questão " + questao.Pergunta + " Deletado!";
            
        }


    }
}
