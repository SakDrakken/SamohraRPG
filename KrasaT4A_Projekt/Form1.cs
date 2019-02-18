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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                Proceed(textBox2.Text);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

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
                           Character[0].atkmod + " +ATK | " +
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
                int dmg = Convert.ToInt32(a.str * 0.3) + a.atkmod - Convert.ToInt32(b.agi * 0.1f);
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
                    dmg = Convert.ToInt32(a.str * 0.3) + a.atkmod - Convert.ToInt32(b.agi * 0.1f);
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
            Character[difficulty - 1] = new Characters(i, "Vlokesh, the Demon", 40, 40, 40);
        }

        private void Setup()
        {
            InitDB();
            ProcNPCs(difficulty);
            VirtualConsole.Draw("Start a new game via the menu or press CTRL+SHIFT+G for a new adventure.");
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
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

        private void Help()
        {
            VirtualConsole.Draw("List of all commands:\n");
            string[] commands = { "help", "fight", "name" };

            foreach (string command in commands)
            {
                VirtualConsole.Draw(command + "\r\n");
            }
        }

        private void Help(string command)
        {
            switch (command)
            {
                case "help":
                    VirtualConsole.Draw("Shows all commands or command usage. Usage: 'help [command]'");
                    break;
                default:
                    break;
            }
        }

        private void Proceed(string input)
        {           
            string[] arguments = input.ToLower().Split(' ');

            if(arguments[0].ToLower() == "name")
            {
                arguments = input.Split(' ');
            }
            
                        
            if (!GameOverFlag)
            {
                switch (arguments[0])
                {
                    case "name":
                        try
                        {
                            VirtualConsole.Draw("\r\nChanged name to " + arguments[1]);
                            Character[0].name = arguments[1];
                        }
                        catch (Exception)
                        {
                            VirtualConsole.Draw("\r\nNo name entered. Usage: 'name [name]'");
                        }                                            
                        break;

                    case "help":
                        VirtualConsole.Draw("\r\nShowing help...");
                        try
                        {
                            Help(arguments[1]);
                        }
                        catch (Exception)
                        {
                            Help();
                        }                    
                        break;

                    case "fight":
                        if (i < Character.Count)
                        {
                            VirtualConsole.Draw("\r\n\r\nFLOOR " + (difficulty + 1 - i));
                            Character[0].RefreshStats();
                            VirtualConsole.Draw(
                                   Character[0].name + "\r\n" +
                                   Character[0].atkmod + " +ATK | " +
                                   Character[0].str + " STR | " +
                                   Character[0].wis + " WIS | " +
                                   Character[0].agi + " AGI | " +
                                   Character[0].hp + " hp | " +
                                   GetStringClss(Character[0].clss)
                                   );

                            Fight(Character[0], Character[i]);
                            i++;
                        }
                        else if(i == 0)
                        {
                            VirtualConsole.Draw("\r\nCONGRATULATIONS!\r\nYou have defeated all the" +
                                "guardians and escaped from the tower..." +
                                "\r\n\r\nPress CTRL-SHIFT-G to start a new adventure...");
                            GameOverFlag = true;
                        }
                        break;
                    case "stats":
                        VirtualConsole.Draw("\r\nStats:\r\n" + Character[0].name + " | " +
                           Character[0].atkmod + " +ATK | " +
                           Character[0].str + " STR | " +
                           Character[0].wis + " WIS | " +
                           Character[0].agi + " AGI | " +
                           Character[0].hp + " hp | " +
                           Character[0].upg + " UPG | " +
                           GetStringClss(Character[0].clss));
                        break;
                    case "enemy":
                        VirtualConsole.Draw("\r\nEnemy stats:\r\n" + Character[i].name + " | " +
                           Character[i].str + " STR | " +
                           Character[i].wis + " WIS | " +
                           Character[i].agi + " AGI | " +
                           Character[i].hp + " hp");
                        break;
                    case "do":
                        WhatToDo();
                        break;
                    case "upgrade":
                        if (Character[0].upg >= 1)
                        {
                            switch (arguments[1])
                            {
                                case "str":
                                    Character[0].str++;
                                    Character[0].upg--;
                                    VirtualConsole.Draw("\r\nSTR upgraded!");
                                    break;
                                case "wis":
                                    Character[0].wis++;
                                    Character[0].upg--;
                                    VirtualConsole.Draw("\r\nWIS upgraded!");
                                    break;
                                case "agi":
                                    Character[0].agi++;
                                    Character[0].upg--;
                                    VirtualConsole.Draw("\r\nAGI upgraded!");
                                    break;
                                case "atk":
                                    Character[0].atkmod += 3;
                                    Character[0].upg--;
                                    VirtualConsole.Draw("\r\nATK upgraded by 3 points!");
                                    break;
                                case "hp":
                                    Character[0].bnshp += 2;
                                    Character[0].upg--;
                                    VirtualConsole.Draw("\r\nHP upgraded by 2 points!");
                                    Character[0].RefreshStats();
                                    break;
                                default:
                                    VirtualConsole.Draw("\r\nWrong stat chosen. [str][wis][agi][hp][atk]");
                                    break;
                            }                            
                        }
                        break;
                    default:
                        try
                        {
                            if (arguments[0] == String.Empty)
                            {

                            }
                            else
                            {
                                VirtualConsole.Draw("Unrecognized command.");
                            }
                        }
                        catch (Exception)
                        {

                        }                                      
                        break;
                }                                       
            }

            textBox2.Text = String.Empty;            
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Proceed(textBox2.Text);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void WhatToDo()
        {
            VirtualConsole.Draw("\r\n\r\nYou can [do]:" +
                "\r\n[fight]> Proceeds to next floor guardian." +
                "\r\n[stats]> Shows player stats. " +
                "\r\n[enemy]> Shows enemy's stats. " +
                "\r\n[upgrade [stat]]> Spends upgrade points to stats.");
        }

        private void StartNewGameText()
        {
            VirtualConsole.Draw("Welcome to the world of Samohra! You're a prisoner locked in a tower\r\n" +
                "and your goal is to escape it. This game gets harder with every round. Every floor\r\n" +
                "has it's guardian. You can proceed to the next floor, only if you defeat the floor's\r\n" +
                "guardian.");
            WhatToDo();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            i = 1;
            VirtualConsole.Clear();
            VirtualConsole.Draw("New game started!\r\nChoose your name with the 'name' command" +
                " and the press Advance (or Enter)!\r\n");
            RefreshDB();
            GameOverFlag = false;
            StartNewGameText();
        }
    }
}
