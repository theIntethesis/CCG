using System;
using CCG;

internal class Program 
{

	public static List<Card> drawPile = new List<Card>();
	public static List<Card> discardPile = new List<Card>();
	public static List<Card> hand = new List<Card>();
	public static List<Card> yourField = new List<Card>();
	public static List<Card> theirField = new List<Card>();

	public static int yourDevotion = 25;
	public static int theirDevotion = 25;
	public static int yourSanity = 50;
	public static int yourBloodCoffers = 0;
	public static int yourBlood = 0;
	public static int theirSanity = 50;

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
			draw();
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
		tutorial();
	}

	static void draw()
    {
		hand.Add(drawPile.ElementAt(0));
		drawPile.RemoveAt(0);
	}

	static void parse()
	{
		displayHeader();
		string next;
		string input = "";
		while(input == "")
		{
			Console.Write($"\n\n > ");
			input = Console.ReadLine();
		}
		string temp;
		try
		{
			temp = input.Substring(0, input.IndexOf(' ')).ToLower();
		}
		catch (Exception ex)
        {
			temp = input;
		}
		next = input.Remove(0, input.IndexOf(' ') + 1);
		switch (temp)
        {
			case "show":
				switch (next.ToLower())
				{
					case "hand":
						showHand();
						break;
					case "my battlefield":
						showMyBattlefield();
						break;
					case "their battlefield":
						showTheirBattlefield();
						break;
					default:
						break;
				}
				break;
			case "help":
				Console.WriteLine("show hand - Shows cards that you have in hand\n" +
                    "show my battlefield - shows cards on your side of the battlefield\n" +
                    "show their battlefield - shows cards on their side of the battlefield\n" +
                    "play [CARDNAME] - plays a card if you have enough resources\n" +
                    "attack with [CREATURE] - attacks with a creature on the battlefield\n" +
                    "block [OPPONENT'S CREATURE] with [YOUR CREATURE] - explanitory\n" +
                    "clear - clears the screen\n" +
                    "end - ends your turn");
				break;
			case "play":
				foreach (Card c in hand)
                {
					int cardStart = hand.Count();
					if(c.name.ToLower() == next.ToLower())
                    {
                        switch (c.spellType)
                        {
							case Card.spellNum.Creature:
								if(yourBlood >= c.cost)
                                {
									yourField.Add(c);
									hand.Remove(c);
									c.curWill = c.will;
									yourBlood -= c.cost;
									break;
                                }
								break;
							case Card.spellNum.Incantation:
								yourSanity -= c.cost;
								hand.Remove(c);
								break;
							case Card.spellNum.Ritual:
								yourSanity -= c.cost;
								hand.Remove(c);
								break;
							default:
								break;
								
                        }
						Console.WriteLine($"Played {c.name}");
                    }
					if(hand.Count() < cardStart)
						break;
				}
				break;
			case "attack":
				foreach(Card c in yourField)
                {
					int fieldstart = yourField.Count();
					if(next.ToLower().Contains(c.name.ToLower()))
                    {
                        if (!c.attacked)
						{
							c.attacked = true;
							theirDevotion -= c.strength;
							if (c.keywords.Contains("Warping"))
								theirSanity -= c.warpingMod;
							CheckCreatures();
						}
                        else
                        {
							Console.WriteLine("That creature has already attacked this turn!");
                        }
                    }
					if (yourField.Count() < fieldstart)
						break;
                }
				break;
			case "end":
				return;
			case "clear":
				Console.Clear();
				break;
			default:
				Console.WriteLine("Can't find that keyword!");
				break;
		}
		parse();
	}
	
	static void showHand()
	{
		foreach(Card c in hand)
		{
			c.DrawCard();
		}
	}
	static void showMyBattlefield()
	{
		foreach(Card c in yourField)
        {
			c.DrawCard();
		}
	}
	static void showTheirBattlefield()
	{
		foreach (Card c in theirField)
		{
			c.DrawCard();
		}
	}
	static void CheckCreatures()
    {
		List<Card> removeThese = new List<Card>();
		foreach(Card c in yourField)
        {
			if(c.curWill <= 0)
            {
				discardPile.Add(c);
				removeThese.Add(c);
            }
        }
		foreach(Card c in theirField)
        {
			if(c.curWill <= 0)
            {
				removeThese.Add(c);
            }
        }
		foreach(Card c in removeThese)
        {
			yourField.Remove(c);
			theirField.Remove(c);
        }
		if(theirDevotion <= 0)
        {
			youwin();
        }
    }

	static void youwin()
    {
		Console.Clear();

		int width = Console.BufferWidth;
		for (int i = 0; i < (width - 8) / 2; i++)
		{
			Console.Write(' ');
		}
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("You WIN!\n\n");
		Console.ForegroundColor = ConsoleColor.White;

		Console.WriteLine("This ends the 'demo'. Please press 'enter' to close the program :)");
		Console.ReadLine();
		System.Environment.Exit(0);
	}

	static void tutorial()
	{
		int width = Console.BufferWidth;
		for (int i = 0; i < (width - 24) / 2; i++)
		{
			Console.Write(' ');
		}
		Console.WriteLine("Welcome to the Tutorial!\n\n");
	
		Console.WriteLine("This is the command line. Commands like \"show hand\", \"show my battlefield\", and \"help\" allow you to move your point of reference.\n" +
			"Try looking at your hand!");
		string input = "";
		while(input != "show hand")
		{
			Console.Write($"\n\n > ");
			input = Console.ReadLine();
			if(input != "show hand")
				Console.WriteLine("Try looking at your hand!");
        }
		showHand();
		Console.Write("This is your hand, consisting of any number of three types of cards.\n" +
			"Creatures are the frontline of your cult. You play them with ");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("'Blood'");
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(", a resource that refreshes every round.\n" +
			"Rituals and Incantations are played with ");
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		Console.Write("'Sanity'");
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(", a resource that you do not regain.\n" +
            "\n" +
            "You start the game with 5 cards in hand.\n" +
            "Please press enter when you are ready to continue.");
		Console.ReadLine();
		Console.Clear();

		Console.WriteLine("At the beginning of every turn, you gain one additional 'Blood Coffer', to a maximum of 5 Blood Coffers.\n" +
            "These signify the maximum amount of blood that your cult can store.\n" +
            "In addition, you gain 3 blood at the start of your turn. This means that if you have a creature that has a cost of '4 Blood', you can't cast it until you\n" +
            "\tA) Have 4 Blood Coffers and\n" +
            "\tB) End one of your turns with at least one blood.\n" +
            "\n" +
            "Sanity works differently. Sanity is used to cast quick, one-time spells. You start the game with 50 Sanity. If that number reaches 0, you start to lose health equal to the number of turns you've started with 0 Sanity.\n" +
            "For example: If you have 0 Sanity at the start of your turn, and this is the 3rd turn that you've had 0 Sanity, you'll lose 3 Devotion.\n" +
            "\n" +
            "Devotion is the metric by which health is discerned. If you reduce your opponent to 0 Devotion through damage, you win the game." +
            "\n\n" +
            "This demo 'experience' is going to be very one-sided. There is no AI, and no multiplayer option. Sorry.\n" +
            "Pretty much the only thing that works are the creatures in combat.\n" +
            "Please press enter to continue!");
		Console.ReadLine();
		Console.Clear();
		turn();
	}
	static void displayHeader()
    {
		// ┌┐└┘├┤│─
		int width = Console.BufferWidth;
		Console.Write('├');
		for(int i = Console.CursorLeft; i < (width - 22) / 2; i++)
        {
			Console.Write('─');
        }
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		Console.Write("Conflict of the Cosmos");
		Console.ForegroundColor = ConsoleColor.White;
		for (int i = Console.CursorLeft; i < width - 1; i++)
		{
			Console.Write('─');
		}
		Console.WriteLine('┤');
		for (int i = 0; i < (width - 16) / 2; i++)
		{
			Console.Write(' ');
		}
		Console.WriteLine("It is your turn!\n\n");
		Console.Write($" Your Devotion: {yourDevotion}");
		for (int i = 0; i < width - 18 - 19; i++)
		{
			Console.Write(' ');
		}
		Console.WriteLine($"Their Devotion: {theirDevotion} ");
		Console.Write($" Your Sanity: {yourSanity}");
		for (int i = 0; i < width - 16 - 17; i++)
		{
			Console.Write(' ');
		}
		Console.WriteLine($"Their Sanity: {theirSanity} ");
		Console.WriteLine($" Blood Coffers: {yourBloodCoffers}\n" +
			$" Blood: {yourBlood}");
		Console.Write('├'); 
		for (int i = Console.CursorLeft; i < width - 1; i++)
		{
			Console.Write('─');
		}
		Console.WriteLine('┤');
	}
	static void turn()
	{
		foreach(Card c in yourField)
        {
			c.attacked = false;
        }
		draw();
		yourBloodCoffers += 1;
		yourBlood += 3;
		if (yourBlood > yourBloodCoffers)
			yourBlood = yourBloodCoffers;
		parse();
		turn();
	}
}