using BlackJack.Core.Entities.Base;
using System.Collections.Generic;

namespace BlackJack.Core.Entities
{
    public class Player : EntityBase
    {
        private static Player instance;

        public int Chips { get; set; }
        public List<Card> PlayerHend { get; set; }

        private Player()
        {
            this.Chips = 1000;
        }

        public static Player GetInstanse
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }

                return instance;
            }
        }
    }
}
