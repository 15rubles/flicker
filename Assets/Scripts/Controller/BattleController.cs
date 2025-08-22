using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Object.Monster;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;
using DG.Tweening;
using Entity.Card;
using Entity.Encounter.Battle;
using JetBrains.Annotations;
using Object.Creature;

namespace Controller
{
    
    public class BattleController : RegisteredMonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private CardSlotController cardSlotController;
        [SerializeField] private MonsterController monsterController;
        [SerializeField] private CreatureController creatureController;

        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private TextMeshProUGUI nextStepButtonText;
        [SerializeField] private GameObject coverScreen;
        
        [SerializeField] private int startHandCount = 3;
        [SerializeField] private Battle currentBattle;

        [SerializeField] private float animationSpeed = 0.5f;

        [SerializeField] private RunState runState;

        [SerializeField] private int shieldValue;
        [SerializeField] private TextMeshProUGUI shieldText;
        [SerializeField] private RectTransform shiedZone;
        [SerializeField] private RectTransform monsterZoneRect;

        [SerializeField] private int rewardMoneyTurnOne = 5;
        [SerializeField] private int rewardMoneyTurnTwo = 4;
        [SerializeField] private int rewardMoneyTurnThreePlus = 3;

        [SerializeField] private List<Card> extraCardsAtTheStartOfTheRound = new List<Card>();

        public TextMeshProUGUI TurnText
        {
            get => turnText;
            set => turnText = value;
        }
        
        [CanBeNull]
        public CreatureObj CurrentAttackingCreatureObj
        {
            get;
            set;
        }

        public List<Card> ExtraCardsAtTheStartOfTheRound
        {
            get => extraCardsAtTheStartOfTheRound;
            set => extraCardsAtTheStartOfTheRound = value;
        }

        public RectTransform ShiedZone => shiedZone;

        public int StartHandCount
        {
            get => startHandCount;
            set => startHandCount = value;
        }

        public RunState RunState
        {
            set => runState = value;
        }

        public int MaxMulliganCount => runState.MaxMulliganCount;

        public Deck Deck => runState.Deck;
        
        private bool isBattleWon = false;

        public float AnimationSpeed => animationSpeed;
        
        private CircularNode<TurnStep> currentStep;

        [CanBeNull]
        public CreatureObj NextAttackingCreature
        {
            get;
            set;
        }

        public void StartBattle(Battle newBattle)
        {
            ResetBattleScene();
            currentBattle = newBattle;
            
            StateController.IsMulliganStep = true;
            
            DealHand();
            ResetShieldValue();

            foreach (var monster in currentBattle.MonsterSet)
            {
                monsterController.SpawnMonster(monster);
            }
        }

        private void ResetShieldValue()
        {
            shieldValue = Deck.ShieldValue;
            shieldText.text = shieldValue.ToString();
        }
        
        public void UpdateShieldValue(int increaseAmount)
        {
            shieldValue += increaseAmount;
            shieldText.text = shieldValue.ToString();
        }

        public void ResetBattleScene()
        {
            isBattleWon = false;
            extraCardsAtTheStartOfTheRound = new List<Card>();
            CreateTurnStepsOrder();
            Deck.ResetDeck();
            nextStepButtonText.text = "Mulligan";
            turnText.text = "1";
            coverScreen.SetActive(false);
            cardSlotController.Reset();
            monsterController.Reset();
            creatureController.ResetZones();
        }


        private void ResetDeck()
        {
            
        }
        
        private void DealHand()
        {
            DealCards(startHandCount);
            
            foreach (var extraCard in extraCardsAtTheStartOfTheRound)
            {
                cardSlotController.SpawnCard(extraCard);
            }
            extraCardsAtTheStartOfTheRound = new List<Card>();
        }
        
        public void DealCards(int quantity)
        {
            while (quantity > 0)
            {
                quantity--;
                if (Deck.CardsInDeck.Count == 0)
                {
                    Debug.Log("Deck is empty");
                    if (Deck.CardsInDiscard.Count == 0)
                    {
                        Debug.Log("Discard is empty");
                        return;
                    }
                    Deck.ShuffleDiscardToDeck();
                }
                int randomIndex = Random.Range(0, Deck.CardsInDeck.Count - 1);
                cardSlotController.SpawnCard(Deck.CardsInDeck[randomIndex]);
                Deck.CardsInDeck.RemoveAt(randomIndex);
            }
        }

        private void DiscardHand()
        {
            cardSlotController.DiscardHand(Deck);
        }

        private void ResetCreatureZones()
        {
            creatureController.ResetZones();
            ResetShieldValue();
        }

        private void CreateTurnStepsOrder()
        {
            var firstNode = new CircularNode<TurnStep>(TurnStep.Mulligan);
            var middleNode = new CircularNode<TurnStep>(TurnStep.PlayCards);
            var lastNode = new CircularNode<TurnStep>(TurnStep.Attack);
            firstNode.Next = middleNode;
            middleNode.Next = lastNode;
            lastNode.Next = firstNode;
            currentStep = firstNode;
        }
        
