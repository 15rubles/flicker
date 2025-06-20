using Entity.Monster.Ability;
using Entity.Monster.Attack;
using UnityEngine;

namespace Entity.Monster
{
    [CreateAssetMenu(fileName = "Monster", menuName = "SOs/Monster", order = 1)]
    public class Monster: ScriptableObject
    {
        [SerializeField] private MonsterName monsterName;
        [SerializeField] private int power;
        [SerializeField] [SerializeReference] private MonsterAbility ability;
        [SerializeField] private Sprite visual;
        [SerializeField] [SerializeReference] private MonsterAttack monsterAttack = new DefaultAttack();

        public MonsterName MonsterName
        {
            get => monsterName;
            set => monsterName = value;
        }

        public int Power
        {
            get => power;
            set => power = value;
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

        public Attack.MonsterAttack MonsterAttack
        {
            get => monsterAttack;
            set => monsterAttack = value;
        }
    }
}