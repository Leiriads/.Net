
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.dbc;
using MySqlConnector;
using System;
using System.Data;

namespace MVCIFPR.Pages.User
{
    public class IndexModel : PageModel
    {
        public Questao questao = new Questao();
        public List<Questao> listQuest = new List<Questao>();
       

        public void OnGet()
        {
            try
            {
                var factory = new ConnectionFactory();

                // Obtenha uma conexão MySqlConnection usando o método GetConnection()
                using (var connection = factory.GetConnection())
                {
                    connection.Open();

                    // Execute a consulta SQL de seleção
                    String sql = "SELECT id, pergunta, opcA, opcB, opcC, opcD,opcE, resposta_correta FROM questao";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            // Preencha a lista de clientes com os resultados da consulta
                            while (reader.Read())
                            {
                                var quest = new Questao
                                {
                                    Id = reader.GetInt32("id"),
                                    Pergunta = reader.GetString("pergunta"),
                        
                                    OpcA = reader.GetString("opcA"),
                                    OpcB = reader.GetString("opcB"),
                                    OpcC = reader.GetString("opcC"),
                                    OpcD = reader.GetString("opcD"),
                                    OpcE = reader.GetString("opcE"),
                                    RespostaCorreta = reader.IsDBNull(reader.GetOrdinal("resposta_correta")) ? null : reader.GetString(reader.GetOrdinal("resposta_correta")),



                                };
                                listQuest.Add(quest);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate qualquer exceção que ocorra aqui
                Console.WriteLine("Exception" + ex.ToString());
            }
        }
    }

    public class Questao
    {
        public int Id;
        public string Pergunta;
        public string OpcA;
        public string  OpcB;
        public string  OpcC;
        public string  OpcD;
        public string  OpcE;
        public string  RespostaCorreta;

    }

}
