using Controller;
using UnityEngine;

namespace Object.Card
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private CardObject cardObject;
        private CardSlotController cardSlotController;

        public CardObject CardObject
        {
            get => cardObject;
            set => cardObject = value;
        }

        public void SetCardGroupController(CardSlotController controller)
        {
            cardSlotController = controller;
        }

        public void Setup(CardObject cardObj)
        {
            cardObject = cardObj;
            cardObject.CardSlot = this;
        }

        public void Reset()
        {
            cardObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void Delete()
        {
            cardSlotController.DeleteCard(this);
        }
        
    }
}