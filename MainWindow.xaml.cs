using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XoGame
{

    public struct Move
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string titlePrefix = "Крестики-нолики";
        //const string gameIsFinished = "игра закончена";
        const string noOneWon = "ничья";
        const string theWinnerIs = "победил:";

        Game game;
        User user;
        //Board board;

        public MainWindow()
        {
            InitializeComponent();

            user = new User("игрок");
            var opponent = new RandomAI("комп");
            var board = new Board(width: 3, height: 3);
            game = new Game(x: user, o: opponent, given_board: board);


            //game.step until game.finished?
            //user.see game
            //puts 'Выиграл: %s' % game.winner.name
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            // этот метод на руби по другому (по-лучше), но там синхронный что-ли польз. интерфейс
            // как при таком написать лучше -S ещё надо найти/придумать

            if (game.finished())
            {
                return;
            }

            var theButton = (Button)sender;
            var move = ButtonToMove(theButton);
            user.move_is(move);
            var playerMove = game.step();

            if (!playerMove.Equals(Game.impossible_move()))
            {
                MoveMade(move, "X");

                if (game.finished()) {
                    finish();
                    return;
                }

                Move opponentMove;
                do
                {
                    opponentMove = game.step();
                } while (opponentMove.Equals(Game.impossible_move()));

                MoveMade(opponentMove, "0");

                if (game.finished())
                {
                    finish();
                    return;
                }
            }

            //MessageBoxResult result = MessageBox.Show("Hello: " + move.x.ToString() + " " + move.y.ToString());   
        }

        private void MoveMade(Move move, string what)
        {
            var button = MoveToButton(move);
            button.Content = what;
        }

        private void finish()
        {
            if (game.hasWinner())
            {
                this.Title = titlePrefix + ": " + theWinnerIs + " " + game.winner();
            }
            else
            {
                this.Title = titlePrefix + ": " + noOneWon; // gameIsFinished + ": " + winner();
            }
        }

/*        private string winner()
        {
            return "победитель кто-то там";
        }*/



        // "AtXY" (button.Name)
        const string buttonPrefix = "At";
        const int xpos = 2;
        const int ypos = 3;


        private Button MoveToButton(Move move)
        {
            return (Button)this.FindName(buttonPrefix + move.x.ToString() + move.y.ToString());
        }
                
        private Move ButtonToMove(Button button)
        {
            return new Move { x = ButtonX(button), y = ButtonY(button) };
        }

        
        private int ButtonX(Button button)
        {
            var name = button.Name;
            return Int32.Parse(name[xpos].ToString());
        }

        private int ButtonY(Button button)
        {
            var name = button.Name;
            return Int32.Parse(name[ypos].ToString());
        }
    }
}
