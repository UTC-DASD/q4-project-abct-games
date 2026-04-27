using System.Collections.Generic;

namespace TarotGame
{
    public class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();
        public float score = 0;

        public Player(string n)
        {
            name = n;
        }

        public void AddCard(Card c)
        {
            hand.Add(c);
        }

        public Card PlayCard(int index)
        {
            if (index >= 0 && index < hand.Count)
            {
                Card c = hand[index];
                hand.RemoveAt(index);
                return c;
            }
            return null;
        }

        public void SortHand()
        {
            // Sort by suit then rank
            hand.Sort((a, b) =>
            {
                if (a.suit != b.suit) return a.suit.CompareTo(b.suit);
                return a.rank.CompareTo(b.rank);
            });
        }
    }
}