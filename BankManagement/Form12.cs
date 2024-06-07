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
    public partial class Form12 : Form
    {
        private string taskType;
        private DateTime selectedDateTime;
        public Form12(string taskType, DateTime selectedDateTime)
        {
            InitializeComponent();
            string taskNumber = GenerateTaskNumber(taskType, selectedDateTime);

            // Extract date and time from the selectedDateTime
            string date = selectedDateTime.ToString("dd.MM․yyyy"); // Format: day.month
            string time = selectedDateTime.ToString("HH:mm"); // Format: hours:minutes

           

            // Update labels with extracted information
            label1.Text = taskNumber;
            label2.Text = date;
            label3.Text = time;
        }
        private string GenerateTaskNumber(string taskType, DateTime selectedDateTime)
        {
            // Generate a random number between 1 and 10
            int randomNumber = new Random().Next(1, 11);

            // Combine task type, random number, to form the task number
            string taskNumber = $"{taskType}{randomNumber}";

            return taskNumber;
        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }
    }
}
