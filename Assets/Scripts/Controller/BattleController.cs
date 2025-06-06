using System;
using System.Collections.Generic;
using Entity;
using Entity.Battle;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Controller
{
    
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private CardGroupController cardGroupController;
        [SerializeField] private MonsterController monsterController;
                
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
            for (int i = 0; i < startHandCount; i++)
            {
                int randomIndex = Random.Range(0, deck.CardsInDeck.Count - 1);
                cardGroupController.SpawnCard(deck.CardsInDeck[randomIndex]);
                deck.CardsInDeck.RemoveAt(randomIndex);
            }
        
            foreach (var monster in currentBattle.MonsterSet)
            {
                monsterController.SpawnMonster(monster);
            }
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
                    turnText.text = (Int32.Parse(turnText.text) + 1).ToString();
                    coverScreen.SetActive(false);
                    nextStepButtonText.text = "Next Step";
                    break;
                case TurnStep.Attack:
                    //TODO
                   // CreaturesAttack();
                   // MonstersAttack();
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
    }
}