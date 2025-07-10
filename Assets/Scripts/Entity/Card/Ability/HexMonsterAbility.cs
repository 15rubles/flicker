using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Entity.Monster.Ability;
using Object.Monster;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class HexMonsterAbility : CardAbility
    {
        public Position hexMonsterPosition = Position.LeftMost;
        public override CardAbility UseAbility(Card card)
        {
            var monsterController = ControllerLocator.GetService<MonsterController>();
            MonsterObject monsterObject = monsterController.MonstersPool.First();
            switch (hexMonsterPosition)
            {
                case Position.LeftMost:
                    monsterObject = monsterController.MonstersPool.First();
                    break;
                case Position.RightMost:
                    monsterObject = monsterController.MonstersPool.Last();
                    break;
            }
            monsterObject.Monster.Abilities = new List<MonsterAbility>();
            return this;
        }


        [Serializable]
        public enum Position
        {
            LeftMost,
            RightMost
        }
    }
}