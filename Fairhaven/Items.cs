using Fairhaven.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fairhaven
{
    public static class Items
    {
        public static Weapon WeaponFromTemplate(WeaponTemplate template)
        {
            var weapon = new Weapon();
            weapon.Name = template.Name;
            weapon.Rarity = template.Rarity;
            weapon.Level = template.Level;
            weapon.PrimaryModId = template.PrimaryModId;
            weapon.PrimaryMod = template.PrimaryMod;
            weapon.Modifiers = template.Modifiers.ToList();

            return weapon;
        }

        public static Weapon WeaponRandom(Context db, Rarity rarity = Fairhaven.Rarity.Any, int numModifiers = -1)
        {
            var rnd = new Random();
            var qryM = from row in db.Modifiers select row;

            // weapon
            IQueryable<WeaponTemplate> qryW = null;
            if(rarity == Fairhaven.Rarity.Any)
            {
                qryW = from row in db.WeaponTemplates select row;
            }
            else
            {
                qryW = from row in db.WeaponTemplates.Where(w => w.Rarity == rarity) select row;
            }

            int weaponCount = qryW.Count();
            var wt = qryW.OrderBy(w => w.Id).Skip(rnd.Next(weaponCount)).FirstOrDefault();
            var weapon = WeaponFromTemplate(wt);

            // random modifiers
            int mods = 0;
            if(numModifiers == -1 && weapon.Rarity != Rarity.Common && weapon.Rarity != Rarity.Casual)
            {
                numModifiers = rnd.Next(100);
                if(numModifiers < 50) mods = 0;
                else if(numModifiers < 85) mods = 1;
                else if(numModifiers < 95) mods = 2;
                else mods = 3;
            }

            int modifierDbCount = qryM.Count();
            for(int i = 0; i < mods; i++)
            {
                ((List<Modifier>)weapon.Modifiers).Add(qryM.OrderBy(w => w.Id).Skip(rnd.Next(modifierDbCount)).FirstOrDefault());
            }

            return weapon;
        }
    }
}
