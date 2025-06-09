using TMPro;
using UnityEngine;

namespace Object.Monster
{
    public class MonsterObject: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI powerText;
        [SerializeField]
        private TextMeshProUGUI cardName;

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
            }
            
        }
        

        public void UpdateText()
        {
            cardName.text = monster.MonsterName.ToString();
            powerText.text = Power.ToString();
        }
    }
}