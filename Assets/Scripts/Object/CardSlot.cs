using Controller;
using UnityEngine;

namespace Object
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private CardObject cardObject;
        private CardGroupController cardGroupController;

        public void Setup(CardObject cardObj)
        {
            cardObject = cardObj;
            cardObject.CardSlot = this;
        }
    }
}