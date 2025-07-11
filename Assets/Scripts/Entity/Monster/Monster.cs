using System;
using System.Collections.Generic;
using Entity.Monster.Ability;
using Entity.Monster.Attack;
using UnityEngine;

namespace Entity.Monster
{
    [CreateAssetMenu(fileName = "Monster", menuName = "SOs/Monster", order = 1)]
    public class Monster: ScriptableObject
    {
        [SerializeField] private string monsterName;
        [SerializeField] private int power;
        [SerializeField] [SerializeReference] private List<MonsterAbility> abilities = new List<MonsterAbility>();
        [SerializeField] [SerializeReference] private List<MonsterType> monsterTypes = new List<MonsterType>();
        [SerializeField] private Sprite visual;
        [SerializeField] [SerializeReference] private MonsterAttack monsterAttack = new DefaultAttack();

        public List<MonsterType> MonsterTypes
        {
            get => monsterTypes;
            set => monsterTypes = value;
        }

        public string MonsterName
        {
            get => monsterName;
            set => monsterName = value;
        }

        public int Power
        {
            get => power;
            set => power = value;
        }

        public List<MonsterAbility> Abilities
        {
            get => abilities;
            set => abilities = value;
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