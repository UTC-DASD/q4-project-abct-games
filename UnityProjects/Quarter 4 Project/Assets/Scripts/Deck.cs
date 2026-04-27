using UnityEngine;
using System.Collections.Generic;

namespace TarotGame
{
    public class Deck
    {
        public List<Card> cards = new List<Card>();

        public Deck()
        {
            CreateDeck();
            Shuffle();
        }

        private void CreateDeck()
        {
            // Add suit cards
            foreach (Suit s in System.Enum.GetValues(typeof(Suit)))
            {
                if (s == Suit.Trumps) continue;
                for (int i = 1; i <= 14; i++)
                {
                    Rank r = (Rank)i;
                    cards.Add(new Card(s, r));
                }
            }
            // Add trumps 1-21
            for (int i = 1; i <= 21; i++)
                cards.Add(new Card(Suit.Trumps, (Rank)i));
            // Add Excuse
            cards.Add(new Card(Suit.Trumps, Rank.Excuse));
        }

        private void Shuffle()
        {
            // Simple shuffle
            for (int i = 0; i < cards.Count; i++)
            {
                int j = Random.Range(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card Draw()
        {
            if (cards.Count > 0)
            {
                Card c = cards[0];
                cards.RemoveAt(0);
                return c;
            }
            return null;
        }
    }
}