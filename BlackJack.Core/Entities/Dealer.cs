using BlackJack.Core.Entities.Base;
using System.Collections.Generic;

namespace BlackJack.Core.Entities
{
    public class Dealer : EntityBase
    {
        private static Dealer instance;

        private Dealer() { }

        public List<Card> DealerHend { get; set; }

        public static Dealer GetInstanse
        {
            get
            {
                if (instance == null)
                {
                    instance = new Dealer();
                }

                return instance;
            }
        }
    }
}
