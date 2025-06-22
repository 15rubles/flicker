using Controller;
using UnityEngine;
using Utils;

namespace Object.Shop
{
    public class FullDeckForRemoveObj: MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform deckGrid;
        [SerializeField] private GameObject gridGroup;

        void UpdateDeck()
        {
            var deck = ControllerLocator.GetService<BattleController>().Deck;
            foreach (Transform card in deckGrid)
            {
                Destroy(card.gameObject);
            }
            
            foreach (var card in deck.AllCards)
            {
                var cardInDeck = Instantiate(cardPrefab, deckGrid).GetComponent<CardInRemoveObj>();
                cardInDeck.SetCard(card, gridGroup);
            }
        }
        public void TurnOn()
        {
            UpdateDeck();
            gridGroup.SetActive(true);
        }
    }
}