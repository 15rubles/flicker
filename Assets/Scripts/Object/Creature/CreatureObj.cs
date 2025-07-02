using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controller;
using DG.Tweening;
using Entity.Card;
using Entity.Card.Ability;
using Object.Monster;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Creature
{
    public class CreatureObj : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI cardName;
        
        [SerializeField] private CreatureSlot slot;
        
        [SerializeField] private CardHint cardHintLeft;
        [SerializeField] private CardHint cardHintRight;

        public CreatureSlot Slot
        {
            get => slot;
            set => slot = value;
        }
        public int Power { get; set; }

        private Entity.Card.Card card;
        
        private Canvas canvas;
        private RectTransform rectTransform;

        public RectTransform Rect => rectTransform;

        public Entity.Card.Card Card
        {
            get => card;
            set
            {
                card = value;
                if (card.power <= 0)
                {
                    DestroyCreature();
                    return;
                }
                Power = value.power;
                cardHintLeft.Card = card;
                cardHintRight.Card = card;
            }
        }
        
        public AttackZoneController AttackCardZone { get; set; }
        
        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        public void UpdateText()
        {
            powerText.text = Power.ToString();
            cardName.text = Card.cardName;
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
                    }
                    else
                    {
                        UtilitiesFunctions.MoveAfter(slot.gameObject, rect.gameObject);
                    }
                    return;
                }
            }
            
            if (AttackCardZone.rectTransform.rect.Contains(
                    AttackCardZone.rectTransform.InverseTransformPoint(eventData.position)))
            {
                slot.transform.SetParent(AttackCardZone.transform);
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (card.CheckKeyword(KeywordType.Untouchable)) return;

            transform.SetParent(slot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        private List<GameObject> GetListOfAllCardSlotsGOs()
        {
            return AttackCardZone.CreatureGOs.ToList();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (card.CheckKeyword(KeywordType.Untouchable)) return;

            transform.SetParent(canvas.transform);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (UtilitiesFunctions.IsRectTransformFullyOnScreen(cardHintRight.Boundaries))
            {
                cardHintRight.gameObject.SetActive(true);
            }
            else
            {
                cardHintLeft.gameObject.SetActive(true);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            cardHintLeft.gameObject.SetActive(false);
            cardHintRight.gameObject.SetActive(false);
        }

        public async Task<bool> Attack(MonsterObject monsterObject)
        {
           return await card.cardAttack.Attack(this, monsterObject);
        }
        
        public async Task DestroyCreature()
        {
            Sequence explosion = DOTween.Sequence();
            
            explosion.Append(Rect.DOScale(1.5f, 0.1f).SetEase(Ease.OutQuad)) // Quick expand
                     .Join(Rect.DOShakePosition(0.2f, strength: 20f, vibrato: 10)) // Shake violently
                     .Append(Rect.DOScale(0f, 0.15f).SetEase(Ease.InBack));
            await explosion.AsyncWaitForCompletion();
            Destroy(slot.gameObject);
        }
    }
}