using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Deck", menuName = "SOs/Deck", order = 1)]
    public class Deck: ScriptableObject
    {
        public int StartHealth { get; set; }
        
        [SerializeField]
        private List<Card.Card> allCards;

        private List<Card.Card> cardsInDeck;

        private List<Card.Card> cardsInDiscard;

        public Deck(List<Card.Card> allCards)
        {
            this.allCards = allCards;
        }

        public List<Card.Card> AllCards
        {
            get => allCards;
            set => allCards = value;
        }
        

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

        public void ResetDeck()
        {
            cardsInDiscard.Clear();
            cardsInDeck = new List<Card.Card>(allCards);
        }
    }
}