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
        [SerializeField] private int minMonstersPowerAfterDamage = 1;
        public override CardAbility UseAbility(Card cardObj)
        {
            MonsterController monsterController = ControllerLocator.GetService<MonsterController>();
            foreach (var monster in monsterController.MonstersPool)
            {
                monster.Power = monster.Power - damageValue > minMonstersPowerAfterDamage ?
                    monster.Power - damageValue : minMonstersPowerAfterDamage;
                
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