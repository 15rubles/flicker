using System.Collections.Generic;
using System.Linq;
using Entity.Card;
using Object.Card;
using UnityEngine;

namespace Controller
{
    public class CardSlotController: MonoBehaviour
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
            var firstDisabled = cardsInHand
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
            cardsInHand.Remove(slot);
            Destroy(slot.gameObject);
        }

        public void DiscardHand()
        {
            foreach (var card in CardsInHand)
            {
                Destroy(card.gameObject);
            }
            cardsInHand = new LinkedList<CardSlot>();
            cardSlotsPool = new List<CardSlot>();
            cardController.CardsPool = new List<CardObject>();
        }
        
        private void SpawnCardSlot(CardSlot cardSlot, Card card)
        {
            var node = cardsInHand.AddLast(cardSlot);
            cardController.SpawnCard(card, cardSlot, node);
            cardSlot.gameObject.SetActive(true);
        }

    }
}