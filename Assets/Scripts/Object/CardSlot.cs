using Controller;
using UnityEngine;

namespace Object
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private CardObject cardObject;
        private CardGroupController cardGroupController;

        public CardObject CardObject
        {
            get => cardObject;
            set => cardObject = value;
        }

        public void SetCardGroupController(CardGroupController controller)
        {
            cardGroupController = controller;
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
            cardGroupController.DeleteCard(this);
        }
        
    }
}