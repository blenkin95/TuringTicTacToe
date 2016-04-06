using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoeTuring
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
       //     if (e.keyChar.ToString() == "\r")
         //       button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Form1.setplayernames(p1.Text, p2.Text);
            this.Close();
        }
    }
}
