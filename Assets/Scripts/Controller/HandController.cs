using System.Collections.Generic;
using Object;
using UnityEngine;

namespace Controller
{
    public class HandController: MonoBehaviour
    {
        [SerializeField] private RectTransform startPoint;
        [SerializeField] private RectTransform endPoint;
        [SerializeField] private List<RectTransform> cardInHand;
        [SerializeField] private float maxDistanceBetweenCards;
        [SerializeField] private float cardWidth;

        public void AddNewCard(CardObject newCard)
        {
            var rectTransform = newCard.gameObject.GetComponent<RectTransform>();
            cardInHand.Add(rectTransform);
            UpdateCardsPosition();
        }
        
        public void UpdateCardsPosition()
        {
            float handLen = (cardInHand.Count - 1) * (cardWidth + maxDistanceBetweenCards);
            float handBoxLen = (endPoint.anchoredPosition - startPoint.anchoredPosition).x;
            if (handLen > handBoxLen)
            {
                handLen = handBoxLen;
            }
            float step = handLen / cardInHand.Count;
            float currentSpawnPoint = (handBoxLen - handLen) / 2 + startPoint.anchoredPosition.x + step / 2;
            
            foreach (var card in cardInHand)
            {
                card.anchoredPosition = new Vector3(currentSpawnPoint,  startPoint.anchoredPosition.y);
                currentSpawnPoint += step;
            }
        }
    }
}