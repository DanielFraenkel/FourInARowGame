using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Core
{
    public enum BoardMessage
    {
        Ongoing,                // The game is still in progress.
        Win,
        Tie,                     // The game ended in a tie.
        ColumnFull,              // The selected column is full.
        OutOfBounds,             // The selected column is out of the board's range.
        InvalidBoardSize         // The board dimensions are outside the valid range (4x4 to 8x8).
    }
}


