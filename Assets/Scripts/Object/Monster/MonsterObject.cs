using System.Collections.Generic;
using System.Threading.Tasks;
using Controller;
using DG.Tweening;
using Entity.Card.Ability;
using Object.Creature;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Monster
{
    public class MonsterObject: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private MonsterDescription monsterDescription;
        [SerializeField] private MonsterSlot slot;
        [SerializeField] private RectTransform rect;
        private MonsterController monsterController;
        private Canvas canvas;
        
        public RectTransform Rect => rect;

        public MonsterController MonsterController
        {
            get => monsterController;
            set => monsterController = value;
        }

        public MonsterSlot Slot
        {
            get => slot;
            set => slot = value;
        }

        [SerializeField]
        private Entity.Monster.Monster monster;

        private int power;

        public int PreviousPower
        {
            get;
            set;
        }

        public int LastDamage => PreviousPower - power;
        
        public int Power
        {
            get => power;
            set
            {
                PreviousPower = power;
                power = value;

                if (PreviousPower > power)
                {
                    monster.Abilities.UseAbilitiesOfType(AbilityType.DamageDealt, this);
                }
            }
        }

        public void SetPowerWithoutTrigger(int newPower)
        {
            power = newPower;
        }
        
        public Entity.Monster.Monster Monster
        {
            get => monster;
            set
            {
                monster = value;
                Power = value.Power;
                // TODO TODO TODO TODO TODO make may descriptions for each ability
                monsterDescription.Description = monster.Abilities[0].Description;
            }
            
        }
        
        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }
        public void UpdateText()
        {
            cardName.text = monster.MonsterName.ToString();
            powerText.text = Power.ToString();
        }

        private void OnDestroy()
        {
            monster.Abilities.UseAbilitiesOfType(AbilityType.DeathRattle, this);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            monsterDescription.gameObject.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            monsterDescription.gameObject.SetActive(false);
        }
        
        public async Task<bool> Attack(CreatureObj creatureObj)
        {
            return await monster.MonsterAttack.Attack(this, creatureObj);
        }
        
         public void OnDrag(PointerEventData eventData)
        {

            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
            List<MonsterSlot> list = monsterController.MonsterSlots;
            for (int index = 0; index < list.Count; index++)
            {
                RectTransform monsterRect = list[index].GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(monsterRect, eventData.position, eventData.enterEventCamera))
                {
                    // Convert to local space
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        monsterRect, eventData.position, eventData.enterEventCamera, out Vector2 localPoint
                    );

                    UtilitiesFunctions.SetSameParent(slot.gameObject, monsterRect.gameObject);
                    
                    // Check which side
                    if (localPoint.x < 0)
                    {
                        UtilitiesFunctions.MoveBefore(slot.gameObject, monsterRect.gameObject);
                    }
                    else
                    {
                        UtilitiesFunctions.MoveAfter(slot.gameObject, monsterRect.gameObject);
                    }
                    return;
                }
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(slot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(canvas.transform);
        }

        public async Task DestroyMonster()
        {
            Sequence explosion = DOTween.Sequence();
            
            explosion.Append(rect.DOScale(1.5f, 0.1f).SetEase(Ease.OutQuad)) // Quick expand
                     .Join(rect.DOShakePosition(0.2f, strength: 20f, vibrato: 10)) // Shake violently
                     .Append(rect.DOScale(0f, 0.15f).SetEase(Ease.InBack));
            await explosion.AsyncWaitForCompletion();
            monsterController.RemoveSlotFromMonsterSlots(slot);
            Destroy(slot.gameObject);
        }
    }
}