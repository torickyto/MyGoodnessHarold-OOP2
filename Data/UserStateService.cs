using System;
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

//this class helps keep track of user currently on the system
namespace MyGoodnessHarold.Data
{
    public class UserStateService
    {
        //stores first name of user
        public string FirstName { get; private set; }

        public event Action OnUserStateChanged;
        //use to log in user and set as current
        public void SetCurrentUser(string firstName)
        {
            FirstName = firstName;
            NotifyUserStateChanged();
        }
        //clears the current user
        public void ClearCurrentUser()
        {
            FirstName = null;
            NotifyUserStateChanged();
        }

        //updates the user of logout
        private void NotifyUserStateChanged() => OnUserStateChanged?.Invoke();
    }
}
