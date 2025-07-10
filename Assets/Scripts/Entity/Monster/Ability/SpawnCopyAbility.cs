using System;
using Controller;
using Object.Monster;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class SpawnCopyAbility : MonsterAbility
    {
        public bool spawnWithTheSamePower = true;
        
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            if (monsterObject.Power <= 0)
                return this;

            Monster newMonsterInstance = UnityEngine.Object.Instantiate(monsterObject.Monster);
            var newMonsterObj = ControllerLocator.GetService<MonsterController>().SpawnMonster(newMonsterInstance).MonsterObj;

            if (spawnWithTheSamePower)
            {
                newMonsterObj.SetPowerWithoutTrigger(monsterObject.Power);
            }
            return this;
        }
    }
}