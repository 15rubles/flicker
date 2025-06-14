using System.Collections.Generic;
using System.Linq;
using Controller;
using Entity.Card.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Creature
{
    public class CreatureObj : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI cardName;
        
        [SerializeField] private CreatureSlot slot;

        public CreatureSlot Slot
        {
            get => slot;
            set => slot = value;
        }
        public int Power { get; set; }

        private Entity.Card.Card card;
        
        private Canvas canvas;
        private RectTransform rectTransform;
        
        public Entity.Card.Card Card
        {
            get => card;
            set
            {
                card = value;
                Power = value.power;
            }
        }

        // public ZoneController Zone { get; set; }
        
        public AttackZoneController AttackCardZone { get; set; }

        public BlockZoneController BlockCardZone { get; set; }
        
        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        public void UpdateText()
        {
            powerText.text = Power.ToString();
            cardName.text = Card.cardName.ToString();
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (card.CheckKeyword(KeywordType.Untouchable)) return;

            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            List<GameObject> list = GetListOfAllCardSlotsGOs();
            for (int index = 0; index < list.Count; index++)
            {
                RectTransform rect = list[index].GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(rect, eventData.position, eventData.enterEventCamera))
                {
                    // Convert to local space
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        rect, eventData.position, eventData.enterEventCamera, out Vector2 localPoint
                    );

                    UtilitiesFunctions.SetSameParent(slot.gameObject, rect.gameObject);
                    
                    // Check which side
                    if (localPoint.x < 0)
                    {
                        UtilitiesFunctions.MoveBefore(slot.gameObject, rect.gameObject);
                        // indexToInsert = list[index].Index;
                    }
                    else
                    {
                        UtilitiesFunctions.MoveAfter(slot.gameObject, rect.gameObject);
                        // indexToInsert = list[index].Index + 1;
                    }
                    // zoneToInsert = list[index].Zone;
                    return;
                }
            }
            
            if (AttackCardZone.rectTransform.rect.Contains(
                    AttackCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                slot.transform.SetParent(AttackCardZone.transform);
            }
            else if (BlockCardZone.rectTransform.rect.Contains(
                         BlockCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                slot.transform.SetParent(BlockCardZone.transform);
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (card.CheckKeyword(KeywordType.Untouchable)) return;

            transform.SetParent(slot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            // zoneToInsert.CreatureSlots.Insert(indexToInsert, slot);
            // Zone = zoneToInsert;
        }

        private List<GameObject> GetListOfAllCardSlotsGOs()
        {
            return AttackCardZone.CreatureGOs.Concat(BlockCardZone.CreatureGOs).ToList();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (card.CheckKeyword(KeywordType.Untouchable)) return;

            transform.SetParent(canvas.transform);
            // Zone.CreatureSlots.Remove(slot);
        }
    }
}