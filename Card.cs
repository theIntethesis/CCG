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
        public bool attacked { set; get; }
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
            attacked = false;
        }
        public List<string> DrawCardSide()
        {
            // ┌┐└┘├┤│─
            List<String> returnMe = new List<string>();
            int farEdge = name.Length + 12;
            string frontspace()
            {
                return "│ ";
            }
            string backspace(string append)
            {
                string addMe = "";
                for (int i = append.Length + addMe.Length; i < farEdge-1; i++)
                {
                    addMe += ' ';
                }
                addMe += "│";
                return addMe;
            }
            void header()
            {
                string addMe = "";

                addMe += "┌";
                for (int i = -5; i < name.Length + 5; i++)
                {
                    addMe += "─";
                }
                addMe += "┐";
                returnMe.Add(addMe);

                addMe = "│";
                for (int i = 0; i < 5; i++)
                {
                    addMe += ' ';
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                addMe += name;
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < 5; i++)
                {
                    addMe += ' ';
                }

                addMe += "│";
                returnMe.Add(addMe);

                addMe = "├";
                for (int i = -5; i < name.Length + 5; i++)
                {
                   addMe += "─";
                }
                addMe += "┤";
                returnMe.Add(addMe);
            }
            void bottomBar()
            {
                string addMe = "└";
                for (int i = addMe.Length; i < farEdge - 1; i++)
                {
                    addMe += '─';
                }
                addMe += "┘";
                returnMe.Add(addMe);
            }

            header();
            string temp = text;
            string addMe = frontspace();
            switch (spellType)
            {
                case spellNum.Creature:
                    addMe +=$"Creature: {creatureType}";
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    addMe += $"Blood: {cost}";
                    Console.ForegroundColor = ConsoleColor.White;
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();

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
                        if (addMe.Length + nextWord.Length > farEdge - 2)
                        {
                            addMe += backspace(addMe);
                            returnMe.Add(addMe);

                            addMe = frontspace();
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
                        addMe += $"{nextWord} ";
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
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = "├───┬";
                    for (int i = addMe.Length; i < farEdge - 5; i++)
                    {
                        addMe += '─';
                    }
                    addMe += "┬───┤";
                    returnMe.Add(addMe);

                    addMe = "│   │";
                    for(int i = addMe.Length; i < farEdge - 5; i++)
                    {
                        addMe += ' ';
                    }
                    addMe += "│   │";
                    returnMe.Add(addMe);

                    addMe = "│ ";
                    Console.ForegroundColor = ConsoleColor.Red;
                    addMe += $"{strength}";
                    Console.ForegroundColor = ConsoleColor.White;
                    addMe += " │ S";
                    for (int i = addMe.Length; i < farEdge - 7; i++)
                    {
                        addMe += ' ';
                    }
                    addMe += "W │ ";
                    Console.ForegroundColor = ConsoleColor.Red;
                    addMe += $"{will}";
                    Console.ForegroundColor = ConsoleColor.White;
                    addMe += " │";
                    returnMe.Add(addMe);

                    addMe = "│   │";
                    for (int i = addMe.Length; i < farEdge - 5; i++)
                    {
                        addMe += ' ';
                    }
                    addMe += "│   │";
                    returnMe.Add(addMe);
                    bottomBar();
                    break;
                case spellNum.Ritual:
                case spellNum.Incantation:
                    addMe += $"{spellType}";
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
                    Console.ForegroundColor = ConsoleColor.Green;
                    addMe += $"Sanity: {cost}";
                    Console.ForegroundColor = ConsoleColor.White;
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);

                    addMe = frontspace();
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
                        if (addMe.Length + nextWord.Length > farEdge - 2)
                        {
                            addMe += backspace(addMe);
                            returnMe.Add(addMe);

                            addMe = frontspace();
                        }
                        addMe += $"{nextWord} ";
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
                        addMe += backspace(addMe);
                        returnMe.Add(addMe);

                        addMe = frontspace();
                    }
                    addMe += backspace(addMe);
                    returnMe.Add(addMe);
                    bottomBar();
                    break;
                default:
                    throw new Exception("Error upon Printing: type not recognized");
            }
            for(int i = returnMe.Count; i < 20; i++)
            {
                addMe = "";
                for(int j = 0; j < farEdge; j++)
                {
                    addMe += ' ';
                }
                returnMe.Add(addMe);
            }
            return returnMe;
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
                for (int i = Console.CursorLeft; i < farEdge - 1; i++)
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
                    for (int i = Console.CursorLeft; i < farEdge - 5; i++)
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
