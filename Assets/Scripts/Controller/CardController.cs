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

        [SerializeField] private HandController handController;
        
        public void SpawnCard(Card cardData)
        {
            var firstDisabled = cardsPool
                                .FirstOrDefault(card => !card.gameObject.activeInHierarchy);
            if (firstDisabled is not null)
            {
                ChangeCardValuesAndEnable(firstDisabled, cardData);
            }
            else
            {
                var newCard = Instantiate(cardObjectPrefab, gameObject.transform.parent);
                newCard.SetActive(false);
                var newCardObject = newCard.GetComponent<CardObject>();
                cardsPool.Add(newCardObject);
                ChangeCardValuesAndEnable(newCardObject, cardData);
            }
        }

        private void ChangeCardValuesAndEnable(CardObject card, Card cardData)
        {
            card.Card = cardData;
            card.gameObject.SetActive(true);
            handController.AddNewCard(card);
        }
    }
}