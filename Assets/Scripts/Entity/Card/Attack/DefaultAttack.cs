using System.Threading.Tasks;
using Entity.Card.Ability;
using Object.Creature;
using Object.Monster;

namespace Entity.Card.Attack
{
    public class DefaultAttack : CardAttack
    {
        //return true if monster died
        public override async Task<bool> Attack(CreatureObj creature, MonsterObject monster)
        {
            if (monster.Power - creature.Power <= 0 || creature.Card.CheckKeyword(KeywordType.Poison))
            {
                await monster.DestroyMonster();
                await Task.Yield();

                return true;
            }

            monster.Power -= creature.Power;
            monster.UpdateText();
            return false;
        }

    }
}