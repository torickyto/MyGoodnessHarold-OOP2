using System;

namespace MyGoodnessHarold.Data
{
    public class UserStateService
    {
        public string FirstName { get; private set; }

    public static List<Employee> Connect()
    {
        List<Employee> Employees = new List<Employee>();
        var builder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "harold",
            UserID = "root",
            Password = "password",
        };
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
