using Entity;
using Entity.Battle;
using UnityEngine;

namespace Controller
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private int startHandCount = 5;
        [SerializeField] private Deck deck;
        [SerializeField] private CardController cardController;
        [SerializeField] private MonsterController monsterController;
        [SerializeField] private Battle currentBattle;
        
        private void Awake()
        {
            deck.ResetDeck();
            for (int i = 0; i < startHandCount; i++)
            {
                int randomIndex = Random.Range(0, deck.CardsInDeck.Count - 1);
                cardController.SpawnCard(deck.CardsInDeck[randomIndex]);
                deck.CardsInDeck.RemoveAt(randomIndex);
            }

            foreach (var monster in currentBattle.MonsterSet)
            {
                monsterController.SpawnMonster(monster);
            }
        }
    }
}