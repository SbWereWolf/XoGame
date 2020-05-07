namespace XoGame
{
    internal class Game
    {
        private PlayerOne _activePlayer;
        private readonly PlayerOne _playerX;
        private readonly PlayerOne _player0;
        private GameState _state = GameState.InProcess;

        public Game(string player1, string player2)
        {
            _playerX = new PlayerOne(player1);
            _player0 = new PlayerOne(player2);
            _activePlayer = _playerX;
        }

        public bool Apply(Turn turn)
        {
            var may = turn != null && IsInProcess();
            var complete = false;
            if (may && _activePlayer == _playerX)
            {
                complete =  turn.RedoX();
            }
            if (may && _activePlayer != _playerX)
            {
                complete = turn.Redo0();
            }
            if (complete)
            {
                CalculateState(turn);
            }
            

            return may;
        }

        private void CalculateState(Turn turn)
        {
            var isWin = false;
            var isDraw = false;
            if (turn != null)
            {
                isWin = turn.IsWin();
                isDraw = turn.IsDraw();
            }
            var isInProcess = IsInProcess();
            if (isInProcess)
            {
                PassTurnToOtherPlayer();
            }
            if (isWin)
            {
                _state = GameState.Win;
            }
            if (!isWin && isDraw)
            {
                _state = GameState.Draw;
            }
        }

        private void PassTurnToOtherPlayer()
        {
            var isX = _activePlayer == _playerX;
            if (isX)
            {
                _activePlayer = _player0;
            }
            if (!isX)
            {
                _activePlayer = _playerX;
            }
        }

        private bool IsInProcess()
        {
            return _state == GameState.InProcess;
        }

        public bool IsWin()
        {
            return _state == GameState.Win;
        }

        public bool IsDraw()
        {
            return _state == GameState.Draw;
        }

        public void Reverse(Turn turn)
        {
            var result = turn != null && turn.Undo();
            if (result && !IsInProcess())
            {
                _state = GameState.InProcess;
            }
            if (result)
            {
                PassTurnToOtherPlayer();
            }
        }

        public string GetActivePlayer()
        {
            var result = string.Empty;
            if (_activePlayer != null)
            {
                result = _activePlayer.GetTitle();
            }
            return result;
        }
    }
}
