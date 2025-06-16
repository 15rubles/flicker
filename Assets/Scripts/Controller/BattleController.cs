using System;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Entity.Battle;
using Object.Creature;
using Object.Monster;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace Controller
{
    
    public class BattleController : RegisteredMonoBehaviour
    {
        [SerializeField] private CardSlotController cardSlotController;
        [SerializeField] private MonsterController monsterController;
        [SerializeField] private CreatureController creatureController;
                
        [SerializeField] private TextMeshProUGUI currStepText;
        [SerializeField] private TextMeshProUGUI nextStepText;
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private TextMeshProUGUI nextStepButtonText;
        [SerializeField] private GameObject coverScreen;
        
        [SerializeField] private int startHandCount = 5;
        [SerializeField] private Deck deck;
        [SerializeField] private Battle currentBattle;

        [SerializeField] private float animationSpeed = 0.5f;

        public float AnimationSpeed => animationSpeed;
        
        private CircularNode<TurnStep> currentStep;
        
        override protected void Awake()
        {
            base.Awake();
            CreateTurnStepsOrder();
            
            StateController.IsMulliganStep = true;
            
            deck.ResetDeck();
            DealHand();
            creatureController.SpawnHealthCard(deck.HealthCard);

            foreach (var monster in currentBattle.MonsterSet)
            {
                monsterController.SpawnMonster(monster);
            }
        }

        private void DealHand()
        {
            DealCards(startHandCount);
        }
        
        public void DealCards(int quantity)
        {
            if (deck.CardsInDeck.Count < quantity)
            {
                //TODO
                Debug.Log("Deck is empty");
                quantity = deck.CardsInDeck.Count;
            }
            for (int i = 0; i < quantity; i++)
            {
                int randomIndex = Random.Range(0, deck.CardsInDeck.Count - 1);
                cardSlotController.SpawnCard(deck.CardsInDeck[randomIndex]);
                deck.CardsInDeck.RemoveAt(randomIndex);
            }
        }

        private void DiscardHand()
        {
            cardSlotController.DiscardHand();
            //TODO add to discard
        }

        private void ResetCreatureZones()
        {
            creatureController.ResetZones(deck.HealthCard);
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
            int discardedQuantity = cardSlotController.DiscardSelectedForMulligan();
            DealCards(discardedQuantity);
        }
        
        private async Task CreaturesAttack()
        {
            MonsterSlot slot = monsterController.MonsterSlots.FirstOrDefault();
            if (slot == null)
            {
                //TODO
                Debug.Log("WON!!!");
                return;
            }
            if (creatureController.AttackZone.Creatures.Count == 0)
            {
                //TODO
                return;
            }
             
            MonsterObject monster = slot.MonsterObj;
            monster.GetComponent<RectTransform>()
                   .DOAnchorPosY(50, animationSpeed).SetEase(Ease.InOutExpo);
            
            foreach (var creature in creatureController.AttackZone.Creatures)
            {
                await creature.GetComponent<RectTransform>()
                              .DOAnchorPos(
                                  UtilitiesFunctions.ConvertAnchoredPosition(monster.GetComponent<RectTransform>(),
                                                                                    creature.GetComponent<RectTransform>()),
                                  animationSpeed).SetEase(Ease.InOutExpo).AsyncWaitForCompletion();
                if (monster.Power - creature.Power <= 0)
                { 
                    Sequence explosion = DOTween.Sequence();
                    var rect = monster.GetComponent<RectTransform>();
                    
                    explosion.Append(rect.DOScale(1.5f, 0.1f).SetEase(Ease.OutQuad)) // Quick expand
                             .Join(rect.DOShakePosition(0.2f, strength: 20f, vibrato: 10)) // Shake violently
                             .Append(rect.DOScale(0f, 0.15f).SetEase(Ease.InBack));
                    await explosion.AsyncWaitForCompletion();
                    
                    Destroy(slot.gameObject);
                    await Task.Yield();
                    
                    monsterController.MonsterSlots.RemoveAt(0);
                    slot = monsterController.MonsterSlots.FirstOrDefault();
                    if (slot == null)
                    {
                        //TODO
                        Debug.Log("WON!!!");
                        return;
                    }
                    monster = slot.MonsterObj;
                    monster.GetComponent<RectTransform>()
                           .DOAnchorPosY(50, animationSpeed).SetEase(Ease.InOutExpo);
                }
                else
                {
                    monster.Power -= creature.Power;
                    monster.UpdateText();
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
                if (creature.Power - monster.Power <= 0)
                {
                    Sequence explosion = DOTween.Sequence();
                    var rect = creature.GetComponent<RectTransform>();
                    
                    explosion.Append(rect.DOScale(1.5f, 0.1f).SetEase(Ease.OutQuad)) // Quick expand
                             .Join(rect.DOShakePosition(0.2f, strength: 20f, vibrato: 10)) // Shake violently
                             .Append(rect.DOScale(0f, 0.15f).SetEase(Ease.InBack));
                    await explosion.AsyncWaitForCompletion();
                    
                    Destroy(slot.gameObject);
                    await Task.Yield();
                    // creatureController.BlockZone.CreatureSlots.RemoveAt(0);
                }
                else
                {
                    creature.Power -= monster.Power;
                    creature.UpdateText();
                }
                monster.GetComponent<RectTransform>()
                       .DOAnchorPos(Vector2.zero, animationSpeed).SetEase(Ease.InOutExpo);
            }
        }
    }
}