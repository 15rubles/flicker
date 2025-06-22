using Controller;
using Object.Deck;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Shop
{
    public class CardInRemoveObj : CardVisualObj, IPointerClickHandler
    {
        [SerializeField] private GameObject removeButton;
        private GameObject gridGroup;

        public void SetCard(Entity.Card.Card card, GameObject girdGroupGo)
        {
            gridGroup = girdGroupGo;
            Card = card;
        }
        
        public void RemoveCard()
        {
            gridGroup.SetActive(false);
            var deck = ControllerLocator.GetService<BattleController>().Deck;
            deck.AllCards.Remove(Card);
            Destroy(gameObject);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            removeButton.SetActive(!removeButton.activeSelf);
        }
    }
}