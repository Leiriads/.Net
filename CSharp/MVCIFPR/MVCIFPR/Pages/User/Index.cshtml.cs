using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.dbc;
using MySqlConnector;
using System;

namespace MVCIFPR.Pages.User
{
    public class IndexModel : PageModel
    {
        public UserInfo userinfo = new UserInfo();
        public List<UserInfo> listUsers = new List<UserInfo>();
       

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
                    String sql = "SELECT id, nome, cpf, tipousuario FROM usuario";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            // Preencha a lista de clientes com os resultados da consulta
                            while (reader.Read())
                            {
                                var user = new UserInfo
                                {
                                    Id = reader.GetInt32("id"),
                                    Nome = reader.GetString("nome"),
                                    Cpf = reader.GetString("cpf"),
                                    TipoUsuario = reader.GetString("tipousuario")
                                };
                                listUsers.Add(user);
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

    public class UserInfo
    {
        public int Id;

        public string Nome;

        public string Cpf;

        public string TipoUsuario;
    }
}
