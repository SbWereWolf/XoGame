using System;

namespace XoGame
{
    class TurnFactory
    {
        private readonly IFactorization _factorization;

        public TurnFactory(IFactorization factorization)
        {
            _factorization = factorization;
        }

        public Turn Make(Board board, int tag)
        {
            if (_factorization == null)
            {
                throw new Exception("Factorization not defined for this TurnFactory.");
            }
            var cell = _factorization.Decode(tag);

            return new Turn(board, cell);
        }
    }
}
