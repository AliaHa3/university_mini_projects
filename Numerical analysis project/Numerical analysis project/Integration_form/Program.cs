using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathParserTK;

namespace Numerical_analysis
{
   
    public class Program
    {
        public static bool OpenDetailFormOnClose { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
      
        [STAThread]
       
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OpenDetailFormOnClose = false;
            Application.Run(new main_form());

            if (OpenDetailFormOnClose)
            {
                Application.Run(new Form1());
            }

        }
    }
}