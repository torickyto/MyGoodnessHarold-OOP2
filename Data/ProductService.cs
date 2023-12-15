using System.Collections.Generic;
using System.Linq;

namespace MyGoodnessHarold.Data
{
    public class ProductService
    {
        public List<Product> Products { get; private set; }
        public List<MenuItem> SelectedItems { get; private set; } = new List<MenuItem>();

        public List<string> Categories { get; private set; }

        public ProductService()
        {
            Products = Product.GetAllProducts();
            Categories = FetchCategories();
        }

        private List<string> FetchCategories()
        {
            return new List<string> { "All", "Mains", "Appetizers", "Desserts" };
        }

        public void DeleteProduct(int productId)
        {
            Product.DeleteProduct(productId);
            Products = Product.GetAllProducts();
        }

        public void AddNewProduct(string productName, decimal price, int stockQuantity, int categoryId)
        {
            Product.SaveProduct(productName, price, stockQuantity, categoryId);
            Products = Product.GetAllProducts();
        }

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

        public void UpdateProductStock(Product product, int quantity)
        {
            Product.UpdateStockQuantity(product.ProductID, quantity);
        }

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



        public decimal GetTotal()
        {
            return SelectedItems.Sum(item => item.Price * item.Quantity);
        }
    }
}
