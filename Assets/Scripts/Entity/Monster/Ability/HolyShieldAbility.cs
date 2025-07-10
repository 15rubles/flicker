using System;
using Object.Monster;
using UnityEngine;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class HolyShieldAbility : MonsterAbility
    {

        [Header("Work best with damageDealt trigger type")]
        public int holyShieldCount = 1;
        
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            if (holyShieldCount > 0)
            {
                monsterObject.Power = monsterObject.PreviousPower;
                holyShieldCount--;
            }
            return this;
        }
    }
}