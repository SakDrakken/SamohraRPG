using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrasaT4A_Projekt
{
    public class VirtualConsole : Form1
    {
        public static void Draw(string input)
        {
            textBox1.Text += input + "\r\n";
        }

        public static void Clear()
        {
            textBox1.Text = String.Empty;
        }
    }
}