        public async void NextStep()
        {
            var nextStep = currentStep.Next;
            
            switch (nextStep?.Value)
            {
                case TurnStep.Mulligan:
                    StateController.IsMulliganStep = true;
                    ResetCreatureZones();
                    turnText.text = (Int32.Parse(turnText.text) + 1).ToString();
                    DealHand();
                    nextStepButtonText.text = "Mulligan";
                    break;
                case TurnStep.PlayCards:
                    Mulligan();
                    StateController.IsMulliganStep = false;
                    coverScreen.SetActive(false);
                    nextStepButtonText.text = "Next Step";
                    break;
                case TurnStep.Attack:
                    coverScreen.SetActive(true);
                    DiscardHand();
                    
                    gameController.BeginningOfCombatItemTriggers();
                    monsterController.BeginningOfCombatTrigger();
                    
                    await CreaturesAttack();
                    if (isBattleWon)
                    {
                        gameController.BattleWonTriggers();
                        ReceiveReward();
                        gameController.StartNewEncounter();
                        return;
                    }
                   
                    bool didNotLose = await MonstersAttack();
                    if (didNotLose)
                    {
                        coverScreen.SetActive(true);
                        nextStepButtonText.text = "Next Turn";
                    }
                    
                    monsterController.EndOfCombatTrigger();
                    break;
            }
            currentStep = nextStep;
        }

        private void ReceiveReward()
        {
            switch (Int32.Parse(turnText.text))
            {
                case 1:
                    gameController.Money += rewardMoneyTurnOne;
                    gameController. Hp++;
                    break;
                case 2:
                    gameController.Money += rewardMoneyTurnTwo;
                    break;
                default:
                    gameController.Money += rewardMoneyTurnThreePlus;
                    break;
            }
            
            //TODO
        }
        private void UpdateRewardText()
        {
            switch (Int32.Parse(turnText.text))
            {
                case 1:
                    gameController.Money += rewardMoneyTurnOne;
                    gameController. Hp++;
                    break;
                case 2:
                    gameController.Money += rewardMoneyTurnTwo;
                    break;
                default:
                    gameController.Money += rewardMoneyTurnThreePlus;
                    break;
            }
            
            //TODO
        }

        private void Mulligan()
        {
            int discardedQuantity = cardSlotController.DiscardSelectedForMulligan(Deck);
            DealCards(discardedQuantity);
        }
        
        private async Task CreaturesAttack()
        {
            MonsterSlot slot = monsterController.MonsterSlots.FirstOrDefault();
            if (slot == null)
            {
                //TODO
                Debug.Log("WON!!!");
                isBattleWon = true;
                return;
            }
            if (creatureController.AttackZone.Creatures.Count == 0)
            {
                //TODO
                return;
            }
             
            MonsterObject monster = slot.MonsterObj;
            monster.Rect
                   .DOAnchorPosY(50, animationSpeed).SetEase(Ease.InOutExpo);

            for (int index = 0; index < creatureController.AttackZone.Creatures.Count; index++)
            {
                NextAttackingCreature = index + 1 < creatureController.AttackZone.Creatures.Count 
                                                    ? creatureController.AttackZone.Creatures[index + 1] : null;
                CreatureObj creature = creatureController.AttackZone.Creatures[index];
                await creature.GetComponent<RectTransform>()
                              .DOAnchorPos(
                                  UtilitiesFunctions.ConvertAnchoredPosition(monster.GetComponent<RectTransform>(),
                                      creature.GetComponent<RectTransform>()),
                                  animationSpeed).SetEase(Ease.InOutExpo).AsyncWaitForCompletion();

                bool isMonsterDied = await creature.Attack(monster);

                slot = monsterController.MonsterSlots.FirstOrDefault();
                if (slot == null)
                {
                    //TODO
                    Debug.Log("WON!!!");
                    isBattleWon = true;
                    return;
                }
                monster = slot.MonsterObj;
                if (isMonsterDied)
                {
                    monster.Rect
                           .DOAnchorPosY(50, animationSpeed).SetEase(Ease.InOutExpo);
                }

                creature.GetComponent<RectTransform>()
                        .DOAnchorPos(Vector2.zero, animationSpeed).SetEase(Ease.InOutExpo);
            }
        }
        
        private async Task<bool> MonstersAttack()
        {
            int originalIndex = monsterZoneRect.GetSiblingIndex();
            monsterZoneRect.SetAsLastSibling();
            
            foreach (var monster in monsterController.MonstersPool)
            {
                
                
                await monster.GetComponent<RectTransform>()
                       .DOAnchorPos(UtilitiesFunctions.ConvertAnchoredPosition(shieldText.GetComponent<RectTransform>(),
                           monster.GetComponent<RectTransform>()), animationSpeed).SetEase(Ease.InOutExpo).AsyncWaitForCompletion();
                
                UpdateShieldValue(-monster.Power);

                await monster.GetComponent<RectTransform>()
                            .DOAnchorPos(Vector2.zero, animationSpeed).SetEase(Ease.InOutExpo).AsyncWaitForCompletion();
                
                if (shieldValue <= 0)
                {
                    monsterZoneRect.SetSiblingIndex(originalIndex);

                    gameController.Hp--;

                    if (gameController.Hp <= 0)
                    {
                        //TODO
                        Debug.Log("GAME OVER!!!");
                    }
                    else
                    {
                        StartBattle(currentBattle);
                    }
                    
                    return false;
                }
                
                
            }
            monsterZoneRect.SetSiblingIndex(originalIndex);
            return true;
        }
    }
}