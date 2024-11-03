using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FourInARow.Core;

namespace FourInARow.ConsoleApp
{
    public class GameController
    {
        private readonly ConsoleUI ui;
        private GameManager gameManager;
        private int rows;//don't have property because assigned once via out parameter in ui.GetRowsAndCols method
        private int cols;//don't have property because assigned once via out parameter in ui.GetRowsAndCols method
        private char isQuit = 'N';
        private bool playAgain = false;
        internal int ColToDrop { get; set; }
        internal string Player1Name { get; set; }
        internal string? Player2Name { get; set; } = null;
        internal bool IsAgainstComputer { get; set; } 
        internal BoardMessage Message { get; set; }
        public GameController()//לבדוק מה קורה פה
        {
            ui = new ConsoleUI();
        }

        public void Run()
        {
            ui.PrintWelcomeMessage();

            do
            {
                ui.GetRowsAndCols(out rows, out cols);
                Message = GameManager.IsBoardSizeInsideBound(rows, cols);
                if (Message == BoardMessage.InvalidBoardSize)
                {
                    ui.InputOutOfBoundMessage();
                }
            } while (Message == BoardMessage.InvalidBoardSize);

            IsAgainstComputer = ui.IsAgainstComputer();
            //לא בטוח שחכם כי הגיים קונטרולר מחליט איך קוראים למחשב. כלומר מישהו יכל לממש פה שגם אם זה נגד המחשב, עדיין
            //ינתן כאן שם לשחקן השני, ואז בגיים מאנאג'ר יינתן שם למחשב שהיו איי החליט, ולא הלוגיקה. לא טוב. כאילו, בלוגיקה
            //עשיתי שאם השם של השחקן השני הוא נאל, אז זה אומר משחק מול המחשב, ואז שמתי שם לשחקן השני את השם "מחשב". ופה למעלה עשיתי
            //שהשם של השחקן השני יכול להיות נולאבל. לא יודע אם כל זה טוב

            //אני יכול אולי שבקונסטרקטור של מאנאג'ר לא יהיה לקבל את השם של השני, יהיה רק אפשרות של סט שלו. ואז היו איי יחליט אם 
            //ירצה לבקש מהמשתמש שם של השני ולשים בסט. והדיפולט של השם השני במאנאג'ר יהיה מחשב
            if (IsAgainstComputer)
            {
                Player1Name = ui.GetPlayerName();
            }

            else
            {
                Player1Name = ui.GetPlayer1Name();
                Player2Name = ui.GetPlayer2Name();
            }

            gameManager = new GameManager(rows, cols, Player1Name, Player2Name);

            do
            {
                ui.StartOfPlayMessage();
                ui.PrintBoard(gameManager.Board);
                do
                {
                    if (gameManager.IsCurrentPlayerComputer)
                    {
                        Message = gameManager.ComputerMove();
                    }

                    else
                    {
                        ColToDrop = ui.GetColumnOrQuitFromUser(gameManager.GetCurrentPlayerName(), IsAgainstComputer);
                        if (ColToDrop == ConsoleUI.quit)
                        {
                            gameManager.switchPlayer();
                            gameManager.CurrentPlayer.Score++;
                            gameManager.IsCurrentPlayerComputer = false;//לא אידיאלי ואני צריך לעשות את זה יותר אובג'קט אוריינטד. מה שקורה זה שאם
                            //לא אעשה את זה אז אם השחקן שפרש ירצה לעשות עוד סיבוב, במחלקת גיים מאנאג'ר האיז קורנט פלייר קומפיוטר יהיה טרו, והמחשב יתחיל. מה שלא טוב
                            Message = BoardMessage.Win;
                        }

                        else
                        {
                            Message = gameManager.Move(ColToDrop);
                        }
                    }


                    if (Message == BoardMessage.Ongoing || Message == BoardMessage.Win || Message == BoardMessage.Tie)
                    {
                        ui.ClearScreenAndPrintBoard(gameManager.Board);
                    }

                    switch (Message)
                    {
                        case BoardMessage.OutOfBounds:
                            ui.InputOutOfBoundMessage();
                            break;

                        case BoardMessage.ColumnFull:
                            ui.ColumnIsFullMessage();
                            break;

                        case BoardMessage.Win:
                            ui.PrintWinMessage(gameManager.GetCurrentPlayerName(), gameManager.IsCurrentPlayerComputer);
                            break;

                        case BoardMessage.Tie:
                            ui.PrintTieMessage();
                            break;

                        case BoardMessage.Ongoing:
                            break;
                    }

                    if (Message == BoardMessage.Win || Message == BoardMessage.Tie)
                    {
                        ui.PrintPlayersScore(gameManager.Player1, gameManager.Player2);
                        if (ui.PlayAgain())
                        {
                            gameManager.CurrentPlayer = gameManager.Player1;
                            gameManager.ClearBoard();
                            playAgain = true;
                        }
                    }

                } while (Message == BoardMessage.Ongoing || Message == BoardMessage.ColumnFull || Message == BoardMessage.OutOfBounds);
            } while (playAgain);



        }



    }

}
