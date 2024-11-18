using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MastermindGame
{
    public partial class MainWindow : Window
    {
        private readonly string[] _colors = { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        private string[] _generatedCode;
        private int _attempts;
        private DispatcherTimer _timer;
        private int _timeLeft;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        // Mastermind-PE1: Pogingen
        private void NextAttempt()
        {
            _attempts++;
            Title = $"Mastermind - Poging {_attempts}";
        }

        // Mastermind-PE2: Debug-mode
        private void ToggleDebug()
        {
            DebugTextBox.Visibility = DebugTextBox.Visibility == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;
            DebugTextBox.Text = string.Join(", ", _generatedCode);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.F12)
                ToggleDebug();
        }

        // Mastermind-PE3: Timer Start
        private void StartCountdown()
        {
            _timeLeft = 10;
            if (_timer == null)
            {
                _timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                _timer.Tick += Timer_Tick;
            }
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (--_timeLeft <= 0)
            {
                StopCountdown();
                MessageBox.Show("Tijd is op! Je verliest deze beurt.");
                NextAttempt();
            }
        }

        // Mastermind-PE4: Timer Stop
        private void StopCountdown()
        {
            _timer?.Stop();
        }

       
