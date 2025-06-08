using System.Collections.Generic;
using System.Linq;
using Entity.Card;
using Object;
using UnityEngine;

namespace Controller
{
    public class CardGroupController: MonoBehaviour
    {
        [SerializeField] private int startCardsInHand = 5;
        
        [SerializeField] 
        private List<CardSlot> cardSlotsPool;

        [SerializeField] 
        private GameObject cardSlotPrefab;

        [SerializeField] private CardController cardController;

        [SerializeField] private Card cardToSpawn;
        
        private LinkedList<CardSlot> cardsInHand = new LinkedList<CardSlot>();

        public LinkedList<CardSlot> CardsInHand
        {
            get => cardsInHand;
            set => cardsInHand = value;
        }

        public void SpawnCard(Card card)
        {
            var firstDisabled = cardSlotsPool
                .FirstOrDefault(cardSlot => !cardSlot.gameObject.activeInHierarchy);
            if (firstDisabled is not null)
            {
                SpawnCardSlot(firstDisabled, card);
            }
            else
            {
                var newCardSlotObject = Instantiate(cardSlotPrefab, gameObject.transform);
                var newCardSlot = newCardSlotObject.GetComponent<CardSlot>();
                newCardSlot.SetCardGroupController(this);
                cardSlotsPool.Add(newCardSlot);
                SpawnCardSlot(newCardSlot, card);
            }
            
        }

        public void DeleteCard(CardSlot slot)
        {
            cardController.DeleteCard(slot.CardObject);
            cardSlotsPool.Remove(slot);
            Destroy(slot.gameObject);
        }
        
        private void SpawnCardSlot(CardSlot cardSlot, Card card)
        {
            var node = cardsInHand.AddLast(cardSlot);
            cardController.SpawnCard(card, cardSlot, node);
            cardSlot.gameObject.SetActive(true);
        }

    }
}