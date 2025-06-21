using Controller;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Deck
{
    public class FullDeckObj: MonoBehaviour, IPointerClickHandler
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
                var cardInDeck = Instantiate(cardPrefab, deckGrid).GetComponent<CardVisualObj>();
                cardInDeck.Card = card;
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            UpdateDeck();
            gridGroup.SetActive(true);
        }
    }
}