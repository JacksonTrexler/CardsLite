using System;
using System.Collections.Generic;
using System.Linq;

// Define the Card class
public class Card
{
    public string Suite { get; set; }
    public string Rank { get; set; }

    public Card(string suite, string rank)
    {
        Suite = suite;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suite}";
    }
}

// Define the Deck class
public class Deck
{
    private List<Card> cards = new List<Card>();

    // Define the Suites
    private readonly string[] suites = { "Spades", "Clubs", "Hearts", "Diamonds", "Red", "Black" };
    
    // Define the Ranks
    private readonly string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Joker" };

    public Deck()
    {
        InitializeDeck();
    }

    // Method to initialize the deck
    private void InitializeDeck()
    {
        // Create cards for the four main suites
        foreach (string suite in suites.Take(4)) // Spades, Clubs, Hearts, Diamonds
        {
            foreach (string rank in ranks.Take(13)) // Ace to King
            {
                cards.Add(new Card(suite, rank));
            }
        }

        // Add two Jokers, one Red and one Black
        cards.Add(new Card("Red", "Joker"));
        cards.Add(new Card("Black", "Joker"));
    }

    // Method to shuffle the deck
    public void Shuffle()
    {
        Random random = new Random();
        cards = cards.OrderBy(x => random.Next()).ToList();
    }

    // Method to deal a card
    public Card DealCard()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("No more cards in the deck.");
        }

        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    // Method to show the remaining cards
    public void ShowDeck()
    {
        foreach (var card in cards)
        {
            Console.WriteLine(card);
        }
    }

    public int CardsRemaining()
    {
        return cards.Count;
    }
}

class Program
{
    static void Main()
    {
        // Create a new deck
        Deck deck = new Deck();

        // Shuffle the deck
        deck.Shuffle();

        // Deal a card
        Console.WriteLine("Dealt card: " + deck.DealCard());

        // Show the remaining cards in the deck
        Console.WriteLine("\nRemaining cards in the deck:");
        deck.ShowDeck();

        // Display how many cards are remaining
        Console.WriteLine($"\nCards remaining in deck: {deck.CardsRemaining()}");
    }
}
