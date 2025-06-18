using System;
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
    }
}