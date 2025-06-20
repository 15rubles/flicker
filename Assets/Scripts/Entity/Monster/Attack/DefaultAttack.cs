using System;
using System.Threading.Tasks;
using Object.Creature;
using Object.Monster;

namespace Entity.Monster.Attack
{
    [Serializable]
    public class DefaultAttack : MonsterAttack
    {
        //return true if creature died
        public override async Task<bool> Attack(MonsterObject monster, CreatureObj creature)
        {
            if (creature.Power - monster.Power <= 0)
            {
                await creature.DestroyCreature();
                await Task.Yield();
                
                return true;
            }
            creature.Power -= monster.Power;
            creature.UpdateText();
            return false;
        }
    }
}