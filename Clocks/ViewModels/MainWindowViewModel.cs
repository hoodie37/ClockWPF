using Clocks.ViewModels.Base;
using Clocks.Commands;
using System.Windows.Input;
using System.Windows;
using System;
using System.Windows.Threading;

namespace Clocks.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        private readonly DispatcherTimer _timer;
        

        public DateTime CurrentTime { get { return DateTime.Now; } }

        private int _SecondAngle;
        public int SecondAngle
        {
            get => _SecondAngle;
            set => Set(ref _SecondAngle,value);
        }

        private double _MinuteAngle;
        public double MinuteAngle
        {
            get => _MinuteAngle;
            set => Set(ref _MinuteAngle, value);
        }

        private double _HourAngle;
        public double HourAngle
        {
            get => _HourAngle;
            set => Set(ref _HourAngle, value);
        }

        public ICommand CloseAppCommand { get; }
        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecuted(object p) => true;

        public MainWindowViewModel()
        {
            CloseAppCommand = new ActionCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecuted);
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            
            _timer.Start();
            _timer.Tick += new EventHandler(Timer_Tick);
          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            int sec = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;

            SecondAngle = 6 * sec;

            MinuteAngle = (min * 6) + (SecondAngle / 60.0);

            HourAngle = (hour - 12) * 6 + MinuteAngle / 12.0;

        }
    }
}
