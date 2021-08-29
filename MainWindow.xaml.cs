using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InventoryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string insertQuery = @"SELECT * FROM CompanyTable";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CompanyName.Items.Add(reader["Name"].ToString());
            }
            reader.Close();
            mySqlConnection.Close();
        }
        private bool VerifyInfo(string username, string password, string companyName)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("One of the required fields was empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyName.Text = string.Empty;
                Username.Text = string.Empty;
                Password.Clear();
                return false;
            }
            if(username.Length>45 || password.Length > 45)
            {
                MessageBox.Show("One of the required fields was too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyName.Text = string.Empty;
                Username.Text = string.Empty;
                Password.Clear();
                return false;
            }
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string insertQuery = @"SELECT * FROM CompanyTable WHERE Name ='" + companyName + "'";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                if (reader["Name"].ToString() == companyName)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                MessageBox.Show("The company you inputted does not exist, please register a new company instead or re-enter the company name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyName.Text = string.Empty;
                return false;
            }
            reader.Close();

            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(CompanyName.Text.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery2 = @"SELECT * FROM "+companyNameNoSpaces+"Users WHERE Username = '"+username+"' AND Password = '"+password+"'";
            cmd2.CommandText = insertQuery2;
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            int count2 = 0;
            while (reader2.Read())
            {
                if (reader2["Username"].ToString() == username && reader2["Password"].ToString() == password)
                {
                    count2++;
                }
            }

            if(count2 == 0)
            {
                MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Username.Text = string.Empty;
                Password.Clear();
                return false;
            }
            reader2.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if(VerifyInfo(Username.Text, Password.Password, CompanyName.Text) == true)
            {
                MessageBox.Show("Login successful!", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                InventoryPage inventoryPage = new InventoryPage(CompanyName.Text);
                this.Close();
                inventoryPage.Show();
            }
        }

        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            this.Close();
            registrationForm.Show();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (VerifyInfo(Username.Text, Password.Password, CompanyName.Text) == true)
                {
                    MessageBox.Show("Login successful!", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                    InventoryPage inventoryPage = new InventoryPage(CompanyName.Text);
                    this.Close();
                    inventoryPage.Show();
                }
            }
        }

    }
}
