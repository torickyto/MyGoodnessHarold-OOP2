using System;

namespace MyGoodnessHarold.Data
{
    public class UserStateService
    {
        public string FirstName { get; private set; }

<<<<<<< HEAD
        public event Action OnUserStateChanged;
=======
        public event Action OnUserStateChanged;
>>>>>>> 210a73951002691b36dacfff4228920dbd255187

        public void SetCurrentUser(string firstName)
        {
            FirstName = firstName;
            NotifyUserStateChanged();
        }

        public void ClearCurrentUser()
        {
            FirstName = null;
            NotifyUserStateChanged();
        }

        private void NotifyUserStateChanged() => OnUserStateChanged?.Invoke();
    }
}
