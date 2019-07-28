using System;

namespace TicTacToe
{
    public enum Player
    {
        Noone = 0,
        X,
        O
    }
    public struct Square
    {
        public Player Owner { get; }

        public Square(Player owner)
        {
            this.Owner = owner;
        }

        public override string ToString()
        {
            switch (this.Owner)
            {
                case Player.Noone:
                    return ".";
                case Player.O:
                    return "O";
                case Player.X:
                    return "X";
                default:
                    throw new Exception("Invalid Player State");
            }
        }
        // public override int GetHashCode() => HashCode.Combine(this.Owner, this.Name);

    }
}