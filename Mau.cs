using System;
using System.Collections.Generic;
using System.Linq;

public class MauGame
{
    private Deck deck;
    private List<Card> player1Hand;
    private List<Card> player2Hand;
    private Card lastPlayedCard;

    public MauGame()
    {
        deck = new Deck();
        player1Hand = new List<Card>();
        player2Hand = new List<Card>();
        deck.Shuffle();
        DealInitialCards();
        lastPlayedCard = deck.DealCard(); // The first card played
        Console.WriteLine($"Starting card: {lastPlayedCard}");
    }

    // Deal 5 cards to each player
    private void DealInitialCards()
    {
        for (int i = 0; i < 5; i++)
        {
            player1Hand.Add(deck.DealCard());
            player2Hand.Add(deck.DealCard());
        }
    }

    // Check if a card can be played based on the last played card
    private bool CanPlay(Card card)
    {
        return card.Rank == lastPlayedCard.Rank || card.Suite == lastPlayedCard.Suite;
    }

    // Player turn logic
    private void PlayerTurn(List<Card> playerHand, string playerName)
    {
        Console.WriteLine($"\n{playerName}'s turn. Last played card: {lastPlayedCard}");

        // Display player hand
        Console.WriteLine($"{playerName}'s hand:");
        for (int i = 0; i < playerHand.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {playerHand[i]}");
        }

        // Find cards that can be played
        var playableCards = playerHand.Where(CanPlay).ToList();

        if (playableCards.Any())
        {
            Console.WriteLine("\nPlayable cards:");
            for (int i = 0; i < playableCards.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {playableCards[i]}");
            }

            Console.WriteLine("Choose a card to play (1 for first playable card, etc.):");
            int choice = int.Parse(Console.ReadLine()) - 1;
            lastPlayedCard = playableCards[choice];
            playerHand.Remove(lastPlayedCard);
            Console.WriteLine($"{playerName} played {lastPlayedCard}");
        }
        else
        {
            // Draw a card if no playable cards
            Console.WriteLine($"{playerName} has no playable cards. Drawing a card.");
            var drawnCard = deck.DealCard();
            playerHand.Add(drawnCard);
            Console.WriteLine($"{playerName} drew {drawnCard}");
        }
    }

    // Check if a player has won
    private bool CheckForWinner(List<Card> playerHand, string playerName)
    {
        if (playerHand.Count == 0)
        {
            Console.WriteLine($"{playerName} wins the game!");
            return true;
        }
        return false;
    }

    // Main game loop
    public void PlayGame()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            // Player 1's turn
            PlayerTurn(player1Hand, "Player 1");
            gameOver = CheckForWinner(player1Hand, "Player 1");
            if (gameOver) break;

            // Player 2's turn
            PlayerTurn(player2Hand, "Player 2");
            gameOver = CheckForWinner(player2Hand, "Player 2");
        }
    }
}
