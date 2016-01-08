using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gomoku
{
    public partial class Form2 : Form
    {
      
        public Form2()
        {
            InitializeComponent();
            Class1.pc = radioButton2.Checked;
            Class1.znak = radioButton3.Checked;
            Class1.kolvohod = 0;
            Class1.time = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();  
                this.Hide();
                f.Show();
                
                Class1.imya = textBox1.Text;
                if (checkBox1.Checked) { Class1.time1 = true; }
                else { Class1.time1 =false; }
                if (checkBox2.Checked) { Class1.kolvohod1 = true; }
                else { Class1.kolvohod1 = false; }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           Class1.pc = radioButton2.Checked;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Class1.znak = radioButton3.Checked;
        }

        
    }
}
