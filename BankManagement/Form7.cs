using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form8 form8 = new Form8();
            this.Hide();
            form8.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
