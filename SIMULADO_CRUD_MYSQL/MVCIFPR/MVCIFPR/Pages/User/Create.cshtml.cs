using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.dbc;
using MySqlConnector;

namespace MVCIFPR.Pages.User
{
    public class CreateModel : PageModel
    {
        public Questao questao = new Questao();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            
            questao.Pergunta = Request.Form["enunciado"];
            questao.OpcA = Request.Form["opcA"];
            questao.OpcB = Request.Form["opcB"];
            questao.OpcC = Request.Form["opcC"];
            questao.OpcD = Request.Form["opcD"];
            questao.OpcE = Request.Form["opcE"];
            questao.RespostaCorreta = Request.Form["resposta_correta"];
            

            if (questao.Pergunta.Length == 0 || questao.OpcA.Length == 0 || questao.OpcB.Length == 0 || questao.OpcC.Length == 0 || questao.OpcD.Length == 0 || questao.OpcE.Length == 0 || questao.RespostaCorreta.Length == 0)
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
                    string query = "INSERT INTO questao (pergunta, opcA, opcB, opcC,opcD,opcE,resposta_correta) VALUES (@pergunta, @opcA, @opcB, @opcC,@opcD,@opcE,@resposta_correta)";

                    using (var command = new MySqlCommand(query, connection))
                    {
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

            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Questao " + questao.Pergunta + " Cadastrado!";
            questao.Pergunta = "";
            questao.OpcA = "";
            questao.OpcB = "";
            questao.OpcC = "";
            questao.OpcD = "";
            questao.OpcE = "";



        }
    }
}
