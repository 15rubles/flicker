using Controller;
using Object.Deck;
using Utils;

namespace Object.Shop
{
    public class CardInShopObj : CardVisualObj
    {
        public void BuyCard()
        {
            var deck = ControllerLocator.GetService<BattleController>().Deck;
            deck.AllCards.Add(Card);
            //TODO add price and price check
            Destroy(gameObject);
        }
    }
}