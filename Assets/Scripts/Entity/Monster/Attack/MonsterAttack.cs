using System;
using System.Threading.Tasks;
using Object.Creature;
using Object.Monster;

namespace Entity.Monster.Attack
{
    [Serializable]
    public abstract class MonsterAttack
    {
        public abstract Task<bool> Attack(MonsterObject monster, CreatureObj creature);
    }
}