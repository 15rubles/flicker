using System.Collections.Generic;
using System.Linq;
using Entity.Card;
using Object;
using UnityEngine;

namespace Controller
{
    public class CardController: MonoBehaviour
    { 
        [SerializeField] 
        private List<CardObject> cardsPool;

        [SerializeField] 
        private GameObject cardObjectPrefab;
        
        [SerializeField] 
        private AttackZoneController attackCardZone;
        [SerializeField] 
        private BlockZoneController blockCardZone;
        

        [SerializeField] private CardGroupController cardGroupController;
        
        public void SpawnCard(Card cardData, CardSlot cardSlot, LinkedListNode<CardSlot> cardSlotNode)
        {
            var firstDisabled = cardsPool
                                .FirstOrDefault(card => !card.gameObject.activeInHierarchy);
            if (firstDisabled is not null)
            {
                ChangeCardValuesAndEnable(firstDisabled, cardData, cardSlot, cardSlotNode);
            }
            else
            {
                var newCard = Instantiate(cardObjectPrefab, gameObject.transform.parent);
                var newCardObject = newCard.GetComponent<CardObject>();
                cardsPool.Add(newCardObject);
                ChangeCardValuesAndEnable(newCardObject, cardData, cardSlot, cardSlotNode);
            }
        }

        public void DeleteCard(CardObject cardObj)
        {
            cardsPool.Remove(cardObj);
        }

        private void ChangeCardValuesAndEnable(CardObject cardObj, Card cardData, CardSlot cardSlot,
            LinkedListNode<CardSlot> cardSlotNode)
        {
            cardObj.AttackCardZone = attackCardZone;
            cardObj.BlockCardZone = blockCardZone;
            cardObj.Card = cardData;
            cardObj.CardSlotNode = cardSlotNode;
            cardObj.gameObject.transform.SetParent(cardSlot.transform);
            cardObj.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            cardObj.gameObject.SetActive(true);
            cardSlot.Setup(cardObj);
        }
    }
}