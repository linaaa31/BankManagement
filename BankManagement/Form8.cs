using MySql.Data.MySqlClient;
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
    public partial class Form8 : Form
    {
        private string connectionString = "server=localhost;database=mydatabase;uid=root;password=admin;";
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
        private bool ValidateCredentials(string cardNumber, string pinCode)
        {
            bool isValid = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM userstable WHERE cardNumber = @cardNumber AND pinCode = @pinCode";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);
                    command.Parameters.AddWithValue("@pinCode", pinCode);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return isValid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cardNumber = textBox1.Text;
            string pinCode = textBox2.Text;

            if (ValidateCredentials(cardNumber, pinCode))
            { 
                Form10 form10 = new Form10(cardNumber);
                form10.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid card number or pin code. Please try again.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
