using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace CCG
{
    internal class Card
    {
        public string name { set; get; }
        public int cost { set; get; }
        public string typeName { set; get; }
        public int strength { set; get; }
        public int will { set; get; }
        public string text { set; get; }
        public List<string> keywords { set; get; }
        public string creatureType { set; get; }
        spellNum spellType { set; get; }
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
                Console.Write(name);
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
            switch (spellType)
            {
                case spellNum.Creature:
                    Console.Write($"Creature: {creatureType}");
                    wrapspace(); wrapspace();
                    Console.Write($"Blood: {cost}");
                    wrapspace(); wrapspace();
                    for(int i = 0; i < text.Length; i++)
                    {
                        if (Console.CursorLeft < farEdge - 2)
                            Console.Write(text[i]);
                        else
                        {
                            wrapspace(); i--;
                        }
                    }
                    middleBar();
                    Console.Write("│   │");
                    for(int i = Console.CursorLeft; i < farEdge - 5; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.WriteLine("│   │");
                    Console.Write($"│ {strength} │ S");
                    for (int i = Console.CursorLeft; i < farEdge - 7; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.WriteLine($"W │ {will} │");
                    Console.Write("│   │");
                    for (int i = Console.CursorLeft; i < farEdge - 5; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.WriteLine("│   │");
                    bottomBar();
                    break;
                case spellNum.Ritual:
                    Console.Write($"Sanity: {cost}");
                    wrapspace(); wrapspace();
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (Console.CursorLeft < farEdge - 2)
                            Console.Write(text[i]);
                        else
                        {
                            wrapspace(); i--;
                        }
                    }
                    for(int i = 0; i < 5; i++)
                    {
                        wrapspace();
                    }
                    backspace();
                    bottomBar();
                    break;
                case spellNum.Incantation:
                    Console.Write($"Sanity: {cost}");
                    wrapspace(); wrapspace();
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (Console.CursorLeft < farEdge - 2)
                            Console.Write(text[i]);
                        else
                        {
                            wrapspace(); i--;
                        }
                    }
                    for (int i = 0; i < 5; i++)
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
