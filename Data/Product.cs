using MySqlConnector;
using System;
using System.Collections.Generic;
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

//Class page for product, has getters and setters and a constructor
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
        //creates a list of all the products from the database
        public static List<Product> GetAllProducts()
        {
            //product list
            List<Product> products = new List<Product>();
            //setup how to connect to the database
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "harold",
                UserID = "root",
                Password = "password"
            };
            //open the database and pull product information
            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT p.*, c.CategoryName FROM products p JOIN Categories c ON p.CategoryID = c.CategoryID";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //create a new instance of product for each new product
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

        //Converts product to a string
        public override string ToString()
        {
            return $"{ProductID}, {ProductName}, {Price:C}, {StockQuantity}, {Category}";
        }
        public static void DeleteProduct(int productId)
            //method for deleting a product from database/menu
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
        //method for adding a product from database/menu
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
                        //add all parameters so system knows what info to fill in
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        command.Parameters.AddWithValue("@CategoryId", categoryId);


                        // Log the SQL command for debugging
                        Console.WriteLine($"SQL Command: {command.CommandText}");

                        command.ExecuteNonQuery();
                    }
                }
            
            
        }
        //updates the database with new quantity after adding an item
        public static void UpdateStockQuantity(int productId, int newQuantity)
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

                string sql = "UPDATE products SET StockQuantity = @NewQuantity WHERE ProductID = @ProductId";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NewQuantity", newQuantity);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}