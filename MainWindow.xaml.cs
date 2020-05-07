using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace XoGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
         private readonly Game _game;
         private readonly Board _board;
         private Turn[] _comands;

         private readonly Painter _painter;
         private readonly Button[] _cells;
         private Paint[] _paints;

        private const int NoIndex = -1;
        private int _current = NoIndex;
        private const int Factorization = 10;

        public MainWindow()
        {
            InitializeComponent();

            _game = new Game("X", "0");

            ShowActivePlayer();

            _board = new Board(new ReferyFactory());
            _comands = new Turn[] { };
            _paints = new Paint[] { };

            _cells = new[]
            {
                C00,C01,C02,C10,C11,C12,C20,C21,C22
            };
            _painter = new Painter();
        }

        private void ShowActivePlayer()
        {
            var title = _game.GetActivePlayer();
            if (TurnIndicator != null)
                TurnIndicator.Content = title;
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var theButton = (Button)sender;
            var key = 0;
            var isSuccess = theButton?.Tag != null && int.TryParse(theButton.Tag.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out key);
            if (!isSuccess)
                MessageBox.Show("Fail parse cell number");

            var player = _game.GetActivePlayer();
            Turn turn = null;
            var cell = new Tuple<int, int>(NoIndex, NoIndex);
            if (isSuccess)
            {
                var x = key % Factorization;
                var y = key / Factorization;
                cell = new Tuple<int, int>(x, y);
                turn = new Turn(_board, cell);
                isSuccess = _game.Apply(turn);
            }
            if (!isSuccess)
            {
                MessageBox.Show("Не верный ход");
            }
            if (isSuccess)
            {
                var paint = new Paint(_cells, cell, player);
                _painter.Paint(paint);
                ShowActivePlayer();

                TakeTurns(_current);
                AddTurn(turn);

                TakePaints(_current);
                AddPaint(paint);

                _current = _comands.GetUpperBound(0);
            }
            if (isSuccess)
            {
                ShowWinner(player);
            }
        }

        private void ShowWinner(string mark)
        {
            var isWin = _game.IsWin();
            var isDraw = _game.IsDraw();
            if (isWin)
            {
                MessageBox.Show($"Победитель {mark}");
            }
            if (isDraw)
            {
                MessageBox.Show("Ничья");
            }
        }

        private void TakePaints(int upperBound)
        {
            var list = new List<Paint>();
            for (var i = 0; i <= upperBound; i++)
                list.Add(_paints[i]);
            _paints = list.ToArray();
        }
        private void TakeTurns(int upperBound)
        {
            var list = new List<Turn>();
            for (var i = 0; i <= upperBound; i++)
                list.Add(_comands[i]);
            _comands = list.ToArray();
        }

        private void AddTurn(Turn turn)
        {
            var list = _comands.ToList();
            list.Add(turn);
            _comands = list.ToArray();
        }
        private void AddPaint(Paint paint)
        {
            var list = _paints.ToList();
            list.Add(paint);
            _paints = list.ToArray();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            var may = _current > NoIndex;
            if (may)
            {
                var action = _comands[_current];
                _game.Reverse(action);
                _painter.Purge(_paints[_current]);
                _current--;
                ShowActivePlayer();
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            var may = _comands.GetUpperBound(0) > _current;
            var isSuccess = false;
            var player = string.Empty;
            if (may)
            {
                player = _game.GetActivePlayer();
                var next = _current + 1;
                isSuccess = _game.Apply(_comands[next]);
            }
            if (isSuccess)
            {
                _current++;
                _painter.Paint(_paints[_current]);
                ShowActivePlayer();
            }
            if (isSuccess)
            {
                ShowWinner(player);
            }
        }
    }
}
