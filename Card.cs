using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace CCG
{
    #pragma warning disable CS0168 // Variable is declared but never used
    internal class Card
    {
        public string name { set; get; }
        public int cost { set; get; }
        public string typeName { set; get; }
        public int strength { set; get; }
        public int will { set; get; }
        public int curWill { set; get; }
        public bool takenDamage { set; get; }
        public string text { set; get; }
        public int warpingMod { set; get; }
        public List<string> keywords { set; get; }
        public string creatureType { set; get; }
        public spellNum spellType { set; get; }
        public enum spellNum { Creature = 1, Ritual = 2, Incantation = 3};
        public enum keyword { massive = 1, warping = 2, manifest = 3, dying_breath = 4, initiation = 5, burn = 6 };
        public Card() 
        {
            keywords = new List<string>();
            strength = 0;
            will = 0;
            cost = 0;
            text = "";
            name = "";
            spellType = 0;
            typeName = "";
            creatureType = "";
            curWill = 0;
            takenDamage = false;
        }
        public void DrawCard()
        {
            // ┌┐└┘├┤│─
            int spacing = 1;
            int farEdge = name.Length + 12;
            void frontspace()
            {
                Console.Write($"│");
                for (int i = 0; i < spacing; i++)
                {
                    Console.Write(' ');
                }
            }
            void backspace()
            {
                for (int i = Console.CursorLeft; i < farEdge-1; i++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine($"│");
            }
            void wrapspace()
            {
                backspace(); frontspace();
            }
            void header()
            {
                Console.Write($"┌");
                for (int i = -5; i < name.Length + 5; i++)
                {
                    Console.Write("─");
                }
                Console.WriteLine("┐");
                Console.Write($"│");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(' ');
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(name);
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(' ');
                }

                Console.Write($"│\n" +
                    $"├");
                for (int i = -5; i < name.Length + 5; i++)
                {
                    Console.Write("─");
                }
                Console.WriteLine("┤");
            }
            void middleBar()
            {
                backspace();
                Console.Write($"├");
                for (int i = Console.CursorLeft; i < farEdge - 1; i++)
                {
                    Console.Write('─');
                }
                Console.WriteLine("┤");
            }
            void bottomBar()
            {
                Console.Write($"└");
                for (int i = Console.CursorLeft; i < farEdge - 1; i++)
                {
                    Console.Write('─');
                }
                Console.WriteLine("┘");
            }

            header();
            frontspace();
            string temp = text;
            switch (spellType)
            {
                case spellNum.Creature:
                    Console.Write($"Creature: {creatureType}");
                    wrapspace(); wrapspace();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"Blood: {cost}");
                    Console.ForegroundColor = ConsoleColor.White;
                    wrapspace(); wrapspace();
                    while (temp.Length > 0)
                    {
                        string nextWord = "";
                        try
                        {
                            nextWord = temp.Substring(0, temp.IndexOf(' '));
                        }
                        catch (Exception ex)
                        {
                            nextWord = temp;
                        }
                        if (Console.CursorLeft + nextWord.Length > farEdge - 2)
                        {
                            wrapspace();
                        }
                        //massive = 1, warping = 2, manifest = 3, dying_breath = 4, initiation = 5
                        if (nextWord.Contains("Massive"))
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else if (nextWord.Contains("Warping"))
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (nextWord.Contains("Dying") || nextWord.Contains("Breath"))
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else if (nextWord.Contains("Initiation"))
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        else Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{nextWord} ");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (temp.Length > 0)
                        {
                            try
                            {
                                temp = temp.Remove(0, nextWord.Length + 1);
                            }
                            catch (Exception ext)
                            {
                                temp = "";
                            }
                        }
                    }
                    middleBar();
                    Console.Write("│   │");
                    for(int i = Console.CursorLeft; i < farEdge - 5; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.WriteLine("│   │");
                    Console.Write($"│ ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(strength);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" │ S");
                    for (int i = Console.CursorLeft; i < farEdge - 7; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write($"W │ ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(will);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" │");
                    Console.Write("│   │");
                    for (int i = Console.CursorLeft; i < farEdge - 5; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.WriteLine("│   │");
                    bottomBar();
                    break;
                case spellNum.Ritual:
                    Console.Write($"Ritual");
                    wrapspace(); wrapspace();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Sanity: {cost}");
                    Console.ForegroundColor = ConsoleColor.White;
                    wrapspace(); wrapspace();
                    while(temp.Length > 0)
                    {
                        string nextWord = "";
                        try
                        {
                            nextWord = temp.Substring(0, temp.IndexOf(' '));
                        }
                        catch (Exception ex)
                        {
                            nextWord = temp;
                        }
                        if (Console.CursorLeft + nextWord.Length > farEdge - 2)
                        {
                            wrapspace();
                        }
                        Console.Write($"{nextWord} ");
                        if (temp.Length > 0)
                        {
                            try
                            {
                                temp = temp.Remove(0, nextWord.Length + 1);
                            }
                            catch (Exception ext)
                            {
                                temp = "";
                            }
                        }
                    }
                    for(int i = 0; i < 2; i++)
                    {
                        wrapspace();
                    }
                    backspace();
                    bottomBar();
                    break;
                case spellNum.Incantation:
                    Console.Write($"Incantation");
                    wrapspace(); wrapspace();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Sanity: {cost}");
                    Console.ForegroundColor = ConsoleColor.White;
                    wrapspace(); wrapspace();
                    while (temp.Length > 0)
                    {
                        string nextWord = "";
                        try
                        {
                            nextWord = temp.Substring(0, temp.IndexOf(' '));
                        }
                        catch (Exception ex)
                        {
                            nextWord = temp;
                        }
                        if (Console.CursorLeft + nextWord.Length > farEdge - 2)
                        {
                            wrapspace();
                        }
                        Console.Write($"{nextWord} ");
                        if (temp.Length > 0)
                        {
                            try
                            {
                                temp = temp.Remove(0, nextWord.Length + 1);
                            }
                            catch (Exception ext)
                            {
                                temp = "";
                            }
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        wrapspace();
                    }
                    backspace();
                    bottomBar();
                    break;
                default:
                    throw new Exception("Error upon Printing: type not recognized");
            }
        }
        public void printCard()
        {
            switch (spellType)
            {
                case spellNum.Creature:
                    Console.Write(
                        $"Name:\t{name}\n" +
                        $"Type:\tCreature - {creatureType}\n" +
                        $"S/W:\t{strength}/{will}\n" +
                        $"Blood:\t{cost}\n" +
                        $"Text:\t{text}\n" +
                        $"Keywords:\t"
                    );
                    
                    if (keywords != null)
                    {
                        foreach (string key in keywords)
                        {
                            Console.Write($"{key}, ");
                        }
                    }
                    else
                    {
                        Console.Write("None");
                    }
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine(
                        $"Name:\t{name}\n" +
                        $"Type:\t{spellType}\n" +
                        $"Sanity:\t{cost}\n" +
                        $"Text:\t{text}\n"
                    );
                    break;
            }
            
        }
        public static Card fromYaml(string fName)
        {
            Card returned;
            using (FileStream fin = File.OpenRead(fName))
            {
                TextReader reader = new StreamReader(fin);

                var deserializer = new Deserializer();
                returned = deserializer.Deserialize<Card>(reader);
            }
            switch (returned.typeName)
            {
                case "Ritual":
                    returned.spellType = spellNum.Ritual;
                    break;
                case "Incantation":
                    returned.spellType = spellNum.Incantation;
                    break;
                case "Creature":
                    returned.spellType = spellNum.Creature;
                    break;
                default: 
                    throw new ArgumentException();
            }
            return returned;
        }
    }
}
