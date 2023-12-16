using System;
using System.Threading;

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

//clock for our app to display the time
namespace MyGoodnessHarold.Data
{
    public class TimerService : IDisposable
    {
        public DateTime CurrentTime { get; private set; }
        private Timer timer;

        //let other parts of app know when a change to time was made
        public event Action OnTimeChanged;

        //sets current time
        public TimerService()
        {
            CurrentTime = DateTime.Now;
            timer = new Timer(_ =>
            {
                CurrentTime = DateTime.Now;
                OnTimeChanged?.Invoke();
            }, null, 0, 1000);
        }

        public void InvokeOnTimeChanged()
        {
            OnTimeChanged?.Invoke();
        }

        //dispose of timer
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
