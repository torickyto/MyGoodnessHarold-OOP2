using MySqlConnector;
using System;
using System.Collections.Generic;

namespace MyGoodnessHarold.Data
{
    public class Product
    {
        public int ProductID { get; set; }  // Change type to int
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string StockQuantity { get; set; }
        public string Category { get; set; }

        public Product(int ProductID, string ProductName, decimal Price, int StockQuantity, string Category)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.Price = Price.ToString();  // Convert decimal to string
            this.StockQuantity = StockQuantity.ToString();  // Convert int to string
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
                Password = "andromon"
            };

            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM products";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductIDs.Add(new Product(
                        reader.GetInt32("ProductID"),  // Use GetInt32 for int type
                        reader.GetString("ProductName"),
                        reader.GetDecimal("Price"),  // Use GetDecimal for decimal type
                        reader.GetInt32("StockQuantity"),  // Use GetInt32 for int type
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
