using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourInARow.Core;
using Ex02.ConsoleUtils;


namespace FourInARow.ConsoleApp
{
    public class ConsoleUI
    {
        internal const int quit = -1;
        internal void GetRowsAndCols(out int rows, out int cols)
        {
            Console.WriteLine($"Enter the number of rows and columns for the game board. they don't need to be the same");
            Console.WriteLine($"Valid range: {GameManager.MinBoardSize} - {GameManager.MaxBoardSize}");
            Console.Write("Rows: ");
            string input = Console.ReadLine();
            rows = GetPositiveNum(input);
            Console.Write("Columns: ");
            input = Console.ReadLine();
            cols = GetPositiveNum(input);
        }

        internal int GetColumnOrQuitFromUser(string currentPlayerName, bool isAgainstComputer)
        {

            if (!isAgainstComputer)
            {
                Console.Write($"{currentPlayerName}, ");
            }

            Console.WriteLine("choose a column to drop your coin into. or enter 'Q' to quit the game:");

            string input = Console.ReadLine();
            if (input.ToUpper() == "Q")
            {
                return quit;
            }

            return GetPositiveNum(input);
        }

        internal int GetPositiveNum(string input)
        {
            do
            {
                if (int.TryParse(input, out int num) && num > 0)
                {
                    return num;
                }

                InputOutOfBoundMessage();
                input = Console.ReadLine();
            } while (true);
        }

        internal void InputOutOfBoundMessage()
        {
            Console.WriteLine("input is out of bounds. Please enter a different number.");
        }

        internal void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to Four in a Row game!");
            Console.WriteLine();
        }

        internal void ColumnIsFullMessage()
        {
            Console.WriteLine("Column is full. Please enter a different column number.");
        }

        internal void ClearScreenAndPrintBoard(BoardGame board)
        {
            Screen.Clear();
            PrintBoard(board);
        }
        /*A method that prints the board according to the template below:
                  1   2   3   4   5   6     
                |   |   |   |   |   |   |  
                ========================= 
                |   |   |   |   |   |   |
                =========================
                |   |   |   |   |   |   |
                =========================  
                |   |   |   |   |   |   |
                =========================  
                |   |   |   |   |   |   |
                =========================*/

        internal void PrintBoard(BoardGame board)
        {
            for (int col = 1; col <= board.Cols; col++)
            {
                Console.Write($"  {col} ");
            }

            Console.WriteLine();
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Cols; j++)
                {
                    Console.Write($"| {board.GetCell(i, j)} ");
                }

                Console.WriteLine("|");
                if (i < board.Rows - 1)
                {
                    Console.WriteLine(new string('=', board.Cols * 4));
                }
            }

            Console.WriteLine();
        }

        internal void PrintWinMessage(string currentPlayerName, bool isCurrentPlayerComputer)
        {
            if (isCurrentPlayerComputer)
            {
                Console.WriteLine("The computer won. you lose");
            }

            else
            {
                Console.WriteLine($"Congratulations {currentPlayerName}! You won!");

            }
        }

        internal void PrintTieMessage()
        {
            Console.WriteLine("It's a tie!");
        }

        internal void PrintPlayersScore(Player player1, Player player2)
        {
            Console.WriteLine($"{player1.Name} score: {player1.Score}");
            Console.WriteLine($"{player2.Name} score: {player2.Score}");
        }

        internal void PrintInvalidBoardSizeMessage()
        {
            Console.WriteLine("Invalid board size. Please enter a different size.");
        }

        internal void StartOfPlayMessage()
        {
            Console.WriteLine("we will start the game now!");
        }

        internal bool IsAgainstComputer()
        {
            Console.WriteLine("Do you want to play against the computer? (Y/N)");
            string input = Console.ReadLine().ToUpper();

            while (input != "Y" && input != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N.");
                input = Console.ReadLine().ToUpper();
            }

            return input == "Y";
        }
        internal string GetPlayerName()
        {
            Console.WriteLine("Please enter your name:");
            return GetName();
        }

        internal string GetPlayer1Name()
        {
            Console.WriteLine("Please enter the name of player 1:");
            return GetName();
        }

        internal string GetPlayer2Name()
        {
            Console.WriteLine("Please enter the name of player 2:");
            return GetName();
        }

        internal string GetName()
        {
            string name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please enter your name:");
                name = Console.ReadLine();
            }

            return name;
        }

        internal bool PlayAgain()
        {
            Console.WriteLine("Do you want to play again? (Y/N). playing again is with the same board and same players," +
                "who continue to accumulate points");
            string input = Console.ReadLine().ToUpper();

            while (input != "Y" && input != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N.");
                input = Console.ReadLine().ToUpper();
            }

            return input == "Y";
        }

    }
}
