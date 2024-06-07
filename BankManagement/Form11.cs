using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class Form11 : Form
    {
        
        public Form11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            string taskType = "A";
            OpenNextPage(taskType, selectedDateTime);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            string taskType = "B";
            OpenNextPage(taskType, selectedDateTime);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            string taskType = "C";
            OpenNextPage(taskType, selectedDateTime);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            string taskType = "D";
            OpenNextPage(taskType, selectedDateTime);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            string taskType = "E";
            OpenNextPage(taskType, selectedDateTime);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            string taskType = "F";
            OpenNextPage(taskType, selectedDateTime);
        }
        private void OpenNextPage(string taskType, DateTime selectedDateTime)
        {
            Form12 form12 = new Form12(taskType, selectedDateTime);
            form12.Show();
            this.Hide();
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm";
        }
    }
}
