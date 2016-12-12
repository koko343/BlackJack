using BlackJack.Core.Entities;
using BlackJack.Core.Enums;
using System;
using System.Collections.Generic;

namespace BlackJack
{
    class Program
    {
        static int currentBet;
        static Deck Deck;
        static Dealer Dealer;
        static Player Player;

        static void Main()
        {
            Deck = Deck.GetInstanse;
            Dealer = Dealer.GetInstanse;
            Player = Player.GetInstanse;

            Console.Title = "♠♥♣♦ Blackjack Game";

            Deck.Shuffle();

            while (Player.Chips > 0)
            {
                DealHand();
                Console.WriteLine("\nTake another hand? y/n\n");
                bool isTakeHandResult = false;
                do
                {
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "y":
                            isTakeHandResult = true;
                            break;
                        case "n":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\nPlease choose y/n\n");
                            Console.WriteLine("\nTake another hand? y/n\n");
                            break;
                    }
                } while (isTakeHandResult == false);

            }

            Console.WriteLine("You Lost! see you next time...");
            Console.ReadLine();
        }

        static void DealHand()
        {
            MakeBet();
            Player.PlayerHend = new List<Card>();
            Player.PlayerHend.Add(Deck.DrawACard());
            Player.PlayerHend.Add(Deck.DrawACard());

            foreach (Card card in Player.PlayerHend)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("[Player]");
            Console.WriteLine("Card 1: {0} of {1}", Player.PlayerHend[0].Face, Player.PlayerHend[0].Suit);
            Console.WriteLine("Card 2: {0} of {1}", Player.PlayerHend[1].Face, Player.PlayerHend[1].Suit);
            Console.WriteLine("Total: {0}\n", Player.PlayerHend[0].Value + Player.PlayerHend[1].Value);

            Dealer.DealerHend = new List<Card>();
            Dealer.DealerHend.Add(Deck.DrawACard());
            Dealer.DealerHend.Add(Deck.DrawACard());

            foreach (Card card in Dealer.DealerHend)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("[Delaer]");
            Console.WriteLine("Card 1: {0} of {1}", Dealer.DealerHend[0].Face, Dealer.DealerHend[1].Suit);
            Console.WriteLine("Card 2: [Hole Card]");
            Console.WriteLine("Total: {0}\n", Dealer.DealerHend[0].Value);

            if (Dealer.DealerHend[0].Face == Face.Ace || Dealer.DealerHend[0].Value == 10)
            {
                Console.WriteLine("Delaer checks if he has blackjack...\n");
                if (Dealer.DealerHend[0].Value + Dealer.DealerHend[1].Value == 21)
                {
                    Console.WriteLine("[Delaer]");
                    Console.WriteLine("Card 1: {0} of {1}", Dealer.DealerHend[0].Face, Dealer.DealerHend[1].Suit);
                    Console.WriteLine("Card 2: {0} of {1}", Dealer.DealerHend[1].Face, Dealer.DealerHend[1].Suit);
                    Console.WriteLine("Total: {0}\n", Dealer.DealerHend[0].Value + Dealer.DealerHend[1].Value);

                    if (Player.PlayerHend[0].Value + Player.PlayerHend[1].Value == 21)
                    {
                        Console.WriteLine("It's a draw");
                    }else
                    {
                        Console.WriteLine("Dealer have a blackjeck");
                        Console.WriteLine("You lost {0}chips", currentBet);
                    }

                    return;
                }
                else
                {
                    Console.WriteLine("Dealer does not have a blackjack, moving on...\n");
                }
            }

            if (Player.PlayerHend[0].Value + Player.PlayerHend[1].Value == 21)
            {
                Console.WriteLine("Blackjack, You Won! ({0} chips)\n", currentBet);
                Player.Chips += currentBet;
                return;
            }

            do
            {
                Console.WriteLine("Please choose a valid option: [(S)tand (H)it]");
                ConsoleKeyInfo userOption = Console.ReadKey(true);
                while (userOption.Key != ConsoleKey.H && userOption.Key != ConsoleKey.S)
                {
                    Console.WriteLine("illegal key. Please choose a valid option: [(S)tand (H)it]");
                    userOption = Console.ReadKey(true);
                }
                Console.WriteLine();

                switch (userOption.Key)
                {
                    case ConsoleKey.H:
                        Player.PlayerHend.Add(Deck.DrawACard());
                        Console.WriteLine("Hitted {0} of {1}", Player.PlayerHend[Player.PlayerHend.Count - 1].Face, Player.PlayerHend[Player.PlayerHend.Count - 1].Suit);
                        int totalCardsValue = 0;
                        foreach (Card card in Player.PlayerHend)
                        {
                            totalCardsValue += card.Value;
                        }
                        Console.WriteLine("Total cards value now: {0}\n", totalCardsValue);
                        if (totalCardsValue > 21)
                        {
                            Console.Write("Busted!\n");
                            Player.Chips -= currentBet;
                            return;
                        }
                        
                        break;

                    case ConsoleKey.S:

                        Console.WriteLine("[Delaer]");
                        Console.WriteLine("Card 1: {0} of {1}", Dealer.DealerHend[0].Face, Dealer.DealerHend[1].Suit);
                        Console.WriteLine("Card 2: {0} of {1}", Dealer.DealerHend[1].Face, Dealer.DealerHend[1].Suit);

                        int dealerCardsValue = 0;
                        foreach (Card card in Dealer.DealerHend)
                        {
                            dealerCardsValue += card.Value;
                        }

                        while (dealerCardsValue < 17)
                        {
                            Dealer.DealerHend.Add(Deck.DrawACard());
                            dealerCardsValue = 0;
                            foreach (Card card in Dealer.DealerHend)
                            {
                                dealerCardsValue += card.Value;
                            }
                            Console.WriteLine("Card {0}: {1} of {2}", Dealer.DealerHend.Count, Dealer.DealerHend[Dealer.DealerHend.Count - 1].Face, Dealer.DealerHend[Dealer.DealerHend.Count - 1].Suit);
                        }
                        dealerCardsValue = 0;
                        foreach (Card card in Dealer.DealerHend)
                        {
                            dealerCardsValue += card.Value;
                        }
                        Console.WriteLine("Total: {0}\n", dealerCardsValue);

                        if (dealerCardsValue > 21)
                        {
                            Console.WriteLine("Dealer bust! You win! ({0} chips)", currentBet);
                            Player.Chips += currentBet;
                            return;
                        }
                        else
                        {
                            int playerCardValue = 0;
                            foreach (Card card in Player.PlayerHend)
                            {
                                playerCardValue += card.Value;
                            }

                            if (dealerCardsValue > playerCardValue)
                            {
                                Console.WriteLine("Dealer has {0} and player has {1}, dealer wins!", dealerCardsValue, playerCardValue);
                                Player.Chips -= currentBet;
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Player has {0} and dealer has {1}, player wins!", playerCardValue, dealerCardsValue);
                                Player.Chips += currentBet;
                                return;
                            }
                        }
                        break;

                    default:
                        break;
                }

            } while (true);
        } 

    static void MakeBet()
    {
        bool isTakeBetResult = false;
        Console.WriteLine("\nPlease make a bet\n");
        Console.WriteLine("\nCurrent balance " + Player.Chips);

        do
        {
            int input = Convert.ToInt32(Console.ReadLine());

            if (input > Player.Chips)
            {
                Console.WriteLine("\nIt's  more than you have\n");
                Console.WriteLine("\nenter correct bet");
            }
            else
            {
                Console.WriteLine("\nThank you for your bet\n");
                currentBet = input;
                isTakeBetResult = true;
            }

        } while (isTakeBetResult == false);
    }
}
}
