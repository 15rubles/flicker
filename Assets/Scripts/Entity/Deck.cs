using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity
{
    [Serializable]
    public class Deck
    {
        [SerializeField] private DeckSo basicDeck;

        [SerializeField] private List<Card.Card> allCards;

        [SerializeField] private List<Card.Card> cardsInDeck;

        [SerializeField] private List<Card.Card> cardsInDiscard;

        public DeckSo BasicDeck
        {
            get => basicDeck;
            set
            {
                basicDeck = value;
                allCards = basicDeck.AllCards.ToList();
            }
            
        }

        public List<Card.Card> AllCards
        {
            get => allCards;
            set => allCards = value;
        }
        
        public int ShieldValue => basicDeck.ShieldValue;

        public List<Card.Card> CardsInDeck
        {
            get => cardsInDeck;
            set => cardsInDeck = value;
        }

        public List<Card.Card> CardsInDiscard
        {
            get => cardsInDiscard;
            set => cardsInDiscard = value;
        }

        public void DiscardCard(Card.Card card)
        {
            cardsInDiscard.Add(card);
        }
        
        public void ResetDeck()
        {
            cardsInDiscard.Clear();
            cardsInDeck = new List<Card.Card>(allCards);
        }

        public void ShuffleDiscardToDeck()
        {
            cardsInDeck = cardsInDeck.Concat(cardsInDiscard).ToList();
            cardsInDiscard.Clear();
        }
    }
}