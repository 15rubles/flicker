using Entity.Card;
using Entity.Monster;
using UnityEngine;
using TMPro;

namespace Object
{
    public class MonsterObject: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI toughness;
        [SerializeField]
        private TextMeshProUGUI power;
        [SerializeField]
        private TextMeshProUGUI cardName;
        
        [SerializeField]
        private Monster monster;

        public Monster Monster
        {
            get => monster;
            set => monster = value;
        }

        void OnEnable()
        {
            cardName.text = monster.MonsterName.ToString();
            toughness.text = monster.Stats.Toughness.ToString();
            power.text = monster.Stats.Power.ToString();
        }
    }
}