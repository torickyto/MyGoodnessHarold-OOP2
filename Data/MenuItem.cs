namespace MyGoodnessHarold.Data
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

    //Getters and setters and constructor for MenuItems
{
    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public MenuItem(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
