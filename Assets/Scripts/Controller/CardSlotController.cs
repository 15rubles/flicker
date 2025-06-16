using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entity.Card;
using Object.Card;
using UnityEngine;
using Utils;

namespace Controller
{
    public class CardSlotController: RegisteredMonoBehaviour
    {
        [SerializeField] private int startCardsInHand = 5;
        
        [SerializeField] 
        private List<CardSlot> cardSlotsPool;

        [SerializeField] 
        private GameObject cardSlotPrefab;

        [SerializeField] private CardController cardController;

        [SerializeField] private Card cardToSpawn;
        
        [SerializeField] private RectTransform discardPile;
        
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
                StartCoroutine(DiscardCardAnimation(card, () => { Destroy(card.gameObject); }));
            }
            cardsInHand = new LinkedList<CardSlot>();
            cardSlotsPool = new List<CardSlot>();
            cardController.CardsPool = new List<CardObject>();
        }
        
        public int DiscardSelectedForMulligan()
        {
            int discardedQuantity = 0;
            var newCardsInHand = new LinkedList<CardSlot>();
            var newCardSlotsPool =  new List<CardSlot>();
            var newCardsPool = new List<CardObject>();
            foreach (var card in CardsInHand)
            {
                if (card.CardObject.IsSelected)
                {
                    StartCoroutine(DiscardCardAnimation(card, () => { Destroy(card.gameObject); }));
                    discardedQuantity++;
                }
                else
                {
                    newCardsInHand.AddLast(card);
                    newCardSlotsPool.Add(card);
                    newCardsPool.Add(card.CardObject);
                }
            }
            cardsInHand = newCardsInHand;
            cardSlotsPool = newCardSlotsPool;
            cardController.CardsPool = newCardsPool;
            
            return discardedQuantity;
        }

        private IEnumerator DiscardCardAnimation(CardSlot cardSlot, Action onComplete = null)
        {
            var card = cardSlot.CardObject.gameObject;
            RectTransform cardRT = card.GetComponent<RectTransform>();
            Vector2 targetPos = UtilitiesFunctions.ConvertAnchoredPosition(discardPile, cardRT);

            Tween tween = cardRT.DOAnchorPos(targetPos,
                ControllerLocator.GetService<BattleController>().AnimationSpeed).SetEase(Ease.InOutExpo);

            yield return tween.WaitForCompletion();
            
            onComplete?.Invoke();
        }
        
        private void SpawnCardSlot(CardSlot cardSlot, Card card)
        {
            var node = cardsInHand.AddLast(cardSlot);
            cardController.SpawnCard(card, cardSlot, node);
            cardSlot.gameObject.SetActive(true);
        }

    }
}