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
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InventoryManager
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
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
                Company.Items.Add(reader["Name"].ToString());
            }
            reader.Close();
            mySqlConnection.Close();
        }

        private void NewCompany_Button_Click(object sender, RoutedEventArgs e)
        {
            CompanyRegistrationForm companyRegistrationForm = new CompanyRegistrationForm();
            this.Close();
            companyRegistrationForm.Show();
        }

        private void Verify_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Company.Text))
            {
                MessageBox.Show("Please enter a company name before verifying", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(Company.Text.Length > 45)
            {
                MessageBox.Show("Name is too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string insertQuery = @"SELECT * FROM CompanyTable WHERE Name ='" + Company.Text + "'";
                cmd.CommandText = insertQuery;
                MySqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    if (reader["Name"].ToString() == Company.Text)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("The company you inputted does not exist, please register a new company instead or re-enter the company name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Company.Text = string.Empty;
                }
                else
                {
                    NewUserRegistration newUser = new NewUserRegistration(Company.Text);
                    this.Close();
                    newUser.Show();
                }
                reader.Close();
                mySqlConnection.Close();
            }

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}
