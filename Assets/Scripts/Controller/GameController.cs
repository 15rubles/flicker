using System.Collections.Generic;
using Entity.Encounter.Battle;
using UnityEngine;
using Utils;

namespace Controller
{
    public class GameController : RegisteredMonoBehaviour
    {
        [SerializeField] private BattleController battleController;

        [SerializeField] private List<Battle> battles = new List<Battle>();

        [SerializeField] private Battle currentBattle;


        override protected void Awake()
        {
            base.Awake();
            StartNewBattle();
        }

        public void StartNewBattle()
        {
            int randomIndex = Random.Range(0, battles.Count);
            battleController.StartBattle(battles[randomIndex]);
        }
    }
}