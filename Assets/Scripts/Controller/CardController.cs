using System.Collections.Generic;
using System.Linq;
using Entity.Card;
using Object;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class CardController: MonoBehaviour
    { 
        [SerializeField] 
        private List<CardObject> cardsPool;

        [SerializeField] 
        private GameObject cardObjectPrefab;

        [SerializeField] private CardGroupController cardGroupController;
        
        public void SpawnCard(Card cardData, CardSlot cardSlot)
        {
            var firstDisabled = cardsPool
                                .FirstOrDefault(card => !card.gameObject.activeInHierarchy);
            if (firstDisabled is not null)
            {
                ChangeCardValuesAndEnable(firstDisabled, cardData, cardSlot);
            }
            else
            {
                var newCard = Instantiate(cardObjectPrefab, gameObject.transform.parent);
                var newCardObject = newCard.GetComponent<CardObject>();
                cardsPool.Add(newCardObject);
                ChangeCardValuesAndEnable(newCardObject, cardData, cardSlot);
            }
        }

        private void ChangeCardValuesAndEnable(CardObject card, Card cardData, CardSlot cardSlot)
        {
            card.Card = cardData;
            card.gameObject.transform.SetParent(cardSlot.transform);
            card.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            card.gameObject.SetActive(true);
            cardSlot.Setup(card);
        }
    }
}