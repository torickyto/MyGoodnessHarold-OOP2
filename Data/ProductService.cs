using System.Collections.Generic;
using System.Linq;

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

//this class is for managing the products we sell
namespace MyGoodnessHarold.Data
{
    public class ProductService
    {
        //list of products
        public List<Product> Products { get; private set; }
        //list of menu items
        public List<MenuItem> SelectedItems { get; private set; } = new List<MenuItem>();
        
        public List<string> Categories { get; private set; }

        //create a new productservice
        public ProductService()
        {
            Products = Product.GetAllProducts();
            Categories = FetchCategories();
        }
        //list of categoires
        private List<string> FetchCategories()
        {
            return new List<string> { "All", "Mains", "Appetizers", "Desserts" };
        }
        //method to delete a product from order
        public void DeleteProduct(int productId)
        {
            Product.DeleteProduct(productId);
            Products = Product.GetAllProducts();
        }
        //adds new product to the menu
        public void AddNewProduct(string productName, decimal price, int stockQuantity, int categoryId)
        {
            Product.SaveProduct(productName, price, stockQuantity, categoryId);
            Products = Product.GetAllProducts();
        }
        //adds a new item the the order
        public void AddItem(Product product)
        {
            var existingItem = SelectedItems.FirstOrDefault(i => i.Name == product.ProductName);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                SelectedItems.Add(new MenuItem(product.ProductName, product.Price, 1));
            }
        }
        //removes an item from the order
        public void RemoveItem(MenuItem item)
        {
            var existingItem = SelectedItems.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Quantity--;
                if (existingItem.Quantity == 0)
                {
                    SelectedItems.Remove(existingItem);
                }
            }
        }
        //updates quantity in database
        public void UpdateProductStock(Product product, int quantity)
        {
            Product.UpdateStockQuantity(product.ProductID, quantity);
        }

        //finalize the order
        public string ProcessOrder()
        {
            foreach (var item in SelectedItems)
            {
                var product = Products.FirstOrDefault(p => p.ProductName == item.Name);
                if (product != null && product.StockQuantity >= item.Quantity)
                {
                    UpdateProductStock(product, product.StockQuantity - item.Quantity);
                }
            }

            SelectedItems.Clear();
            Products = Product.GetAllProducts(); // Refresh product list after processing

            return "Order Printed";
        }


        //calculate the total of the entire thing with taxs
        public decimal GetTotal()
        {
            return SelectedItems.Sum(item => item.Price * item.Quantity);
        }
    }
}
