using System;

namespace Entity.Monster.Attack
{
    [Serializable]
    public abstract class AttackPattern
    {
        public abstract AttackPattern MakeAttack();
    }
}