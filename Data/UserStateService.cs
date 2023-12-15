using MySqlConnector;

namespace MyGoodnessHarold.Data;

public class Employee
{
    public string EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Password { get; set; }

    public Employee(string EmployeeID, string FirstName, string LastName, string Position)
    {
        this.EmployeeID = EmployeeID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Position = Position;
    }

    public static List<Employee> Connect()
    {
        List<Employee> Employees = new List<Employee>();
        var builder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "harold",
            UserID = "root",
            Password = "password",
        };

        using (var connection = new MySqlConnection(builder.ConnectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM products";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employees.Add(new Employee(
                    reader.GetString("EmployeeID"),
                    reader.GetString("FirstName"),
                    reader.GetString("LastName"),
                    reader.GetString("Position")
                ));
            }
        }

        return Employees;
    }

    public override string ToString()
    {
        return $"{EmployeeID}, {FirstName}, {LastName}, {Position}";
    }
}

