using System;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Object.Creature;
using Object.Monster;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;
using DG.Tweening;
using Entity.Encounter.Battle;

namespace Controller
{
    
    public class BattleController : RegisteredMonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private CardSlotController cardSlotController;
        [SerializeField] private MonsterController monsterController;
        [SerializeField] private CreatureController creatureController;
                
        [SerializeField] private TextMeshProUGUI currStepText;
        [SerializeField] private TextMeshProUGUI nextStepText;
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private TextMeshProUGUI nextStepButtonText;
        [SerializeField] private GameObject coverScreen;
        
        [SerializeField] private int startHandCount = 5;
        [SerializeField] private Battle currentBattle;

        [SerializeField] private float animationSpeed = 0.5f;

        [SerializeField] private RunState runState;

        public RunState RunState
        {
            set => runState = value;
        }

        public Deck Deck => runState.Deck;
        
        private bool isBattleWon = false;

        public float AnimationSpeed => animationSpeed;
        
        private CircularNode<TurnStep> currentStep;


        public void StartBattle(Battle newBattle)
        {
            ResetBattleScene();
            currentBattle = newBattle;
            
            StateController.IsMulliganStep = true;
            
            DealHand();
            creatureController.SpawnHealthCard(Deck.HealthCard);

            foreach (var monster in currentBattle.MonsterSet)
            {
                monsterController.SpawnMonster(monster);
            }
        }

        public void ResetBattleScene()
        {
            isBattleWon = false;
            CreateTurnStepsOrder();
            UpdateStepsText();
            Deck.ResetDeck();
            nextStepButtonText.text = "Mulligan";
            turnText.text = "1";
            coverScreen.SetActive(false);
            cardSlotController.Reset();
            monsterController.Reset();
            creatureController.ResetZones();
        }
        

        private void DealHand()
        {
            DealCards(startHandCount);
        }
        
        public void DealCards(int quantity)
        {
            while (quantity > 0)
            {
                quantity--;
                if (Deck.CardsInDeck.Count == 0)
                {
                    Debug.Log("Deck is empty");
                    Deck.ResetDeck();
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
            creatureController.SpawnHealthCard(Deck.HealthCard);
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

            UpdateStepsText();
        }

        private void UpdateStepsText()
        {
            currStepText.text = "current: " + currentStep.Value;
            nextStepText.text = "next: " + currentStep.Next?.Value;
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
                    coverScreen.SetActive(true);
                    nextStepButtonText.text = "Mulligan";
                    break;
                case TurnStep.PlayCards:
                    Mulligan();
                    StateController.IsMulliganStep = false;
                    coverScreen.SetActive(false);
                    nextStepButtonText.text = "Next Step";
                    break;
                case TurnStep.Attack:
                    DiscardHand();
                    await CreaturesAttack();
                    if (isBattleWon)
                    {
                        gameController.StartNewBattle();
                        return;
                    }
                   
                    MonstersAttack();
                    coverScreen.SetActive(true);
                    nextStepButtonText.text = "Next Turn";
                    break;
            }
            currentStep = nextStep;
            UpdateStepsText();
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
            
            foreach (var creature in creatureController.AttackZone.Creatures)
            {
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
        
        private async void MonstersAttack()
        {
            CreatureSlot slot;
            CreatureObj creature;
            
            foreach (var monster in monsterController.MonstersPool)
            {
                slot = creatureController.BlockZone.CreatureSlots.FirstOrDefault();
                
                if (slot == null)
                {
                    //TODO
                    Debug.Log("GAME OVER!!!");
                    return;
                }
                creature = slot.CreatureObj;
                monster.GetComponent<RectTransform>()
                       .DOAnchorPos(UtilitiesFunctions.ConvertAnchoredPosition(creature.GetComponent<RectTransform>(),
                           monster.GetComponent<RectTransform>()), animationSpeed).SetEase(Ease.InOutExpo);
                
                await creature.GetComponent<RectTransform>()
                              .DOAnchorPosY(50, animationSpeed).SetEase(Ease.InOutExpo).AsyncWaitForCompletion();
                
                await monster.Attack(creature);
                
                monster.GetComponent<RectTransform>()
                       .DOAnchorPos(Vector2.zero, animationSpeed).SetEase(Ease.InOutExpo);
            }
        }
    }
}