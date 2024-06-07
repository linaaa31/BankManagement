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
    public partial class Form6 : Form
    {

        private string connectionString = "server=localhost;database=mydatabase;uid=root;password=admin;";
        public Form6()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int thousandCount = string.IsNullOrEmpty(textBox1.Text) ? 0: int.Parse(textBox1.Text);
            int twoThousandCount = string.IsNullOrEmpty(textBox2.Text) ? 0 : int.Parse(textBox2.Text);
            int fiveThousandCount = string.IsNullOrEmpty(textBox3.Text) ? 0 : int.Parse(textBox3.Text);
            int tenThousandCount = string.IsNullOrEmpty(textBox4.Text) ? 0 : int.Parse(textBox4.Text);
            int twentyThousandCount = string.IsNullOrEmpty(textBox5.Text) ? 0 : int.Parse(textBox5.Text);
            int fiftyThousandCount = string.IsNullOrEmpty(textBox6.Text) ? 0 : int.Parse(textBox6.Text);
            int oneHundredThousandCount = string.IsNullOrEmpty(textBox7.Text) ? 0 : int.Parse(textBox7.Text);

            int totalAmount = (thousandCount * 1000) + (twoThousandCount * 2000) + (fiveThousandCount * 5000) +
                                  (tenThousandCount * 10000) + (twentyThousandCount * 20000) + (fiftyThousandCount * 50000) + (oneHundredThousandCount * 100000);

            
            label8.Text = totalAmount + ".00 AMD";
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int thousandCount = string.IsNullOrEmpty(textBox1.Text) ? 0 : int.Parse(textBox1.Text);
            int twoThousandCount = string.IsNullOrEmpty(textBox2.Text) ? 0 : int.Parse(textBox2.Text);
            int fiveThousandCount = string.IsNullOrEmpty(textBox3.Text) ? 0 : int.Parse(textBox3.Text);
            int tenThousandCount = string.IsNullOrEmpty(textBox4.Text) ? 0 : int.Parse(textBox4.Text);
            int twentyThousandCount = string.IsNullOrEmpty(textBox5.Text) ? 0 : int.Parse(textBox5.Text);
            int fiftyThousandCount = string.IsNullOrEmpty(textBox6.Text) ? 0 : int.Parse(textBox6.Text);
            int oneHundredThousandCount = string.IsNullOrEmpty(textBox7.Text) ? 0 : int.Parse(textBox7.Text);

            int totalAmount = (thousandCount * 1000) + (twoThousandCount * 2000) + (fiveThousandCount * 5000) +
                                  (tenThousandCount * 10000) + (twentyThousandCount * 20000) + (fiftyThousandCount * 50000) + (oneHundredThousandCount * 100000);
            bool recordExists = CheckIfRecordExists();

           
            if (recordExists)
            {
                UpdateATMTable(thousandCount, twoThousandCount, fiveThousandCount, tenThousandCount, twentyThousandCount,
                               fiftyThousandCount, oneHundredThousandCount, totalAmount);
            }
            else
            {
                AddToATMTable(thousandCount, twoThousandCount, fiveThousandCount, tenThousandCount, twentyThousandCount,
                              fiftyThousandCount, oneHundredThousandCount, totalAmount);
            }
        }
        private void AddToATMTable(int thousandCount, int twoThousandCount, int fiveThousandCount, int tenThousandCount, int twentyThousandCount,int fiftyThousandCount, int oneHundredThousandCount,int totalAmount)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO atm (thousandCount, twoThousandCount, fiveThousandCount, tenThousandCount,  twentyThousandCount, fiftyThousandCount,oneHundredThousandCount, totalAmount) " +
                                         "VALUES (@thousandCount, @twoThousandCount, @fiveThousandCount, @tenThousandCount, @twentyThousandCount,@fiftyThousandCount,@oneHundredThousandCount, @totalAmount)";
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@thousandCount", thousandCount);
                    command.Parameters.AddWithValue("@twoThousandCount", twoThousandCount);
                    command.Parameters.AddWithValue("@fiveThousandCount", fiveThousandCount);
                    command.Parameters.AddWithValue("@tenThousandCount", tenThousandCount);
                    command.Parameters.AddWithValue("@twentyThousandCount", twentyThousandCount);
                    command.Parameters.AddWithValue("@fiftyThousandCount", fiftyThousandCount);
                    command.Parameters.AddWithValue("@oneHundredThousandCount", oneHundredThousandCount);
                    command.Parameters.AddWithValue("@totalAmount", totalAmount);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Information added to ATM table successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void UpdateATMTable(int thousandCount, int twoThousandCount, int fiveThousandCount, int tenThousandCount,
                            int twentyThousandCount, int fiftyThousandCount, int oneHundredThousandCount, int totalAmount)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE atm SET thousandCount = thousandCount + @thousandCount, " +
                                         "twoThousandCount = twoThousandCount + @twoThousandCount, " +
                                         "fiveThousandCount = fiveThousandCount + @fiveThousandCount, " +
                                         "tenThousandCount = tenThousandCount + @tenThousandCount, " +
                                         "twentyThousandCount = twentyThousandCount + @twentyThousandCount, " +
                                         "fiftyThousandCount =fiftyThousandCount + @fiftyThousandCount, " +
                                         "oneHundredThousandCount = oneHundredThousandCount + @oneHundredThousandCount, " +
                                         "totalAmount = totalAmount + @totalAmount";
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@thousandCount", thousandCount);
                    command.Parameters.AddWithValue("@twoThousandCount", twoThousandCount);
                    command.Parameters.AddWithValue("@fiveThousandCount", fiveThousandCount);
                    command.Parameters.AddWithValue("@tenThousandCount", tenThousandCount);
                    command.Parameters.AddWithValue("@twentyThousandCount", twentyThousandCount);
                    command.Parameters.AddWithValue("@fiftyThousandCount", fiftyThousandCount);
                    command.Parameters.AddWithValue("@oneHundredThousandCount", oneHundredThousandCount);
                    command.Parameters.AddWithValue("@totalAmount", totalAmount);
                    command.ExecuteNonQuery();

                    MessageBox.Show("ATM information updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private bool CheckIfRecordExists()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM atm";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
