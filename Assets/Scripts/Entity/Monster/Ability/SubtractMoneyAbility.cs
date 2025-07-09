using System;
using Controller;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class SubtractMoneyAbility : MonsterAbility
    {
        public int subtractMoneyValue = 1;
        public override MonsterAbility UseAbility()
        {
            ControllerLocator.GetService<GameController>().Money -= subtractMoneyValue;
            return this;
        }
    }
}