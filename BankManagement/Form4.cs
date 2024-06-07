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
    public partial class Form4 : Form
    {
        private string connectionString = "server=localhost;database=mydatabase;uid=root;password=admin;";
        private string username;
        private string password;

        public Form4(string username, string password)
        {
            InitializeComponent();

            this.username = username;
            this.password = password;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        public int RandCardType()
        {
            int[] arr = { 4, 5, 9 };
            Random rnd = new Random();
            int index = rnd.Next(0, arr.Length);
            int type = arr[index];
            return type;
        }
        public string RandBank()
        {
            string[] arr = { "454", "083", "318", "051", "578" };
            Random random = new Random();
            int index = random.Next(0, arr.Length);
            string bank = arr[index];
            return bank;

        }

        public string GenerateCardNumber()
        {
            StringBuilder cardNumber = new StringBuilder();


            Random random = new Random();


            cardNumber.Append(RandCardType().ToString());

            cardNumber.Append(RandBank());

            for (int i = 0; i < 11; i++)
            {
                cardNumber.Append(random.Next(0, 10));
            }
            int last = CalculateChecksum(cardNumber.ToString());

            cardNumber.Append(last);

            for (int i = 4; i < cardNumber.Length; i += 5)
            {
                cardNumber.Insert(i, " ");
            }
            return cardNumber.ToString();
        }


        private int CalculateChecksum(string cardNumber)
        {
            int sum = 0;
            bool doubleDigit = true;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            int checksum = (10 - (sum % 10)) % 10;
            return checksum;
        }
        private string GeneratePinCode(int length)
        {
            const string chars = "0123456789";
            StringBuilder pinBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                pinBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return pinBuilder.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string number = GenerateCardNumber();
            label1.Text = number;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pinCode = GeneratePinCode(4);
            label2.Text = pinCode;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cardNumber = label1.Text;
            string pinCode = label2.Text;
            decimal balance = decimal.Parse(textBox1.Text);

            RegisterUser(username, password, cardNumber, pinCode, balance);
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

               
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void RegisterUser(string username, string password, string cardNumber, string pinCode, decimal balance)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string hashedPassword = HashPassword(password);
                    string insertUserQuery = "INSERT INTO userstable (username, password,cardNumber, pinCode, balance) VALUES (@username, @password, @cardNumber, @pinCode, @balance)";
                    MySqlCommand insertUserCommand = new MySqlCommand(insertUserQuery, connection);
                    insertUserCommand.Parameters.AddWithValue("@username", username);
                    insertUserCommand.Parameters.AddWithValue("@password", hashedPassword);
                    insertUserCommand.Parameters.AddWithValue("@cardNumber", cardNumber);
                    insertUserCommand.Parameters.AddWithValue("@pinCode", pinCode);
                    insertUserCommand.Parameters.AddWithValue("@balance", balance);
                    insertUserCommand.ExecuteNonQuery();

                    MessageBox.Show("User registered successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Hide();
            form5.ShowDialog();
        }
    }
}
