using MySqlConnector;
using System;
using System.Collections.Generic;

namespace MyGoodnessHarold.Data
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }

        public Product(int productID, string productName, decimal price, int stockQuantity, string category)
        {
            ProductID = productID;
            ProductName = productName;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
        }

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "harold",
                UserID = "root",
                Password = "password"
            };

            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT p.*, c.CategoryName FROM products p JOIN Categories c ON p.CategoryID = c.CategoryID";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product(
                        reader.GetInt32("ProductID"),
                        reader.GetString("ProductName"),
                        reader.GetDecimal("Price"),
                        reader.GetInt32("StockQuantity"),
                        reader.GetString("CategoryName")
                    ));
                }
            }

            return products;
        }

        public override string ToString()
        {
            return $"{ProductID}, {ProductName}, {Price:C}, {StockQuantity}, {Category}";
        }
        public static void DeleteProduct(int productId)
        {
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "harold",
                UserID = "root",
                Password = "password"
            };

            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();

                string sql = "DELETE FROM products WHERE ProductID = @ProductId";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void SaveProduct(string productName, decimal price, int stockQuantity, int categoryId)
        {
            
                var builder = new MySqlConnectionStringBuilder()
                {
                    Server = "localhost",
                    Database = "harold",
                    UserID = "root",
                    Password = "password"
                };

                using (var connection = new MySqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO products (ProductName, Price, StockQuantity, CategoryID) VALUES (@ProductName, @Price, @StockQuantity, @CategoryId)";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                       

                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        command.Parameters.AddWithValue("@CategoryId", categoryId);

                        // Log the SQL command for debugging
                        Console.WriteLine($"SQL Command: {command.CommandText}");

                        command.ExecuteNonQuery();
                    }
                }
            
            
        }

    }
}