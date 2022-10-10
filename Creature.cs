using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG
{
    internal class Creature
    {
        public string name { get; }
        public int strength { get; set; }
        public int will { get; set; }
        public string text { get ; set; }
        public string creatureType { get; }
        bool takenDamage { set; get; }

        public Creature(string alias, int str, int tough, string rules, string type)
        {
            name = alias;
            strength = str;
            will = tough;
            text = rules;
            creatureType = type;
            takenDamage = false;
        }
    }
}
