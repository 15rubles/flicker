using System;
using Controller;
using Object.Card;
using UnityEngine;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class DamageToAllMonstersAbility : CardAbility
    {
        [SerializeField] private int damageValue;
        public override CardAbility UseAbility(CardObject cardObj)
        {
            MonsterController monsterController = ControllerLocator.GetService<MonsterController>();
            foreach (var monster in monsterController.MonstersPool)
            {
                monster.Power -= damageValue;
                monster.UpdateText();
                if (monster.Power <= 0)
                {
                    monster.DestroyMonster();
                }
            }
            return this;
        }
    }
}