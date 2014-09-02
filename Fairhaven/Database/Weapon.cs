using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Fairhaven.Database
{
    public class Weapon
    {
        public Weapon()
        {
            Modifiers = new List<Modifier>();
        }

        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Rarity Rarity { get; set; }
        public Int64 Level { get; set; }

        [ForeignKey("PrimaryModId")]
        public Modifier PrimaryMod { get; set; }
        public Int64 PrimaryModId { get; set; }

        public virtual IList<Modifier> Modifiers { get; set; }

        public override string ToString()
        {
            return Ascii.Weapon(this);
        }
    }
}
