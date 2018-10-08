using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanzenBord
{
    public partial class Bord : Form
    {
        public Bord()
        {
            InitializeComponent();
            RulesBox.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            RulesBox.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            RulesBox.Visible = false;
        }

        private void playBoard_Click(object sender, EventArgs e)
        {

        }
    }
}
