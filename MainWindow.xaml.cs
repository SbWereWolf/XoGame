using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace XoGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        [NotNull] private readonly Game _game;
        [NotNull] private readonly Board _board;
        private Turn[] _comands;
        [NotNull] private readonly TurnFactory _turnFactory;

        [NotNull] private readonly Painter _painter;
        [NotNull] private readonly Button[] _cells;
        private Paint[] _paints;
        private const int PositionBase = 10;
        [NotNull] private readonly PaintFactory _paintFactory;

        private const int NoIndex = -1;
        private int _current = NoIndex;

        public MainWindow()
        {
            InitializeComponent();

            _game = new Game("X", "0");

            ShowActivePlayer();

            _board = new Board(new ReferyFactory());
            _comands = new Turn[] { };
            _paints = new Paint[] { };

            var factorization = new Factorization(PositionBase);
            _paintFactory = new PaintFactory();
            _turnFactory = new TurnFactory(factorization);


            _cells = new[]
            {
                C00,C01,C02,C10,C11,C12,C20,C21,C22
            };
            if (C00 != null && C01 != null && C02 != null && C10 != null
                && C11 != null && C12 != null && C20 != null && C21 != null && C22 != null)
            {
                C00.Tag = factorization.Encode(new Tuple<int, int>(0, 0));
                C01.Tag = factorization.Encode(new Tuple<int, int>(0, 1));
                C02.Tag = factorization.Encode(new Tuple<int, int>(0, 2));
                C10.Tag = factorization.Encode(new Tuple<int, int>(1, 0));
                C11.Tag = factorization.Encode(new Tuple<int, int>(1, 1));
                C12.Tag = factorization.Encode(new Tuple<int, int>(1, 2));
                C20.Tag = factorization.Encode(new Tuple<int, int>(2, 0));
                C21.Tag = factorization.Encode(new Tuple<int, int>(2, 1));
                C22.Tag = factorization.Encode(new Tuple<int, int>(2, 2));
            }
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
            var isSuccess = false;
            if (theButton?.Tag != null)
            {
                key = (int)theButton.Tag;
                isSuccess = true;
            }
            if (!isSuccess)
            {
                MessageBox.Show("Fail parse cell number");
            }

            var player = _game.GetActivePlayer();
            Turn turn = null;

            var isComplete = false;
            if (isSuccess)
            {
                turn = _turnFactory.Make(_board, key);
                isComplete = _game.Apply(turn);
            }
            if (!isComplete)
            {
                MessageBox.Show("Не верный ход");
            }
            if (isComplete)
            {
                var paint = _paintFactory.Make(_cells, key, player);
                _painter.Paint(paint);
                ShowActivePlayer();

                TakeTurns(_current);
                AddTurn(turn);

                TakePaints(_current);
                AddPaint(paint);

                if (_comands != null)
                {
                    _current = _comands.GetUpperBound(0);
                }
            }
            if (isComplete)
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
            if (_paints != null)
            {
                var list = new List<Paint>();
                for (var i = 0; i <= upperBound; i++)
                    list.Add(_paints[i]);
                _paints = list.ToArray();
            }
        }
        private void TakeTurns(int upperBound)
        {
            if (_comands != null)
            {
                var list = new List<Turn>();
                for (var i = 0; i <= upperBound; i++)
                    list.Add(_comands[i]);
                _comands = list.ToArray();
            }
        }

        private void AddTurn(Turn turn)
        {
            if (_comands != null)
            {
                var list = _comands.ToList();
                list.Add(turn);
                _comands = list.ToArray();
            }
        }
        private void AddPaint(Paint paint)
        {
            if (_paints != null)
            {
                var list = _paints.ToList();
                list.Add(paint);
                _paints = list.ToArray();
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            var may = _current > NoIndex;
            if (may && _comands != null && _paints != null)
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
            var may = _comands != null && _comands.GetUpperBound(0) > _current;
            var isSuccess = false;
            var player = string.Empty;
            if (may)
            {
                player = _game.GetActivePlayer();
                var next = _current + 1;
                isSuccess = _game.Apply(_comands[next]);
            }
            if (isSuccess && _paints != null)
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
