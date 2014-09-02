using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fairhaven.Database
{
    public class Modifier
    {
        [Key]
        public Int64 Id { get; set; }
        public Stat Stat { get; set; }
        public DamageType DamageType { get; set; }
        public Int64 ModifierValue { get; set; }
        public Rarity Rarity { get; set; }

        public Int64 Range
        {
            get
            {
                return ModifierValue * 2 / 10;
            }
        }

        public override string ToString()
        {
            return Stat.ToString() + ModifierValue.ToString("+#;-#;+0");
        }
    }

    public class ModifiersHelper
    {
        public static int StatTotal(List<Modifier> mods, Stat stat)
        {
            return (int)mods.Where(m => m.Stat == stat).Sum(m => m.ModifierValue);
        }
    }
}
