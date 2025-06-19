using System;
using System.Threading.Tasks;
using Controller;
using DG.Tweening;
using Entity.Card.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Object.Monster
{
    public class MonsterObject: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private MonsterDescription monsterDescription;
        [SerializeField] private MonsterSlot slot;
        [SerializeField] private RectTransform rect;
        private MonsterController monsterController;

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

        public int Power { get; set; }

        public Entity.Monster.Monster Monster
        {
            get => monster;
            set
            {
                monster = value;
                Power = value.Power;
                monsterDescription.Description = monster.Ability.Description;
            }
            
        }
        

        public void UpdateText()
        {
            cardName.text = monster.MonsterName.ToString();
            powerText.text = Power.ToString();
        }

        private void OnDestroy()
        {
            if (monster.Ability.AbilityType == AbilityType.DeathRattle)
            {
                monster.Ability.UseAbility();
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            monsterDescription.gameObject.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            monsterDescription.gameObject.SetActive(false);
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