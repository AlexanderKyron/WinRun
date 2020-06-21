using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D3strUct0r
{
    static class Program
    {
        static Point m1 = MouseHelper.GetPosition();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void startMove()
        {
            int[] vel1 = Logic.getMouseVel( ref m1);
            Logic.getSingleton().Move(vel1);

        }
        public static void stopMove()
        {
            
        }

        
    }
    
}
