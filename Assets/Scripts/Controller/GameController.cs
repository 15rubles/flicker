using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Encounter.Battle;
using Entity.Item;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Utils;

namespace Controller
{
    public class GameController : RegisteredMonoBehaviour
    {
        [SerializeField] private BattleController battleController;
        [SerializeField] private ShopController shopController;
        [SerializeField] private GameObject battleCanvas;
        [SerializeField] private GameObject shopCanvas;
        
        [SerializeField] private TextMeshProUGUI encounterText;
        [SerializeField] private int encounterValue = 0;
        [SerializeField] private int shopEncounterTiming = 3;
        [SerializeField] private int shopEncounterCounter = 0;

        private List<Battle> battles = new List<Battle>();

        [SerializeField] private Battle currentBattle;

        [SerializeField] private RunState runState;

        [Required]
        [SerializeField] private TextMeshProUGUI moneyText;
        [Required]
        [SerializeField] private TextMeshProUGUI hpText;

        
        public int Money
        {
            get => runState.Money;
            set
            {
                runState.Money = value;
                moneyText.text = value.ToString();
            }
        }
        
        public int Hp
        {
            get => runState.Hp;
            set
            {
                runState.Hp = value;
                hpText.text = runState.Hp.ToString();
            }
        }

        public void AddItem(ItemSO item)
        {
            runState.AddItem(item);
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
            battles = Resources.LoadAll<Battle>("Data/Battles").ToList();
            runState.Deck.BasicDeck = runState.DeckSo;
            battleController.RunState = runState;
            StartNewEncounter();
        }

        public void StartNewEncounter()
        {
            IncreaseEncounterValue();
            if (shopEncounterCounter > shopEncounterTiming)
            {
                shopEncounterCounter = 0;
                encounterValue--;
                battleCanvas.SetActive(false);
                shopController.PrepareShop();
                shopCanvas.SetActive(true);
            }
            else
            {
                shopCanvas.SetActive(false);
                battleCanvas.SetActive(true);
                StartNewBattle();
            }
        }

        private void StartNewBattle()
        {
            var list = battles.Where(battle => battle.MaxEncounter >= encounterValue 
                                               && encounterValue >= battle.MinEncounter)
                              .ToList();
            int randomIndex = Random.Range(0, list.Count);
            battleController.StartBattle(list[randomIndex]);
        }

        private void IncreaseEncounterValue()
        {
            shopEncounterCounter++;
            encounterValue++;
            encounterText.text = encounterValue.ToString();
        }
    }
}