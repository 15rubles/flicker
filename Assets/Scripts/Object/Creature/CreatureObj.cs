using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

        public ZoneController Zone { get; set; }
        
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
            if (card.isDraggable)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!card.isDraggable) return;

            transform.SetParent(slot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            if (AttackCardZone.rectTransform.rect.Contains(
                    AttackCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                BlockCardZone.RemoveCreature(slot);
                AttackCardZone.AddCreature(slot);
                Zone = AttackCardZone;
            }
            else if (BlockCardZone.rectTransform.rect.Contains(
                         BlockCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                AttackCardZone.RemoveCreature(slot);
                BlockCardZone.AddCreature(slot);
                Zone = BlockCardZone;
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (card.isDraggable)
            {
                transform.SetParent(canvas.transform);
            }
        }
    }
}