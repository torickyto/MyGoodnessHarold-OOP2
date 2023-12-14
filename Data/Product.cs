using MySqlConnector;
using System;
using System.Collections.Generic;

namespace learning_management_sys.Data
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string StockQuantity { get; set; }
        public string Category { get; set; }

        public Product(string ProductIDID, string ProductName, string Price, string StockQuantity, string Category)
        {
            this.ProductID = ProductIDID;
            this.ProductName = ProductName;
            this.Price = Price;
            this.StockQuantity = StockQuantity;
            this.Category = Category;
        }

        public static List<Product> Connect()
        {
            List<Product> ProductIDs = new List<Product>();
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "harold",
                UserID = "root",
                Password = "",
            };

            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM employees";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductIDs.Add(new Product(
                        reader.GetString("ProductID"),
                        reader.GetString("ProductName"),
                        reader.GetString("Price"),
                        reader.GetString("StockQuantity"),
                        reader.GetString("Category")
                    ));
                }
            }

            return ProductIDs;
        }

        public override string ToString()
        {
            return $"{ProductID}, {ProductName}, {Price}, {StockQuantity}, {Category}";
        }
    }
}
