using System;
namespace TicTacToe
{
    public class Game
    {
        // multidimensional jagged array
        private Square[,] _board = new Square[3, 3];
        // private Square[][] _board =
        // {
        //     // consider as rows
        //     new Square[3],
        //     new Square[3],
        //     new Square[3]
        // };

        public void PlayGame()
        {
            Player player = Player.X;
            bool @continue = true;
            while (@continue)
            {
                DisplayBoard();
                @continue = PlayMove(player);
                if (!@continue)
                    return;
                // swap between players X and O
                player = 3 - player;
            }
        }

        public void DisplayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // iterate through every column in row i
                    System.Console.Write($" {this._board[i][j]}");
                }
                System.Console.WriteLine();
            }
        }

        private bool PlayMove(Player player)
        {
            Console.WriteLine("Invalid input quits game");
            Console.Write($"{player}: Enter row comma column, eg. 3,3 > ");

            // read user input
            string input = Console.ReadLine();
            string[] move = input.Split(',');
            if (move.Length != 2) return false;

            // convert user input into row and column int
            int.TryParse(move[0], out int row);
            int.TryParse(move[1], out int col);

            if (row < 1 || row > 3 || col > 3 || col < 1)
            {
                System.Console.WriteLine("Row and Column must be between 1 and 3");
                return false;
            }


            // chained lookup for array of array
            // if (this._board[row - 1][col - 1].Owner != Player.Noone)
            // multidimensional arrays use one lookup instead
            if (this._board[row - 1, col - 1].Owner != Player.Noone)
            {
                System.Console.WriteLine("Space is already occupied");
                return false;
            }

            this._board[row - 1, col - 1] = new Square(player);
            return true;









            // string input = Console.ReadLine();
            // string[] parts = input.Split(',');
            // if (parts.Length != 2)
            //     return false;
            // int.TryParse(parts[0], out int row);
            // int.TryParse(parts[1], out int column);

            // if (row < 1 || row > 3 || column < 1 || column > 3)
            //     return false;

            // if (_board[row - 1][column - 1].Owner != Player.None)
            // {
            //     Console.WriteLine("Square is already occupied");
            //     return false;
            // }

            // _board[row - 1][column - 1] = new Square(player);
            // return true;

        }
    }
}