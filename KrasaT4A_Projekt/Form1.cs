using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrasaT4A_Projekt
{
    public partial class Form1 : Form
    {
        public static List<Characters> Character = new List<Characters>();
        public static int difficulty = 60; // default
        static int i = 1;

        public static bool GameOverFlag = true;

        public static string GetStringClss(int clss)
        {
            if (clss == 1)
                return "Rogue";
            if (clss == 2)
                return "Wizard";
            else
                return "Warrior";
        }

        static void RefreshDB()
        {
            Character = new List<Characters>();
            InitDB();
            ProcNPCs(difficulty);
        }

        static int InitDB()
        {
            Character.Add(new Characters(0, "Player", 4, 3, 3));
            Character.Add(new Characters(1, "Rat", 2, 1, 1));
            Character.Add(new Characters(2, "Bear", 4, 3, 1));
            Character.Add(new Characters(3, "Bandit", 1, 1, 5));
            Character.Add(new Characters(4, "Bandit Healer", 1, 4, 1));
            Character.Add(new Characters(5, "Bandit Boss", 10, 10, 10));
            return 0;
        }


        static void GameOver()
        {
            GameOverFlag = true;
            VirtualConsole.Clear();
            VirtualConsole.Draw("\r\n\r\n\r\n             YOU DIED\r\n\r\n\r\nRestart the game!");
            VirtualConsole.Draw("\r\nFinal stats:\r\n" + Character[0].name + " | " +
                           Character[0].str + " STR | " +
                           Character[0].wis + " WIS | " +
                           Character[0].agi + " AGI | " +
                           Character[0].hp + " hp | " +
                           GetStringClss(Character[0].clss));
            VirtualConsole.Draw("\r\r\n\nUnder the hands of: " + Character[i].name);

        }

        static bool DetermineTurns(Characters a, Characters b)
        {
            if (a.agi < b.agi)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static void FightRound(Characters a, Characters b)
        {
            bool afirst = DetermineTurns(a, b);

            if (afirst)
            {
                int dmg = Convert.ToInt32(a.str * 0.3) + a.atkmod;
                if (dmg >= 0)
                {
                    b.hp -= dmg;
                    VirtualConsole.Draw(b.name + " takes " + dmg + " damage.");
                }
                else
                {
                    VirtualConsole.Draw(b.name + " takes 0 damage.");
                }

                if (b.hp > 0)
                {
                    dmg = Convert.ToInt32(b.str * 0.3) + b.atkmod - Convert.ToInt32(a.agi * 0.3f);
                    if (dmg >= 0)
                    {
                        a.hp -= dmg;
                        VirtualConsole.Draw(a.name + " takes " + dmg + " damage.");
                    }
                    else
                    {
                        VirtualConsole.Draw(a.name + " takes 0 damage.");
                    }
                }
            }
            else
            {
                int dmg = Convert.ToInt32(b.str * 0.3) + b.atkmod - Convert.ToInt32(a.agi * 0.3f);
                if (dmg >= 0)
                {
                    a.hp -= dmg;
                    VirtualConsole.Draw(a.name + " takes " + dmg + " damage.");
                }
                else
                {
                    VirtualConsole.Draw(a.name + " takes 0 damage.");
                }


                if (a.hp > 0)
                {
                    dmg = Convert.ToInt32(a.str * 0.3) + a.atkmod;
                    if (dmg >= 0)
                    {
                        b.hp -= dmg;
                        VirtualConsole.Draw(b.name + " takes " + dmg + " damage.");
                    }
                    else
                    {
                        VirtualConsole.Draw(b.name + " takes 0 damage.");
                    }

                }
            }

        }

        static void Fight(Characters a, Characters b)
        {
            VirtualConsole.Draw("\r\n" + a.name + " approaches " + b.name + "!");

            while (a.hp > 0 && b.hp > 0)
            {
                FightRound(a, b);
            }

            if (a.hp <= 0)
            {
                GameOver();
            }
            else if (b.hp <= 0)
            {
                VirtualConsole.Draw(a.name + " won the fight. " + b.name + " was defeated.");
                a.AdvanceCharacter();
            }
        }

        private static readonly string[] names
            = new string[21] { "Bandit", "Fiery", "Hexxed", "Troll", "Mutated", "Sleepy", "Sneaky", "Orcish",
                               "Demented", "Grown", "Ethernal", "Devilish", "Manly", "Girlish", "Small",
                               "Autistic", "Tiny", "Buffed", "Burning", "Slimy", "Sweaty"};
        private static readonly string[] suffix
            = new string[14] { "Scoundrel", "Guard", "Kobold", "Mage", "Warrior", "Thief", "Cleric", "Paladin",
                                "Bear", "Scorpion", "Imp", "Demon", "Spider", "Elemental"};


        static void ProcNPCs(int count)
        {
            int level = 6;
            Random rnd = new Random();
            for (int i = 6; i <= (count + 6); i++)
            {
                string name = names[rnd.Next(1, names.Length)] + " " + suffix[rnd.Next(1, suffix.Length)];
                Character.Add(new Characters(i, name, rnd.Next(1, level), rnd.Next(1, level), rnd.Next(1, level)));
                level += 3;
            }
        }


        private void Setup()
        {
            InitDB();
            ProcNPCs(difficulty);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Samohra - Another TextRPG";
            Setup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != String.Empty && Character[0].name == "Player")
            {
                Character[0].name = textBox2.Text;
                textBox2.Text = String.Empty;
            }

            if (!GameOverFlag)
            {
                if (i < Character.Count)
                {
                    VirtualConsole.Draw("\r\n\r\nLEVEL " + i);
                    Character[0].RefreshStats();
                    VirtualConsole.Draw(
                           Character[0].name + "\r\n" +
                           Character[0].str + " STR | " +
                           Character[0].wis + " WIS | " +
                           Character[0].agi + " AGI | " +
                           Character[0].hp + " hp | " +
                           GetStringClss(Character[0].clss)
                           );

                    Fight(Character[0], Character[i]);
                    i++;
                }
            }

            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            i = 1;
            VirtualConsole.Clear();
            VirtualConsole.Draw("New game started!\r\nChoose your player name (or leave the line blank " +
                "for default name):\r\n\r\n\r\nThen press 'Advance'!");
            RefreshDB();
            GameOverFlag = false;
        }
    }
}
