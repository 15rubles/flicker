using Entity.Card;
using Entity.Monster.Ability;
using UnityEngine;

namespace Entity.Monster
{
    [CreateAssetMenu(fileName = "Monster", menuName = "SOs/Monster", order = 1)]
    public class Monster: ScriptableObject
    {
        [SerializeField] private MonsterName monsterName;
        [SerializeField] private CardStats stats;
        [SerializeField] [SerializeReference] private MonsterAbility ability;
        [SerializeField] private Sprite visual;
        [SerializeField] [SerializeReference] private Attack.AttackPattern attackPattern;

        public MonsterName MonsterName
        {
            get => monsterName;
            set => monsterName = value;
        }

        public CardStats Stats
        {
            get => stats;
            set => stats = value;
        }

        public MonsterAbility Ability
        {
            get => ability;
            set => ability = value;
        }

        public Sprite Visual
        {
            get => visual;
            set => visual = value;
        }

        public Attack.AttackPattern AttackPattern
        {
            get => attackPattern;
            set => attackPattern = value;
        }
    }
}