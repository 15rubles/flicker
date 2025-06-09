using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Battle;
using Object;
using Object.Card;
using Object.Creature;
using Object.Monster;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Random = UnityEngine.Random;

namespace Controller
{
    
    public class BattleController : MonoBehaviour
    {
        [FormerlySerializedAs("cardGroupController")] [SerializeField] private CardSlotController cardSlotController;
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
        
        private void Awake()
        {
            CreateTurnStepsOrder();
            
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
            for (int i = 0; i < startHandCount; i++)
            {
                int randomIndex = Random.Range(0, deck.CardsInDeck.Count - 1);
                cardSlotController.SpawnCard(deck.CardsInDeck[randomIndex]);
                deck.CardsInDeck.RemoveAt(randomIndex);
            }
        }

        private void DiscardHand()
        {
            cardSlotController.CardsInHand = new LinkedList<CardSlot>();
            //TODO add to discard
        }

        private void ResetCreatureZones()
        {
            creatureController.ResetZones(deck.HealthCard);
        }

        private void CreateTurnStepsOrder()
        {
            var firstNode = new CircularNode<TurnStep>(TurnStep.PlayCards);
            var middleNode = new CircularNode<TurnStep>(TurnStep.Attack);
            var lastNode = new CircularNode<TurnStep>(TurnStep.Flick);
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
                case TurnStep.PlayCards:
                    ResetCreatureZones();
                    DealHand();
                    turnText.text = (Int32.Parse(turnText.text) + 1).ToString();
                    coverScreen.SetActive(false);
                    nextStepButtonText.text = "Next Step";
                    break;
                case TurnStep.Attack:
                    DiscardHand();
                    CreaturesAttack();
                    MonstersAttack();
                    coverScreen.SetActive(true);
                    break;
                case TurnStep.Flick:
                    //TODO
                    //Flick();
                    coverScreen.SetActive(true);
                    nextStepButtonText.text = "Next Turn";
                    break;
            }

            currentStep = nextStep;
            UpdateStepsText();
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