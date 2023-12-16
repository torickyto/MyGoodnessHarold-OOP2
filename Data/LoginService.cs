using System;
using System.Collections.Generic;
using MySqlConnector;
/*
    Team members: Grady Spurrill, Ricky To, Logan Hoppen
  POS Menu System for "My Goodness Harold" Kitchen
 
  This POS (Point of Sale) menu system is designed to facilitate the ordering process for a kitchen environment.
  It displays a list of food items retrieved from a database, allowing kitchen staff or customers to place orders.
  Users are greeted by name and can view the current time. They can add items to their order by interacting with the menu,
  which updates a running total including tax. The system supports item selection, quantity updates, and order finalization.
  Once an order is finalized, a message is displayed, and there is an option to print the order. The interface is intuitive,
  ensuring a smooth ordering experience.
*/

//validates login pin entered 
namespace MyGoodnessHarold.Data
{
    public static class LoginHelper
    {
        private static readonly string ConnectionString = "Server=localhost;Database=harold;User ID=root;Password=password;";

        public static bool ValidatePin(string enteredPin, out Employee loggedInEmployee)
        {
            loggedInEmployee = null;
            //connect to the database
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM employees WHERE Password = '{enteredPin}'";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    //save the employee as an object from the database
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