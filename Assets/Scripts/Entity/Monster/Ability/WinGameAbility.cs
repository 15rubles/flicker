using System;
using Object.Monster;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class WinGameAbility : MonsterAbility
    {
        [SerializeField] private string winSceneName;
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            SceneManager.LoadScene(winSceneName);
            return this;
        }
    }
}