using MySqlConnector;

namespace MyGoodnessHarold.Data;
//employee class
public class Employee
    //getters and setters
{
    public string EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Password { get; set; }

    public Employee(string EmployeeID, string FirstName, string LastName, string Position)
        //constructor
    {
        this.EmployeeID = EmployeeID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Position = Position;
    }

    //connects to database and gets a list of employees
    public static List<Employee> Connect()
    {
        List<Employee> Employees = new List<Employee>();
        //set up connection to database where data is kept
        var builder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "harold",
            UserID = "root",
            Password = "password",
        };
        //talk with database and gather employee data
        using (var connection = new MySqlConnection(builder.ConnectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM products";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //make a new employee for each instance
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
    //to string method for the employee info
    public override string ToString()
    {
        return $"{EmployeeID}, {FirstName}, {LastName}, {Position}";
    }
}

