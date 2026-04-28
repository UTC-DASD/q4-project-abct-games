using System.Collections.Generic;
using UnityEngine;

public class TarotPlayer : MonoBehaviour
{
        public string playerName;
        public List<TarotCard> hand = new List<TarotCard>();
        public float score = 0;

        public TarotPlayer(string n)
        {
            playerName = n;
        }

        public void AddCard(TarotCard c)
        {
            hand.Add(c);
        }

        public TarotCard PlayCard(int index)
        {
            if (index >= 0 && index < hand.Count)
            {
                TarotCard c = hand[index];
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
