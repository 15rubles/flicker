using System;
using System.Linq;
using Entity;
using Entity.Battle;
using Object.Creature;
using Object.Monster;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

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
        
        public void NextStep()
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
                    CreaturesAttack();
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
        
        private void CreaturesAttack()
        {
            MonsterSlot slot = monsterController.MonsterSlots.FirstOrDefault();
            if (slot == null)
            {
                //TODO
                Debug.Log("WON!!!");
                return;
            }
            MonsterObject monster = slot.MonsterObj;
            
            
            foreach (var creature in creatureController.AttackZone.Creatures)
            {
                if (monster.Power - creature.Power <= 0)
                { 
                    Destroy(slot.gameObject);
                    monsterController.MonsterSlots.RemoveAt(0);
                    slot = monsterController.MonsterSlots.FirstOrDefault();
                    if (slot == null)
                    {
                        //TODO
                        Debug.Log("WON!!!");
                        return;
                    }
                    monster = slot.MonsterObj;
                }
                else
                {
                    monster.Power -= creature.Power;
                    monster.UpdateText();
                }
            }
        }
        
        private void MonstersAttack()
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
                if (creature.Power - monster.Power <= 0)
                { 
                    Destroy(slot.gameObject);
                    creatureController.BlockZone.CreatureSlots.RemoveAt(0);
                }
                else
                {
                    creature.Power -= monster.Power;
                    creature.UpdateText();
                }
            }
        }
    }
}