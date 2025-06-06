using System.Collections.Generic;
using Controller;
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

        private RectTransform rectTransform;

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

        public LinkedListNode<CardSlot> CardSlotNode { get; set; }

        public AttackZoneController AttackCardZone { get; set; }

        public BlockZoneController BlockCardZone { get; set; }

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
            if (AttackCardZone.rectTransform.rect.Contains(AttackCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                PlayCard(AttackCardZone);
            }
            else if (BlockCardZone.rectTransform.rect.Contains(BlockCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                PlayCard(BlockCardZone);
            }
            else
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }

        private void PlayCard(ZoneController cardZone)
        {
            Card.CardAbility.UseAbility(Card);
            Delete();
            cardZone.creatureController.SpawnCreature(cardZone, card);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            //todo add hover text when card will be played in hovered zone if stop holding
            // if (AttackCardZone.rect.Contains(AttackCardZone.InverseTransformPoint(eventData.position)))
            // {
            //    
            // }
            // else if (BlockCardZone.rect.Contains(BlockCardZone.InverseTransformPoint(eventData.position)))
            // {
            //     
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

        private void Delete()
        {
            cardSlot.Delete();
        }
    }
}