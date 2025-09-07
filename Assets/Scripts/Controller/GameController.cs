using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Encounter.Battle;
using Entity.Item;
using Object.Item;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Controller
{
    public class GameController : RegisteredMonoBehaviour
    {
        [SerializeField] private BattleController battleController;
        [SerializeField] private ShopController shopController;
        [SerializeField] private GameObject battleCanvas;
        [SerializeField] private GameObject shopCanvas;
        [SerializeField] private GameObject boughtItemsGrid;
        
        [SerializeField] private GameObject boughtItemsPrefab;
        
        [SerializeField] private TextMeshProUGUI encounterText;
        [SerializeField] private int encounterValue = 0;
        [SerializeField] private int shopEncounterTiming = 3;
        [SerializeField] private int shopEncounterCounter = 0;

        private List<Battle> battles = new List<Battle>();
        private List<Battle> battlesToPickFrom = new List<Battle>();

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
                if (value < 0)
                    value = 0;
                
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
            var itemObj = Instantiate(boughtItemsPrefab, boughtItemsGrid.transform).GetComponent<BoughtItemObj>();
            itemObj.SetItem(item);
        }

        public void SellItem(ItemSO item, int sellPrice)
        {
            runState.Items.Remove(item);
            Money += sellPrice;
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
        

        protected override void Awake()
        {
            base.Awake();
            battles = Resources.LoadAll<Battle>("Data/Battles").ToList();
            battlesToPickFrom = battles.ToList();
            runState.Deck.BasicDeck = runState.DeckSo;
            battleController.RunState = runState;
        }

        private void Start()
        {
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
            var list = battlesToPickFrom.Where(battle => battle.MaxEncounter >= encounterValue 
                                               && encounterValue >= battle.MinEncounter).ToList();
            if (list.Count == 0)
            {
                battlesToPickFrom = battles.ToList();
                list = battlesToPickFrom.Where(battle => battle.MaxEncounter >= encounterValue 
                                                         && encounterValue >= battle.MinEncounter).ToList();
            }
            int randomIndex = Random.Range(0, list.Count);
            battleController.StartBattle(list[randomIndex]);
            battlesToPickFrom.RemoveAt(randomIndex);
        }

        private void IncreaseEncounterValue()
        {
            shopEncounterCounter++;
            encounterValue++;
            encounterText.text = encounterValue.ToString();
        }

        public void BeginningOfCombatItemTriggers()
        {
            foreach (var triggerItem in runState.Items
                                                .FindAll(item => item.ability.ItemType == ItemType.TriggerBeginningOfCombat))
            {
                triggerItem.ability.Trigger();
            }
        }
        
        public void BattleWonTriggers()
        {
            foreach (var triggerItem in runState.Items
                                                .FindAll(item => item.ability.ItemType == ItemType.BattleWon))
            {
                triggerItem.ability.Trigger();
            }
        }

        public void CardPlayedTriggers()
        {
            foreach (var triggerItem in runState.Items
                                                .FindAll(item => item.ability.ItemType == ItemType.TriggerCardPlayed))
            {
                triggerItem.ability.Trigger();
            }
        }
    }
}