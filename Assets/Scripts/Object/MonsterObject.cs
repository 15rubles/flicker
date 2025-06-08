using Entity.Card;
using Entity.Monster;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace Object
{
    public class MonsterObject: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI powerText;
        [SerializeField]
        private TextMeshProUGUI cardName;
        
        [SerializeField]
        private Monster monster;

        public int Power { get; set; }

        public Monster Monster
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