using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Integration_form;

namespace Numerical_analysis
{
    public partial class main_form : Form
    {
        public main_form()
        {
            InitializeComponent();
        
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
           
            Program.OpenDetailFormOnClose = true;
            Close();



        }

        private void main_form_Load(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            about_form about = new about_form();
            about.Show();

        }
    }
}
