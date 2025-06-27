using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Deck", menuName = "SOs/Deck", order = 1)]
    public class DeckSo: ScriptableObject
    {
        [SerializeField] private int shieldValue;
        
        [SerializeField] private List<Card.Card> allCards;

        public DeckSo(List<Card.Card> allCards)
        {
            this.allCards = allCards;
        }

        public List<Card.Card> AllCards
        {
            get => allCards;
            set => allCards = value;
        }

        public int ShieldValue
        {
            get => shieldValue;
            set => shieldValue = value;
        }
    }
}