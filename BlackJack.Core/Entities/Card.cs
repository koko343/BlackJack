using BlackJack.Core.Entities.Base;
using BlackJack.Core.Enums;

namespace BlackJack.Core.Entities
{
    public class Card
    {
        public Suit Suit { get; set; }

        public Face Face { get; set; }

        public int Value { get; set; }
    }
}
