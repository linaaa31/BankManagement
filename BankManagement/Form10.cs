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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankManagement
{
    public partial class Form10 : Form
    {
        private string connectionString = "server=localhost;database=mydatabase;uid=root;password=admin;";
        private string cardNumber;
        public Form10(string cardNumber)
        {
            InitializeComponent();
            this.cardNumber = cardNumber;
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }
        private decimal GetUserBalance(string cardNumber)
        {
            decimal userBalance = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT balance FROM userstable WHERE cardNumber = @cardNumber";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        userBalance = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching user balance: " + ex.Message);
                }
            }

            return userBalance;
        }

        private decimal GetATMBalance()
        {
            decimal atmBalance = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT totalAmount FROM atm";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        atmBalance = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching ATM balance: " + ex.Message);
                }
            }

            return atmBalance;
        }

        private void DeductAmountFromBalance(decimal withdrawalAmount, string cardNumber)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE userstable SET balance = balance - @withdrawalAmount WHERE cardNumber = @cardNumber";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@withdrawalAmount", withdrawalAmount);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deducting amount from user balance: " + ex.Message);
                }
            }
        }

        private void DeductAmountFromATM(decimal withdrawalAmount)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE atm SET totalAmount = totalAmount - @withdrawalAmount";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@withdrawalAmount", withdrawalAmount);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deducting amount from ATM balance: " + ex.Message);
                }
            }
        }
        private bool ValidateWithdrawalAmount(decimal withdrawalAmount)
        {
            
            decimal userBalance = GetUserBalance(cardNumber); 

           
            decimal atmBalance = GetATMBalance();

            if (userBalance >= withdrawalAmount && atmBalance >= withdrawalAmount)
            {
                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal withdrawalAmount;
            if (!decimal.TryParse(textBox1.Text, out withdrawalAmount))
            {
                MessageBox.Show("Please enter a valid withdrawal amount.");
                return;
            }

          
            if (!ValidateWithdrawalAmount(withdrawalAmount))
            {
                MessageBox.Show("Not enough balance or ATM funds.");
                return;
            }

          
            DeductAmountFromBalance(withdrawalAmount,cardNumber);
            DeductAmountFromATM(withdrawalAmount);

            MessageBox.Show("Withdrawal successful!");
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
