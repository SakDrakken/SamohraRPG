using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasaT4A_Projekt
{
    public class Characters
    {
        public int id = 0;

        public string name = null;
        public int str = 0;
        public int wis = 0;
        public int agi = 0;

        public int hp = 0;
        public int mana = 0;

        public int clss = 0;

        public static float hpmult = 0.6f;
        public static float mpmult = 0.5f;
        public int atkmod = 0;
        public int bnshp = 0;

        public int upg = 0;

        readonly Random choice = new Random();

        //levelup or some advancement shit
        public void AdvanceCharacter()
        {
            switch (choice.Next(1, 6))
            {
                case 1:
                    VirtualConsole.Draw(name + " has found an upgrade for his weapon. +1 ATK");
                    atkmod++; RefreshStats();
                    break;
                case 2:
                    VirtualConsole.Draw(name + " got stronger. +1 STR");
                    str++; RefreshStats();
                    break;
                case 3:
                    VirtualConsole.Draw(name + " got quicker. +1 AGI");
                    agi++; RefreshStats();
                    break;
                case 4:
                    VirtualConsole.Draw(name + " got smarter. +1 WIS +1 UPG");
                    wis++; upg++; RefreshStats();
                    break;
                case 5:
                    VirtualConsole.Draw(name + " has found a magic item, that increases power! +3 STR");
                    str += 3; RefreshStats();
                    break;
                case 6:
                    VirtualConsole.Draw(name + " has found a tome, that increases all stats! +1 to all");
                    str++; agi++; wis++; atkmod++; bnshp++; RefreshStats();
                    break;
            }

            switch (choice.Next(1, 2))
            {
                case 1:
                    VirtualConsole.Draw("Upgrade point available! UPG +1");
                    upg++;
                    break;
                case 2:
                    break;
            }

            
            if(wis - choice.Next(1, 100) > 0)
            {
                VirtualConsole.Draw("Upgrade point available! UPG +1");
                upg++;
            }

        }

        public Characters(int id, string name, int str, int wis, int agi)
        {
            this.id = id;
            this.name = name;
            this.str = str;
            this.wis = wis;
            this.agi = agi;

            hp = Convert.ToInt32(str * hpmult);
            mana = Convert.ToInt32(wis * mpmult);

            RefreshStats();


        }

        public void RefreshStats()
        {
            hp = Convert.ToInt32(str * hpmult) + bnshp;
            mana = Convert.ToInt32(wis * mpmult);

            if (str > agi && str > wis)
                clss = 0;
            else if (agi > str && agi > wis)
                clss = 1;
            else if (wis > str && wis > agi)
                clss = 2;
            else if (wis == str && str == agi)
                clss = 0;
            else
                clss = 0;
        }

    }
}
