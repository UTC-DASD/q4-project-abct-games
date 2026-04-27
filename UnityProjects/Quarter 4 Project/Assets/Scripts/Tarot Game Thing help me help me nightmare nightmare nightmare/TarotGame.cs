using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TarotGame;

//public class TarotGame : MonoBehaviour
//{
  /*  public enum GamePhase { Dealing, Bidding, DogExchange, Playing, Scoring }
    public GamePhase currentPhase = GamePhase.Dealing;

    public Deck deck;
    public List<Player> players = new List<Player>();
    public List<Card> chien = new List<Card>();
    public int currentPlayerIndex = 0;
    public List<Card> trick = new List<Card>();
    public Suit leadSuit;
    public int numPlayers = 4;
    public Player taker;
    public enum Bid { Pass, Prise, Garde, GardeSans, GardeContre }
    public Bid contract = Bid.Pass;
    public bool excusePlayed = false;
    public Player excuseOwner;
    public bool waitingForInput = false;
    public string currentPrompt = "";
    public int selectedCardIndex = -1;
    public int humanPlayerIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        deck = new Deck();
        players.Clear();
        chien.Clear();
        for (int i = 0; i < numPlayers; i++)
        {
            players.Add(new Player("Player " + (i + 1)));
        }
        DealCards();
        currentPhase = GamePhase.Bidding;
    }

    void DealCards()
    {
        // Proper dealing based on player count
        int cardsPerPlayer = numPlayers == 3 ? 24 : numPlayers == 4 ? 18 : 15;
        int chienCards = numPlayers == 3 ? 6 : numPlayers == 4 ? 6 : 3;
        for (int i = 0; i < cardsPerPlayer; i++)
        {
            foreach (Player p in players)
            {
                Card c = deck.Draw();
                p.AddCard(c);
                if (c.rank == Rank.Excuse) excuseOwner = p;
            }
        }
        for (int i = 0; i < chienCards; i++)
        {
            chien.Add(deck.Draw());
        }
        foreach (Player p in players)
        {
            p.SortHand();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle game logic based on phase
        switch (currentPhase)
        {
            case GamePhase.Bidding:
                HandleBidding();
                break;
            case GamePhase.DogExchange:
                HandleDogExchange();
                break;
            case GamePhase.Playing:
                HandlePlaying();
                break;
            case GamePhase.Scoring:
                HandleScoring();
                break;
        }

        // Handle human input
        if (waitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) selectedCardIndex = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha1)) selectedCardIndex = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha2)) selectedCardIndex = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha3)) selectedCardIndex = 3;
            else if (Input.GetKeyDown(KeyCode.Alpha4)) selectedCardIndex = 4;
            else if (Input.GetKeyDown(KeyCode.Alpha5)) selectedCardIndex = 5;
            else if (Input.GetKeyDown(KeyCode.Alpha6)) selectedCardIndex = 6;
            else if (Input.GetKeyDown(KeyCode.Alpha7)) selectedCardIndex = 7;
            else if (Input.GetKeyDown(KeyCode.Alpha8)) selectedCardIndex = 8;
            else if (Input.GetKeyDown(KeyCode.Alpha9)) selectedCardIndex = 9;

            if (selectedCardIndex >= 0 && selectedCardIndex < players[humanPlayerIndex].hand.Count)
            {
                Card selectedCard = players[humanPlayerIndex].hand[selectedCardIndex];
                bool isValid = true;
                if (trick.Count > 0)
                {
                    // Must follow suit if possible
                    bool hasSuit = players[humanPlayerIndex].hand.Any(c => c.suit == leadSuit);
                    bool hasTrump = players[humanPlayerIndex].hand.Any(c => c.suit == Suit.Trumps);
                    if (hasSuit && selectedCard.suit != leadSuit)
                    {
                        isValid = false;
                    }
                    else if (!hasSuit && hasTrump && selectedCard.suit != Suit.Trumps && selectedCard.suit != leadSuit)
                    {
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    // Valid selection
                    trick.Add(selectedCard);
                    players[humanPlayerIndex].hand.RemoveAt(selectedCardIndex);
                    if (selectedCard.rank == Rank.Excuse) excusePlayed = true;
                    if (trick.Count == 1) leadSuit = selectedCard.suit;
                    currentPlayerIndex = (currentPlayerIndex + 1) % numPlayers;
                    waitingForInput = false;
                    selectedCardIndex = -1;
                }
                else
                {
                    Debug.Log("Invalid card selection. Must follow suit if possible.");
                    selectedCardIndex = -1;
                }
            }
            else if (selectedCardIndex != -1)
            {
                Debug.Log("Invalid index, try again.");
                selectedCardIndex = -1;
            }
        }
    }

    void HandleBidding()
    {
        // Proper bidding: players bid in order, can raise or pass
        int dealerIndex = 0;
        int bidder = (dealerIndex + 1) % numPlayers;
        Bid currentBid = Bid.Pass;
        taker = null;
        contract = Bid.Pass;
        bool biddingActive = true;
        int passCount = 0;
        int round = 0;
        while (biddingActive && round < numPlayers * 2) // Prevent infinite
        {
            Player p = players[bidder];
            Bid bid = SimulateBid(p, currentBid, passCount, round);
            if (bid > currentBid)
            {
                currentBid = bid;
                taker = p;
                contract = bid;
                passCount = 0;
            }
            else
            {
                passCount++;
            }
            if (passCount == numPlayers)
            {
                biddingActive = false;
            }
            bidder = (bidder + 1) % numPlayers;
            round++;
        }
        if (taker != null)
        {
            currentPhase = GamePhase.DogExchange;
        }
        else
        {
            // All passed, redeal
            InitializeGame();
        }
    }

    Bid SimulateBid(Player p, Bid currentBid, int passCount, int round)
    {
        // Simplified AI: first player bids Prise, others pass unless higher
        if (round == 0) return Bid.Prise;
        if (currentBid == Bid.Prise && Random.value > 0.5f) return Bid.Garde;
        return Bid.Pass;
    }

    void HandleDogExchange()
    {
        // Taker takes chien cards
        foreach (Card c in chien)
        {
            taker.AddCard(c);
            if (c.rank == Rank.Excuse) excuseOwner = taker;
        }
        chien.Clear();
        // Check for Petit and Poignée
        takerHasPetit = taker.hand.Any(c => c.suit == Suit.Trumps && c.rank == Rank.One);
        int trumpCount = taker.hand.Count(c => c.suit == Suit.Trumps);
        if (trumpCount >= 10) takerBonus += 20; // Poignée bonus
        if (takerHasPetit) takerBonus += 10; // Petit bonus if has it
        // Simplified exchange: discard 6 lowest point cards
        List<Card> sortedHand = taker.hand.OrderBy(c => c.points).ToList();
        for (int i = 0; i < 6 && i < sortedHand.Count; i++)
        {
            taker.hand.Remove(sortedHand[i]);
        }
        taker.SortHand();
        currentPhase = GamePhase.Playing;
        currentPlayerIndex = (players.IndexOf(taker) + 1) % numPlayers; // Player after taker leads
    }

    void HandlePlaying()
    {
        if (waitingForInput) return; // Wait for human input

        if (trick.Count == 0)
        {
            // Start new trick: current player leads
            Player leader = players[currentPlayerIndex];
            // NPC lead (human handled in Update)
            Card played = ChooseCardToPlay(leader, true);
            trick.Add(played);
            leadSuit = played.suit;
            if (played.rank == Rank.Excuse) excusePlayed = true;
            currentPlayerIndex = (currentPlayerIndex + 1) % numPlayers;
        }
        else if (trick.Count < numPlayers)
        {
            Player p = players[currentPlayerIndex];
            // NPC play (human handled in Update)
            Card toPlay = ChooseCardToPlay(p, false);
            trick.Add(toPlay);
            p.hand.Remove(toPlay);
            if (toPlay.rank == Rank.Excuse) excusePlayed = true;
            currentPlayerIndex = (currentPlayerIndex + 1) % numPlayers;
        }
        else
        {
            // Trick complete
            int winnerIndex = DetermineTrickWinner();
            float points = CalculateTrickPoints();
            players[winnerIndex].score += points;
            trick.Clear();
            currentPlayerIndex = winnerIndex;
            excusePlayed = false;
            if (players[0].hand.Count == 0)
            {
                currentPhase = GamePhase.Scoring;
            }
        }
    }

    Card ChooseCardToPlay(Player p, bool isLead)
    {
        if (isLead)
        {
            // Lead with any card, simplified: play first
            return p.hand[0];
        }
        // Must follow suit if possible
        List<Card> validCards = p.hand.Where(c => c.suit == leadSuit).ToList();
        if (validCards.Count > 0)
        {
            // Play highest in suit
            return validCards.OrderByDescending(c => (int)c.rank).First();
        }
        // No suit, check for trumps
        validCards = p.hand.Where(c => c.suit == Suit.Trumps).ToList();
        if (validCards.Count > 0)
        {
            // Play lowest trump
            return validCards.OrderBy(c => (int)c.rank).First();
        }
        // Discard lowest card
        return p.hand.OrderBy(c => c.points).First();
    }

    int DetermineTrickWinner()
    {
        Card winningCard = trick[0];
        int winner = 0;
        for (int i = 1; i < trick.Count; i++)
        {
            Card c = trick[i];
            if (c.rank == Rank.Excuse) continue;
            if (IsHigher(c, winningCard, leadSuit))
            {
                winningCard = c;
                winner = i;
            }
        }
        int actualWinner = (currentPlayerIndex - numPlayers + winner) % numPlayers;
        if (actualWinner < 0) actualWinner += numPlayers;
        if (excusePlayed)
        {
            excuseOwner = players[actualWinner];
        }
        return actualWinner;
    }

    bool IsHigher(Card a, Card b, Suit lead)
    {
        if (a.suit == Suit.Trumps && b.suit != Suit.Trumps) return true;
        if (b.suit == Suit.Trumps && a.suit != Suit.Trumps) return false;
        if (a.suit == Suit.Trumps && b.suit == Suit.Trumps)
        {
            if (a.rank == Rank.Excuse) return false;
            if (b.rank == Rank.Excuse) return true;
            return (int)a.rank > (int)b.rank;
        }
        if (a.suit == lead && b.suit == lead)
        {
            return (int)a.rank > (int)b.rank;
        }
        return false;
    }

    float CalculateTrickPoints()
    {
        float points = 0;
        foreach (Card c in trick)
        {
            if (c.rank != Rank.Excuse)
                points += c.points;
        }
        return points;
    }

    void HandleScoring()
    {
        // Calculate final scores with contract
        float totalPoints = numPlayers == 3 ? 51f : numPlayers == 4 ? 56.5f : 41f; // Approximate points needed
        float takerPoints = taker.score + takerBonus;
        if (excuseOwner == taker) takerPoints += 4.5f;
        float multiplier = 1;
        switch (contract)
        {
            case Bid.Garde: multiplier = 2; break;
            case Bid.GardeSans: multiplier = 3; break;
            case Bid.GardeContre: multiplier = 4; break;
        }
        float required = totalPoints * multiplier;
        bool petitAuBout = takerHasPetit && (players.IndexOf(taker) == currentPlayerIndex); // Won last trick
        Debug.Log($"Taker {taker.name} points: {takerPoints}, Required: {required}, Petit au bout: {petitAuBout}");
        if (takerPoints >= required || petitAuBout)
        {
            float difference = takerPoints - totalPoints;
            Debug.Log($"Taker wins by {difference * multiplier} points!");
        }
        else
        {
            float difference = totalPoints - takerPoints;
            Debug.Log($"Defense wins, taker loses {difference * multiplier} points!");
        }
        // Reset for new game
    }*/
//}
