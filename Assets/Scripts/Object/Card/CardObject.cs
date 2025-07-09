using System.Collections.Generic;
using Controller;
using Entity.Card.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Card
{
    public class CardObject : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI power;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI description;

        private Canvas canvas;

        [SerializeField] private CardSlot cardSlot;

        private RectTransform rectTransform;
        private BattleController battleController;

        [SerializeField] private Vector2 selectedPosition;

        [SerializeField] private GameObject keywordExplainPrefab;

        [SerializeField] private GameObject keywordExplainPool;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = gameObject.GetComponent<RectTransform>();
            battleController = ControllerLocator.GetService<BattleController>();
            ShieldZone = battleController.ShiedZone;
        }

        [SerializeField] private Entity.Card.Card card;

        public Entity.Card.Card Card
        {
            get => card;
            set
            {
                card = value;
                foreach (Transform child in keywordExplainPool.transform) {
                    Destroy(child.gameObject);
                }
                foreach (var keyword in card.keywords)
                {
                    var newKeyword = Instantiate(keywordExplainPrefab.gameObject, keywordExplainPool.transform);
                    newKeyword.GetComponent<KeywordExplain>().UpdateText(keyword);
                    newKeyword.SetActive(false);
                }
                UpdateText();
            }
        }

        public CardSlot CardSlot
        {
            get => cardSlot;
            set => cardSlot = value;
        }
        
        public bool IsSelected { get; set; }

        public LinkedListNode<CardSlot> CardSlotNode { get; set; }

        public AttackZoneController AttackCardZone { get; set; }

        public RectTransform ShieldZone { get; set; }
        
        private void UpdateText()
        {
            cardName.text = card.cardName;
            power.text = card.power <= 0 ? "" : card.power.ToString();
            description.text = card.Description;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (StateController.IsMulliganStep)
                return;
            if (AttackCardZone.rectTransform.rect.Contains(AttackCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                PlayCard();
            }
            else if (ShieldZone.rect.Contains(ShieldZone.InverseTransformPoint(eventData.position)))
            {
                card.shieldCardAbility.UpdateShieldValue(card);
                Discard();
            }
            else
            {
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        private void PlayCard()
        {
            bool isNeededToBeDeleted = AttackCardZone.creatureController.SpawnCreature(card);
            
            if (isNeededToBeDeleted)
                Delete();
        }

        private void AddShield()
        {
            
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
        
        private void Discard()
        {
            battleController.Deck.DiscardCard(card);
            Delete();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (Transform child in keywordExplainPool.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (Transform child in keywordExplainPool.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}