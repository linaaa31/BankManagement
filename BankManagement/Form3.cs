using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class Form3 : Form
    {
        private string connectionString = "server=localhost;database=mydatabase;uid=root;password=admin;";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        private bool VerifyPassword(string enteredPassword, string hashedPasswordFromDatabase)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
               
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                string hashedEnteredPassword = builder.ToString();

         
                return hashedEnteredPassword == hashedPasswordFromDatabase;
            }
        }
        private bool Login(string enteredUsername, string enteredPassword)
        {
            bool isAuthenticated = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string getPasswordQuery = "SELECT password FROM userstable WHERE username = @username";
                    MySqlCommand getPasswordCommand = new MySqlCommand(getPasswordQuery, connection);
                    getPasswordCommand.Parameters.AddWithValue("@username", enteredUsername);
                    string hashedPasswordFromDatabase = getPasswordCommand.ExecuteScalar()?.ToString();

                    if (hashedPasswordFromDatabase != null)
                    {
                        if (VerifyPassword(enteredPassword, hashedPasswordFromDatabase))
                        {
                            isAuthenticated = true;
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username not found!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return isAuthenticated;
        }

        private string FetchCardNumberFromDatabase(string username)
        {
            string cardNumber = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT cardNumber FROM userstable WHERE username = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        cardNumber = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching card number: " + ex.Message);
                }
            }

            return cardNumber;
        }

       
        private string FetchPinCodeFromDatabase(string username)
        {
            string pinCode = ""; 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT pinCode FROM userstable WHERE username = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        pinCode = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching pin code: " + ex.Message);
                }
            }

            return pinCode;
        }

    
        private decimal FetchBalanceFromDatabase(string username)
        {
            decimal balance = 0; 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT balance FROM userstable WHERE username = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

             
                    object result = command.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out balance))
                    {
                        balance = Convert.ToDecimal(balance);
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Error fetching balance: " + ex.Message);
                }
            }

            return balance;
        }
        private string DetermineCardType(string cardNumber)
        {
            char firstDigit = cardNumber[0];

            switch (firstDigit)
            {
                case '4':
                    return "Visa";
                case '5':
                    return "Mastercard";
                case '9':
                    return "Arca";
                default:
                    return "Unknown";
            }
        }

        private string DetermineBank(string cardNumber)
        {
            string firstThreeDigits = cardNumber.Substring(1, 3);

            switch (firstThreeDigits)
            {
                case "454":
                    return "Ardshin Bank";
                case "083":
                    return "Ameria Bank";
                case "318":
                    return "ID Bank";
                case "051":
                    return "Evoca Bank";
                case "578":
                    return "Ineco Bank";
                default:
                    return "Unknown";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (Login(username, password))
            {
                string cardNumber = FetchCardNumberFromDatabase(username);
                string pinCode = FetchPinCodeFromDatabase(username);
                decimal balance = FetchBalanceFromDatabase(username);
                string bank = DetermineBank(cardNumber);
                string cardType = DetermineCardType(cardNumber);




                Form9 form9 = new Form9();
                form9.UpdateUserInfo(cardNumber, pinCode, balance, bank, cardType);
                form9.Show();
                this.Hide();
            }
            
        }
    }
}
