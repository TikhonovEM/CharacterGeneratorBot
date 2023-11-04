using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.MorkBorg
{
    public class Character
    {
        public string? Name { get; set; }

        public int? Health { get; set; }

        public string[]? Description { get; set; }

        public Skills? Skills { get; set; }

        public string? WeaponOne { get; set; }
        public string? WeaponTwo { get; set; }
        public string? Armor { get; set; }

        public string[]? Items { get; set; }
    }

    public class Skills
    {
        public int? Skill { get; set; }
        public int? Control { get; set; }
        public int? Strength { get; set; }
        public int? Durability { get; set; }
    }
}
