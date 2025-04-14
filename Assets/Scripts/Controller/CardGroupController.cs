using System.Collections.Generic;
using System.Linq;
using Entity.Card;
using Object;
using UnityEngine;

namespace Controller
{
    public class CardGroupController: MonoBehaviour
    {
        [SerializeField] private List<CardSlot> cardsInHand;

        [SerializeField] private int startCardsInHand = 5;
        
        [SerializeField] 
        private List<CardSlot> cardSlotsPool;

        [SerializeField] 
        private GameObject cardSlotPrefab;

        [SerializeField] private CardController cardController;

        [SerializeField] private Card cardToSpawn;
        
        
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
                cardSlotsPool.Add(newCardSlot);
                SpawnCardSlot(newCardSlot, card);
            }
            
        }

        private void SpawnCardSlot(CardSlot cardSlot, Card card)
        {
            cardController.SpawnCard(card, cardSlot);
            cardSlot.gameObject.SetActive(true);
        }

    }
}