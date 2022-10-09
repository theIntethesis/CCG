using System;
using CCG;


#pragma warning disable CS8321 // Local function is declared but never used
static void main() => new Program();
#pragma warning restore CS8321 // Local function is declared but never used
{ 
    Random rng = new Random();

    void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #region setup
    List<Card> BaseDeck = new List<Card>();
    List<Card> Azathoth = new List<Card>();
    List<Card> Hastor = new List<Card>();
    List<Card> drawPile = BaseDeck;
    foreach(string fName in Directory.GetFiles($@".\BaseDeck"))
    {
        BaseDeck.Add(Card.fromYaml(fName));
        BaseDeck.Add(Card.fromYaml(fName));
    }
    foreach(string fName in Directory.GetFiles($@".\Azathoth"))
    {
        Azathoth.Add(Card.fromYaml(fName));
        Azathoth.Add(Card.fromYaml(fName));
    }
    foreach(string fName in Directory.GetFiles($@".\Hastor"))
    {
        Hastor.Add(Card.fromYaml(fName));
        Hastor.Add(Card.fromYaml(fName));
    }
    Random rand = new Random();
    int coin = rand.Next() % 2;
    switch (coin)
    {
        case 0:
            foreach(Card card in Azathoth)
            {
                drawPile.Add(card);
            }
            break;
        case 1:
            foreach(Card card in Hastor)
            {
                drawPile.Add(card);                
            }
            break;
       default:
            return;
    }
    #endregion setup 
    // drawPile
    List<Card> discardPile = new List<Card>();
    List<Card> hand = new List<Card>();
    Shuffle(drawPile);
    for(int i = 0; i < 5; i++)
    {
        hand.Add(drawPile.ElementAt(0));
        drawPile.RemoveAt(0);
    }
    foreach(Card card in hand)
    {
        card.DrawCard();
        Console.Write("\n\n\n\n\n");
    }
    Thread.Sleep(10000);
    foreach(Card c in drawPile)
    {
        c.DrawCard();
        Thread.Sleep(1000);
        Console.Clear();
    }
}