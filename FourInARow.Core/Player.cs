using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Core
{
    public class Player

    {
        public string Name { get; }//בשניהם אני רוצה לעשות ריד אונלי. האם אפשר לעשות את זה על הפרופרטי
        internal char Coin { get; }
        public int Score { get; set; } = 0;
        internal Player(string name, char coin)
        {
            Name = name;
            Coin = coin;
        }
    }
}
