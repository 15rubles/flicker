using System;
using System.Threading.Tasks;
using Object.Creature;
using Object.Monster;

namespace Entity.Card.Attack
{
    [Serializable]
    public abstract class CardAttack
    {
        public abstract Task<bool> Attack(CreatureObj creature, MonsterObject monster);

    }
}