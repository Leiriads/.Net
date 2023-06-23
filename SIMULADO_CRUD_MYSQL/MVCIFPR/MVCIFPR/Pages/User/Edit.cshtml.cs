using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using MVC.dbc;
using MySqlConnector;
using System.Collections.Generic;

namespace MVCIFPR.Pages.User
{
    public class EditModel : PageModel
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

            questao.Pergunta = Request.Form["Pergunta"];
            questao.OpcA = Request.Form["OpcA"];
            questao.OpcB = Request.Form["OpcB"];
            questao.OpcC = Request.Form["OpcC"];
            questao.OpcD = Request.Form["OpcD"];
            questao.OpcE = Request.Form["OpcE"];
            questao.RespostaCorreta = Request.Form["resposta_correta"];

            Console.WriteLine("user :"+ questao.Pergunta + questao.OpcA + questao.OpcB);

            if (questao.Pergunta.Length == 0 || questao.OpcA.Length == 0 || questao.OpcB.Length == 0 || questao.OpcC.Length == 0 || questao.OpcD.Length == 0 || questao.OpcE.Length == 0)

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

                    // Execute o update no SQL 
                    string query = "UPDATE questao SET pergunta = @pergunta, opcA = @opcA, opcB = @opcB, opcC = @opcC, opcD = @opcD, opcE = @opcE, resposta_correta = @resposta_correta WHERE id = @id";


                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", questao.Id);
                        command.Parameters.AddWithValue("@pergunta", questao.Pergunta);
                        command.Parameters.AddWithValue("@opcA", questao.OpcA);
                        command.Parameters.AddWithValue("@opcB", questao.OpcB);
                        command.Parameters.AddWithValue("@opcC", questao.OpcC);
                        command.Parameters.AddWithValue("@opcD", questao.OpcD);
                        command.Parameters.AddWithValue("@opcE", questao.OpcE);
                        command.Parameters.AddWithValue("@resposta_correta", questao.RespostaCorreta);

                        command.ExecuteNonQuery();
                        
                        
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            successMessage = "Questao " + questao.Pergunta + " Alterado!";
            questao.Pergunta = ""; 
            questao.OpcA = ""; 
            questao.OpcB = "";
            questao.OpcC = "";
            questao.OpcD = "";
            questao.OpcE = "";
            questao.RespostaCorreta = "";

            //Response.Redirect("/User/Index");
            // Adicione o Sweet Alert aqui



        }

    }

}
