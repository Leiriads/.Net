using MySqlConnector;

namespace MVC.dbc
{
    public class ConnectionFactory
    {
        //mysql://127.0.0.1:3306
        private string connectionString = "server=localhost;user=root;database=mvc;password= ;";

        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

    }
}
