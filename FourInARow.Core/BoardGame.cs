using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Core
{
    public class BoardGame
    {
        private readonly char[,] board;//איך זה ריד אונלי אבל במתודה למטה אני יכול לשים ערך חדש?
        private readonly int rows;
        private readonly int cols;
        private readonly int totalSquareNum;
        private int usedSquares = 0;

        public BoardGame(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            board = new char[rows, cols];
            totalSquareNum = rows * cols;
            InitializeBoard();
        }

        internal BoardGame Board { get; }

        public int Rows => rows;
        public int Cols => cols;

        private void InitializeBoard()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    board[i, j] = ' '; // Empty cell
        }

        internal BoardMessage Move(int column, char coin)
        {
            BoardMessage message = BoardMessage.Ongoing;

            if (IsColumnOutOfBound(column))
            {
                message = BoardMessage.OutOfBounds;
            }

            else if (IsColumnFull(column))
            {
                message = BoardMessage.ColumnFull;
            }

            else
            {
                message = DropCoin(column, coin);
            }

            return message;
        }

        public BoardMessage ComputerMove(char coin)
        {
            Random random = new Random();
            int column;

            do
            {
                column = random.Next(1, cols + 1) - 1;
            } while (IsColumnFull(column));

            return DropCoin(column, coin);

        }

        private BoardMessage DropCoin(int column, char coin)
        {
            int row = rows - 1;

            while (row >= 0 && board[row, column] != ' ')
            {
                row--;
            }

            board[row, column] = coin;
            usedSquares++;

            return CheckForWin(row, column) ? BoardMessage.Win : usedSquares == totalSquareNum ? BoardMessage.Tie : BoardMessage.Ongoing;

        }

        private bool IsColumnFull(int column)
        {
            return board[0, column] != ' ';
        }

        private bool IsBoardFull()
        {
            return usedSquares == totalSquareNum;
        }

        private bool CheckForWin(int row, int col)
        {
            return CheckHorizontal(row, col) || CheckVertical(row, col) || CheckDiagonal(row, col);
        }

        private bool CheckHorizontal(int row, int col)
        {
            int count = 1;
            char coin = board[row, col];

            // Check left
            for (int i = col - 1; i >= 0 && board[row, i] == coin; i--)
                count++;

            // Check right
            for (int i = col + 1; i < cols && board[row, i] == coin; i++)
                count++;

            return count >= 4;
        }

        private bool CheckVertical(int row, int col)
        {
            int count = 1;
            char coin = board[row, col];

            // Check down
            for (int i = row + 1; i < rows && board[i, col] == coin; i++)
                count++;

            return count >= 4;
        }

        private bool CheckDiagonal(int row, int col)
        {
            return CheckDiagonalLeft(row, col) || CheckDiagonalRight(row, col);
        }

        private bool CheckDiagonalLeft(int row, int col)
        {
            int count = 1;
            char coin = board[row, col];

            // Check up-left
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0 && board[i, j] == coin; i--, j--)
                count++;

            // Check down-right
            for (int i = row + 1, j = col + 1; i < rows && j < cols && board[i, j] == coin; i++, j++)
                count++;

            return count >= 4;
        }

        private bool CheckDiagonalRight(int row, int col)
        {
            int count = 1;
            char coin = board[row, col];

            // Check up-right
            for (int i = row - 1, j = col + 1; i >= 0 && j < cols && board[i, j] == coin; i--, j++)
                count++;

            // Check down-left
            for (int i = row + 1, j = col - 1; i < rows && j >= 0 && board[i, j] == coin; i++, j--)
                count++;

            return count >= 4;
        }

        public char GetCell(int row, int col)
        {
            return board[row, col];
        }

        internal void ClearBoard()
        {
            InitializeBoard();
            usedSquares = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private bool IsColumnOutOfBound(int column)
        {
            return column < 0 || column >= cols;
        }


    }
}
