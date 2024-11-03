using System.Dynamic;
using System.Numerics;

namespace FourInARow.Core
{
    public class GameManager
    {
        public BoardGame Board { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player CurrentPlayer { get; set; }
        public bool IsCurrentPlayerComputer { get; set; } = false;
        public const int MinBoardSize = 4;
        public const int MaxBoardSize = 8;

        public GameManager(int rows, int cols, string player1Name, string? player2Name)
        {
            Board = new BoardGame(rows, cols);
            Player1 = new Player(player1Name, 'X');
            Player2 = player2Name == null ? new Player("Computer", 'O') : new Player(player2Name, 'O');
            CurrentPlayer = Player1;
        }

        public static BoardMessage IsBoardSizeInsideBound(int rows, int cols)
        {
            if (rows < MinBoardSize || rows > MaxBoardSize || cols < MinBoardSize || cols > MaxBoardSize)
            {
                return BoardMessage.InvalidBoardSize;
            }
            return BoardMessage.Ongoing;
        }

        public BoardMessage Move(int column)
        {
            int logicCol = column - 1;
            BoardMessage message = Board.Move(logicCol, CurrentPlayer.Coin);

            if (message == BoardMessage.Ongoing)
            {
                switchPlayer();
            }
            else if (message == BoardMessage.Win)
            {
                CurrentPlayer.Score++;
            }

            return message;
        }

        public void switchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
            IsCurrentPlayerComputer = CurrentPlayer.Name == "Computer";
        }

        public string GetCurrentPlayerName()
        {
            return CurrentPlayer.Name;
        }

        public BoardMessage ComputerMove()
        {
            BoardMessage message = Board.ComputerMove(CurrentPlayer.Coin);

            if (message == BoardMessage.Ongoing)
            {
                switchPlayer();
            }
            else if (message == BoardMessage.Win)
            {
                CurrentPlayer.Score++;
            }

            return message;
        }

        public void ClearBoard()
        {
            Board.ClearBoard();
        }
    }
}
