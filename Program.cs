using System;
using CCG;

internal class Program 
{

	public static List<Card> drawPile = new List<Card>();
	public static List<Card> discardPile = new List<Card>();
	public static List<Card> hand = new List<Card>();
	static void Main()
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
		foreach(Card c in BaseDeck)
		{
			drawPile.Add(c);
		}
	#endregion setup 

	Shuffle(drawPile);
	for(int i = 0; i < 5; i++)
	{
		hand.Add(drawPile.ElementAt(0));
		drawPile.RemoveAt(0);
	}
	int width = Console.BufferWidth;
	for (int i = 0; i < (width - 24) / 2; i++)
	{
		Console.Write(' ');
	}
	Console.WriteLine("Welcome to the Tutorial!\n\n");

	Console.Write("This game has a WIP name of ");
	Console.ForegroundColor = ConsoleColor.DarkGreen;
	Console.Write("\"Clash of the Cosmos\"");
	Console.ForegroundColor = ConsoleColor.White;
	Console.WriteLine(". It is a card game based loosely off of H.P. Lovecraft's Cthulhu Mythos.");
	Console.WriteLine("Please press 'Enter' to continue.");
	Console.ReadLine();
	Console.Clear();
	cmd();
}
	static void parse(string input)
	{
		if(input.Substring(0, input.IndexOf(' ')).ToLower() == "show")
		{
			string next = input.Remove(0, input.IndexOf(' ')+1);
				Console.WriteLine(next);
			switch (next.ToLower())
			{
				case "hand":
					showHand();
					break;
				case "battlefield":
					showBattlefield();
					break;
				default:
					return;
			}
		}
	}
	
	static void showHand()
	{
		foreach(Card c in hand)
		{
				c.DrawCard();
		}
	}
	
	static void showBattlefield()
	{
	
	}
	
	static void cmd()
	{
		int width = Console.BufferWidth;
		for (int i = 0; i < (width - 24) / 2; i++)
		{
			Console.Write(' ');
		}
		Console.WriteLine("Welcome to the Tutorial!\n\n");
	
		Console.WriteLine("This is the command line. Commands like \"show hand\" or \"show battlefield\" allow you to move your point of reference.\n" +
			"Try looking at your hand!");
		string input = "";
		while (input.ToLower() != "show hand") {
			Console.Write($"\n\n > ");
			input = Console.ReadLine();
			if (input.ToLower() != "show hand") 
				Console.WriteLine("Try looking at your hand.");
		}   
		parse(input);
	}
}