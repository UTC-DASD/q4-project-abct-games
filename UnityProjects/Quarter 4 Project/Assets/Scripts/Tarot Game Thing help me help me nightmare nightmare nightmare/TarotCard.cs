using UnityEngine;



    public enum Suit { Hearts, Diamonds, Clubs, Spades, Trumps }
    public enum Rank { One = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Knight, Queen, King, Excuse }



[System.Serializable]
public class TarotCard
    {
        public Suit suit;
        public Rank rank;
        public float points;

        public TarotCard(Suit s, Rank r)
        {
            suit = s;
            rank = r;
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            if (suit == Suit.Trumps)
            {
                if (rank == Rank.Excuse) points = 4.5f;
                else points = 0.5f;
            }
            else
            {
                switch (rank)
                {
                    case Rank.King: points = 4.5f; break;
                    case Rank.Queen: points = 3.5f; break;
                    case Rank.Knight: points = 2.5f; break;
                    case Rank.Jack: points = 1.5f; break;
                    default: points = (int)rank; break;
                }
            }
        }

        public override string ToString()
        {
            return $"{rank} of {suit}";
        }
    }


