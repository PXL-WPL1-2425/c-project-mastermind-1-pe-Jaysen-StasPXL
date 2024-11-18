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

      
