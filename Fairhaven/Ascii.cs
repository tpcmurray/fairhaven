using Fairhaven.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fairhaven
{
    public class Ctrl
    {
        public static string Bold = "\x02";
        public static string Color = "\x03";
        public static string Underline = "\x15";
        public static string Italic = "\x09";
        public static string StrikeThrough = "\x13";
        public static string White = "0";
        public static string Black = "1";
        public static string Blue = "2";
        public static string Green = "3";
        public static string LightRed = "4";
        public static string Brown = "5";
        public static string Purple = "6";
        public static string Orange = "7";
        public static string Yellow = "8";
        public static string LightGreen = "9";
        public static string Cyan = "10";
        public static string LightCyan = "11";
        public static string LightBlue = "12";
        public static string Pink = "13";
        public static string Grey = "14";
        public static string LightGrey = "15";
    }

    public class AString
    {
        private string _asciStr;
        private string _ctrlStr;

        public AString(string str)
        {
            _asciStr = str;
            _ctrlStr = str;
        }

        public AString Color(string foreground, string background = "1")
        {
            _ctrlStr = String.Format("{0}{1},{2}{3}{0}", Ctrl.Color, foreground, background, _asciStr);
            return this;
        }

        public Int32 Length()
        {
            return _asciStr.Length;
        }

        public override string ToString()
        {
            return _ctrlStr;
        }
    }

    public static class Ascii
    {
        public static string Weapon(Weapon w)
        {

            // ╒ Hammer ══════════════ lv30 ╕
            // │  Dmg 10±2        Legendary │
            // └────────────────────────────┘

            // ╒ Hammer ══════════════ lv30 ╕
            // │  Dmg 10±2        Legendary │
            // ├ Enchants ──────────────────┤
            // │  Str +7                    │
            // └────────────────────────────┘

            var topl = new AString("╒ ").Color(Ctrl.LightGrey);
            var topr = "╕";
            var bar = new AString("│").Color(Ctrl.LightGrey);
            var ench = Color("├", Ctrl.LightGrey) + 
                      Color(" Enchants ", Ctrl.LightGreen) +
                      Color("──────────────────┤", Ctrl.LightGrey);
            var end = Color("└────────────────────────────┘", Ctrl.LightGrey);

            StringBuilder sb = new StringBuilder();

            // title and level
            // ╒ Hammer ══════════════ lv30 ╕
            var name = new AString(w.Name).Color(ColorFromRarity(w.Rarity));
            var lvl = " lv" + w.Level.ToString() + " ";
            string titleLine = " " + new String('═', 26 - w.Name.Length - lvl.Length) + lvl + topr;
            string title = topl + name.ToString() + Color(titleLine, Ctrl.LightGrey);
            sb.AppendLine(title);

            // rarity and dmg
            // │  Dmg 10±2        Legendary │
            var rarity = new AString(w.Rarity.ToString() + " ").Color(ColorFromRarity(w.Rarity));
            var sdmg = "  Dmg " + w.PrimaryMod.ModifierValue.ToString();
            if(w.PrimaryMod.Range > 0)
            {
                sdmg += "±" + w.PrimaryMod.Range.ToString();
            }
            var dmg = new AString(sdmg).Color(Ctrl.White);
            var rardmgLine = " ".Repeat(28 - rarity.Length() - dmg.Length());
            var rardmg = bar + dmg.ToString() + Color(rardmgLine, Ctrl.White) + rarity.ToString() + bar;
            sb.AppendLine(rardmg);
            
            // enchants
            if(w.Modifiers.Count > 0)
            {
                // ├ Enchants ──────────────────┤
                // │  Str +7                    │
                sb.AppendLine(ench);
                
                string modline = "  ";
                foreach(var m in w.Modifiers)
                    modline += m.ToString() + "  ";

                modline += " ".Repeat(28 - modline.Length);
                sb.AppendLine(bar + Color(modline, Ctrl.LightGreen) + bar);
            }

            // closing line
            // └────────────────────────────┘
            sb.AppendLine(end);

            return sb.ToString();
        }

        public static string Bold(string str)
        {
            return Ctrl.Bold + str + Ctrl.Bold;
        }

        public static string Color(string str, string foreground, string background = "1")
        {
            return String.Format("{0}{1},{2}{3}{0}", Ctrl.Color, foreground, background, str);
        }

        public static string ColorFromRarity(Rarity rarity)
        {
            switch(rarity)
            {
                case Rarity.Casual: return Ctrl.Grey;
                case Rarity.Common: return Ctrl.White;
                case Rarity.Uncommon: return Ctrl.LightGreen;
                case Rarity.Rare: return Ctrl.LightCyan;
                case Rarity.Epic: return Ctrl.Pink;
                case Rarity.Legendary: return Ctrl.Orange;
                case Rarity.Artifact: return Ctrl.Yellow;
            }

            return Ctrl.White;
        }

    }
}
