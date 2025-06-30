using System.Collections.Generic;
using UnityEngine;

namespace Entity.Encounter.Battle
{
    [CreateAssetMenu(fileName = "Battle", menuName = "SOs/Battle", order = 1)]
    public class Battle : ScriptableObject
    {
        [SerializeField] private List<Monster.Monster> monsterSet;
        [SerializeField] private int minEncounter;
        [SerializeField] private int maxEncounter;

        public List<Monster.Monster> MonsterSet
        {
            get => monsterSet;
            set => monsterSet = value;
        }

        public int MinEncounter
        {
            get => minEncounter;
            set => minEncounter = value;
        }

        public int MaxEncounter
        {
            get => maxEncounter;
            set => maxEncounter = value;
        }
    }
}
