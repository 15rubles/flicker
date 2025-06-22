using System.Collections.Generic;
using Entity;
using Entity.Encounter.Battle;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Utils;

namespace Controller
{
    public class GameController : RegisteredMonoBehaviour
    {
        [SerializeField] private BattleController battleController;

        [SerializeField] private List<Battle> battles = new List<Battle>();

        [SerializeField] private Battle currentBattle;

        [SerializeField] private RunState runState;

        [Required]
        [SerializeField] private TextMeshProUGUI moneyText;
        
        
        public int Money
        {
            get => runState.Money;
            set
            {
                runState.Money = value;
                moneyText.text = value.ToString();
            }
        }

        public void UpdateMoneyText()
        {
            
            moneyText.text = Money.ToString();
        }

        public void ChangeMoneyBy(int changeAmount)
        {
            Money += changeAmount;
        }

        public bool TryToBuy(int price)
        {
            if (Money >= price)
            {
                Money -= price;
                return true;
            }
            return false;
        }
        

        override protected void Awake()
        {
            base.Awake();
            runState.Deck.BasicDeck = runState.DeckSo;
            battleController.RunState = runState;
        }

        public void StartNewBattle()
        {
            int randomIndex = Random.Range(0, battles.Count);
            battleController.StartBattle(battles[randomIndex]);
        }
    }
}