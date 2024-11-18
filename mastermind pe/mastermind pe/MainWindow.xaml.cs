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

        // Mastermind-PE5: XML-Documentatie
        /// <summary>
        /// Toggle debug mode to show/hide the generated code.
        /// </summary>
        private void ToggleDebug() { /* Zie hierboven */ }

        /// <summary>
        /// Starts a countdown timer for the player's turn.
        /// </summary>
        private void StartCountdown() { /* Zie hierboven */ }

        /// <summary>
        /// Stops the countdown timer if it is running.
        /// </summary>
        private void StopCountdown() { /* Zie hierboven */ }

        // Helper: Game Initialisatie
        private void InitializeGame()
        {
            ComboBox1.ItemsSource = _colors;
            ComboBox2.ItemsSource = _colors;
            ComboBox3.ItemsSource = _colors;
            ComboBox4.ItemsSource = _colors;

            _generatedCode = GenerateRandomCode();
            Title = "Mastermind Game - Start";
            NextAttempt();
        }

        private string[] GenerateRandomCode()
        {
            var random = new Random();
            return Enumerable.Range(0, 4).Select(_ => _colors[random.Next(_colors.Length)]).ToArray();
        }

        // Helper: Gok Controleren
        private void CheckCode(object sender, RoutedEventArgs e)
        {
            var guesses = new[] {
                ComboBox1.SelectedItem as string,
                ComboBox2.SelectedItem as string,
                ComboBox3.SelectedItem as string,
                ComboBox4.SelectedItem as string
            };

            var labels = new[] { Label1, Label2, Label3, Label4 };

            for (int i = 0; i < 4; i++)
            {
                if (guesses[i] == null) continue;

                if (guesses[i] == _generatedCode[i])
                {
                    labels[i].BorderBrush = Brushes.DarkRed;
                }
                else if (_generatedCode.Contains(guesses[i]))
                {
                    labels[i].BorderBrush = Brushes.Wheat;
                }
                else
                {
                    labels[i].BorderBrush = Brushes.Black;
                }

                labels[i].BorderThickness = new Thickness(2);
            }

            StartCountdown();
            NextAttempt();
        }

        private void OnColorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedColor)
            {
                if (comboBox == ComboBox1) Label1.Content = selectedColor;
                if (comboBox == ComboBox2) Label2.Content = selectedColor;
                if (comboBox == ComboBox3) Label3.Content = selectedColor;
                if (comboBox == ComboBox4) Label4.Content = selectedColor;
            }
        }
    }
}
