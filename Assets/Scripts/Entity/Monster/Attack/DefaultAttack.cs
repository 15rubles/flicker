using System;

namespace Entity.Monster.AttackPattern
{
    [Serializable]
    public class DefaultAttack : Attack.AttackPattern
    {
        public override Attack.AttackPattern MakeAttack()
        {
            return this;
        }
    }
}