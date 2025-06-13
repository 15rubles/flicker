using System.Collections.Generic;
using Controller;
using Entity.Card.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Object.Card
{
    public class CardObject : MonoBehaviour, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI toughness;
        [SerializeField] private TextMeshProUGUI power;
        [SerializeField] private TextMeshProUGUI cardName;

        private Canvas canvas;

        [SerializeField] private CardSlot cardSlot;

        private RectTransform rectTransform;

        [SerializeField] private Vector2 selectedPosition;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        [SerializeField] private Entity.Card.Card card;

        public Entity.Card.Card Card
        {
            get => card;
            set => card = value;
        }

        public CardSlot CardSlot
        {
            get => cardSlot;
            set => cardSlot = value;
        }
        
        public bool IsSelected { get; set; }

        public LinkedListNode<CardSlot> CardSlotNode { get; set; }

        public AttackZoneController AttackCardZone { get; set; }

        public BlockZoneController BlockCardZone { get; set; }
        
        public void UpdateText()
        {
            cardName.text = card.cardName.ToString();
            power.text = card.power.ToString();
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (StateController.IsMulliganStep)
                return;
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
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        private void PlayCard(ZoneController cardZone)
        {
            cardZone.creatureController.SpawnCreature(cardZone, card);
            if (Card.cardAbility.AbilityType == AbilityType.EnterTheBattlefield)
            {
                Card.cardAbility.UseAbility(this);
            }
            Delete();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (StateController.IsMulliganStep) 
                return;
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

        public void Select()
        {
            if (!StateController.IsMulliganStep)
                return;
                
            IsSelected = !IsSelected;

            rectTransform.anchoredPosition = 
                IsSelected 
                    ? selectedPosition 
                    : Vector2.zero;
        }

        private void Delete()
        {
            cardSlot.Delete();
        }
    }
}