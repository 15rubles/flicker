using System.Collections.Generic;
using Entity.Card;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Object
{
    public class CardObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI toughness;
        [SerializeField] private TextMeshProUGUI power;
        [SerializeField] private TextMeshProUGUI cardName;

        private Canvas canvas;

        [SerializeField] private CardSlot cardSlot;

        private LinkedListNode<CardSlot> cardSlotNode;

        private RectTransform rectTransform;

        private RectTransform playCardZone;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        [SerializeField] private Card card;

        public Card Card
        {
            get => card;
            set => card = value;
        }

        public CardSlot CardSlot
        {
            get => cardSlot;
            set => cardSlot = value;
        }

        public LinkedListNode<CardSlot> CardSlotNode
        {
            get => cardSlotNode;
            set => cardSlotNode = value;
        }

        public RectTransform PlayCardZone
        {
            get => playCardZone;
            set => playCardZone = value;
        }

        void OnEnable()
        {
            cardName.text = card.cardName.ToString();
            toughness.text = card.cardStats.Toughness.ToString();
            power.text = card.cardStats.Power.ToString();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (playCardZone.rect.Contains(playCardZone.InverseTransformPoint(eventData.position)))
            {
                Debug.Log("test");
            }
            else
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            // TODO
            // if (cardSlotNode.Next?.Value is not null)
            // {
            //     Debug.Log("1: " + gameObject.name+ " 2: " + cardSlotNode.Next.Value.CardObject.gameObject.name);
            //     if (gameObject.transform.position.x > cardSlotNode.Next.Value.CardObject.transform.position.x)
            //     {
            //         Swap(cardSlotNode.Next.Value);
            //     }
            // }
            // else if (cardSlotNode.Previous?.Value is not null)
            // {
            //     if (gameObject.transform.position.x < cardSlotNode.Previous.Value.CardObject.transform.position.x)
            //     {
            //         Swap(cardSlotNode.Previous.Value);
            //     }
            // }
        }
        
        
        // private void Swap(CardSlot cardSlotToSwap)
        // {
        //     CardSlot cardSlotSafe = cardSlot;
        //     cardSlot = cardSlotToSwap;
        //     cardSlotToSwap.CardObject.CardSlot = cardSlotSafe;
        //     cardSlotSafe.CardObject = cardSlotToSwap.CardObject;
        //     cardSlotToSwap.CardObject = this;
        //     cardSlotSafe.Reset();
        // }
    }
}