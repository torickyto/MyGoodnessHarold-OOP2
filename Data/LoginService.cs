using System;
using System.Collections.Generic;
using MySqlConnector;

namespace MyGoodnessHarold.Data
{
    public static class LoginHelper
    {
        private static readonly string ConnectionString = "Server=localhost;Database=harold;User ID=root;Password=andromon;";

        public static bool ValidatePin(string enteredPin, out Employee loggedInEmployee)
        {
            loggedInEmployee = null;

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM employees WHERE Password = '{enteredPin}'";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                        loggedInEmployee = new Employee(
                        reader.GetInt32("EmployeeID").ToString(),
                        reader.GetString("FirstName"),
                        reader.GetString("LastName"),
                        reader.GetString("Position")
                    );

                    return true;
                }
            }

            return false;
        }
    }
}