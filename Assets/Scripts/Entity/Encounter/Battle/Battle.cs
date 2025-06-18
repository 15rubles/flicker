using System.Collections.Generic;
using UnityEngine;

namespace Entity.Encounter.Battle
{
    [CreateAssetMenu(fileName = "Battle", menuName = "SOs/Battle", order = 1)]
    public class Battle : ScriptableObject
    {
        [SerializeField] private BattleName battleName;
        [SerializeField] private List<Monster.Monster> monsterSet;
        [SerializeField] private int difficulty;
        [SerializeField] private Reward reward;

        public BattleName BattleName
        {
            get => battleName;
            set => battleName = value;
        }

        public List<Monster.Monster> MonsterSet
        {
            get => monsterSet;
            set => monsterSet = value;
        }

        public int Difficulty
        {
            get => difficulty;
            set => difficulty = value;
        }

        public Reward Reward
        {
            get => reward;
            set => reward = value;
        }
    }
}
