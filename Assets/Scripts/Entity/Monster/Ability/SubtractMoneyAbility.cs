using System;
using Controller;
using Object.Monster;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class SubtractMoneyAbility : MonsterAbility
    {
        public int subtractMoneyValue = 1;
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            ControllerLocator.GetService<GameController>().Money -= subtractMoneyValue;
            return this;
        }
    }
}