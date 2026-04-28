using System.Collections.Generic;
using UnityEngine;



public class TarotDeck : MonoBehaviour
{
      
        public List<TarotCard> cards = new List<TarotCard>();
        
        public void Start()
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
                    cards.Add(new TarotCard(s, r));
                }
            }
            // Add trumps 1-21
            for (int i = 1; i <= 21; i++)
                cards.Add(new TarotCard(Suit.Trumps, (Rank)i));
            // Add Excuse
            cards.Add(new TarotCard(Suit.Trumps, Rank.Excuse));
        }

        private void Shuffle()
        {
            // Simple shuffle
            for (int i = 0; i < cards.Count; i++)
            {
                int j = Random.Range(i, cards.Count);
                TarotCard temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public TarotCard Draw()
        {
            if (cards.Count > 0)
            {
                TarotCard c = cards[0];
                cards.RemoveAt(0);
                return c;
            }
            return null;
        }
    }

