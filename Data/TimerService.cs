using System;
using System.Threading;

namespace MyGoodnessHarold.Data
{
    public class TimerService : IDisposable
    {
        public DateTime CurrentTime { get; private set; }
        private Timer timer;

        public event Action OnTimeChanged;

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

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
