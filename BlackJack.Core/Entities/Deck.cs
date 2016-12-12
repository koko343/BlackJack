using BlackJack.Core.Enums;
using System;
using System.Collections.Generic;

namespace BlackJack.Core.Entities
{
    public class Deck
    {
        private List<Card> cards;
        private static Deck instance;
        private static Random rng = new Random();

        private Deck()
        {
            this.Initialize();
        }

        public static Deck GetInstanse
        {
            get
            {
                if (instance == null)
                {
                    instance = new Deck();
                }

                return instance;
            }
        }

        public void Initialize()
        {
            cards = new List<Card>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    Card card = new Card();
                    card.Suit = (Suit)i;
                    card.Face = (Face)j;

                    if (j < 10)
                    {
                        card.Value = j;
                    }
                    else
                    {
                        card.Value = 10;
                    }

                    cards.Add(card);
                }
            }
        }

        public void Shuffle()
        {
            if(cards != null)
            {
                int cardsCount = cards.Count;
                int i = cardsCount;

                while (i>1)
                {
                    i--;
                    int r = rng.Next(1, cardsCount);
                    var value = cards[r];
                    cards[r] = cards[i];
                    cards[i] = value;
                }
            }

        }

        public Card DrawACard()
        {
            if (cards.Count <= 0)
            {
                this.Initialize();
                this.Shuffle();
            }

            Card cardToReturn = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return cardToReturn;
        }

        public int GetAmountOfRemainingCrads()
        {
            return cards.Count;
        }

        public void PrintDeck()
        {
            int i = 1;
            foreach (Card card in cards)
            {
                Console.WriteLine("Card {0}: {1} of {2}. Value: {3}", i, card.Face, card.Suit, card.Value);
                i++;
            }
        }
    }
}
