using System;

namespace MyGoodnessHarold.Data
{
    public class UserStateService
    {
        public string FirstName { get; private set; }

        public event Action OnUserStateChanged;

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
