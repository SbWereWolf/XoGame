using System;

namespace XoGame
{
    internal class RandomAi : IPlayer
    {
        private readonly string _name;

        public RandomAi(string name)
        {
            this._name = name;
        }

        public Move Do_move()
        {
            var rand = new Random();
            return new Move { X = rand.Next(0,2 +1), Y = rand.Next(0,2 +1) };
        }

        public string Get_name()
        {
            return _name;
        }
    }
}
