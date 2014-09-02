using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fairhaven
{
    public enum Stat
    {
        Dmg,
        Atk,
        Str,
        Def,
        Mag,
        Luk,
    }

    public enum DamageType
    {
        Physical,
        Fire,
        Ice,
        Power,
    }

    public enum Rarity
    {
        Casual,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact,
        Any,
    }
}
