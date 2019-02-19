using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasaT4A_Projekt
{
    class AdventureGenerator : Form1
    {
        // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        public AdventureGenerator()
        {
            
        }

        public AdventureGenerator(long seed)
        {
            // NIY
        }

        public AdventureGenerator(string parameters)
        {
            CustomAdventure adventure = new CustomAdventure(parameters);
        }

        public void Generate()
        {
            i = 1;
            VirtualConsole.Clear();
            VirtualConsole.Draw("New game started!\r\nChoose your name with the 'name' command" +
                " and the press Advance (or Enter)!\r\n");
            Form1.RefreshDB();
            GameOverFlag = false;
            StartNewGameText();
        }
    }
}
