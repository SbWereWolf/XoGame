using System.Windows;
using System.Windows.Controls;

namespace XoGame
{

    public struct Move
    {
        public int X;
        public int Y;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string TitlePrefix = "Крестики-нолики";
        private const string NoOneWon = "ничья";

        private const string TheWinnerIs = "победил:";

        private readonly Game _game;

        private readonly User _user;

        public MainWindow()
        {
            InitializeComponent();

            _user = new User("игрок");
            var opponent = new RandomAi("комп");
            var board = new Board(width: 3, height: 3);
            _game = new Game(x: _user, o: opponent, givenBoard: board);
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_game == null)
            {
                return;
            }

            if (_game.Finished())
            {
                return;
            }

            var theButton = (Button)sender;
            var move = ButtonToMove(theButton);
            _user?.move_is(move);
            var playerMove = _game.Step();

            if (!playerMove.Equals(Game.impossible_move()))
            {
                MoveMade(move, "X");

                if (_game != null && _game.Finished()) {
                    Finish();
                    return;
                }

                if (_game != null)
                {
                    Move opponentMove;
                    do
                    {
                        opponentMove = _game.Step();
                    } while (opponentMove.Equals(Game.impossible_move()));

                    MoveMade(opponentMove, "0");

                    if (_game != null && _game.Finished())
                    {
                        Finish();
                    }
                }

            }
        }

        private void MoveMade(Move move, string what)
        {
            var button = MoveToButton(move);
            if (button != null) button.Content = what;
        }

        private void Finish()
        {
            if (_game != null && _game.HasWinner())
            {
                this.Title = TitlePrefix + ": " + TheWinnerIs + " " + _game.Winner();
            }
            else
            {
                this.Title = TitlePrefix + ": " + NoOneWon;
            }
        }
        
        private const string ButtonPrefix = "At";

        private const int Xpos = 2;
        private const int Ypos = 3;


        private Button MoveToButton(Move move)
        {
            return (Button)this.FindName(ButtonPrefix + move.X.ToString() + move.Y.ToString());
        }
                
        private static Move ButtonToMove(Button button)
        {
            return new Move { X = ButtonX(button), Y = ButtonY(button) };
        }

        
        private static int ButtonX(Button button)
        {
            var result = -1;
            var name = button?.Name;

            if (name != null) result = int.Parse(name[Xpos].ToString());

            return result;
        }

        private static int ButtonY(Button button)
        {
            var result = -1;

            var name = button?.Name;

            if (name != null) result = int.Parse(name[Ypos].ToString());
            return result;
        }
    }
}
