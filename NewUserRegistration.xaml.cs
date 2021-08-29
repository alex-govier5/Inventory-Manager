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
using System.Text.RegularExpressions;

namespace InventoryManager
{
    /// <summary>
    /// Interaction logic for NewUserRegistration.xaml
    /// </summary>
    public partial class NewUserRegistration : Window
    {
        string company;
        public NewUserRegistration(string company)
        {
            InitializeComponent();
            this.company = company;
            Title.Text = company + " New User Registration Form";
            Gender.Items.Add("Male");
            Gender.Items.Add("Female");
            Gender.Items.Add("Other");
           
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            if(VerifyInfo(FirstName.Text, LastName.Text, Age.Text, Gender.Text, Position.Text, EmployeeUsername.Text, EmployeePassword.Text) == true)
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
                string insertQuery = @"INSERT INTO "+companyNameNoSpaces+"Users VALUES (DEFAULT, '"+company+"', '"+FirstName.Text+"', '"+LastName.Text+"', "+Age.Text+", '"+Gender.Text+"', '"+Position.Text+"', '"+EmployeeUsername.Text+"', '"+EmployeePassword.Text+"', 1)";
                cmd.CommandText = insertQuery;
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();
                MessageBox.Show("Congratulations, you have successfully registered as a new user, now please log in to access inventory", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }

        private bool VerifyInfo(string firstName, string lastName, string age, string gender, string position, string username, string password)
        {
            if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(age) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("One of the required fields was empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                FirstName.Text = string.Empty;
                LastName.Text = string.Empty;
                Age.Text = string.Empty;
                Gender.Text = string.Empty;
                Position.Text = string.Empty;
                EmployeeUsername.Text = string.Empty;
                EmployeePassword.Text = string.Empty;
                return false;
            }

            if (firstName.Length > 45|| lastName.Length>45 || username.Length>45 || password.Length>45)
            {
                MessageBox.Show("One of the required fields was too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                FirstName.Text = string.Empty;
                LastName.Text = string.Empty;
                Age.Text = string.Empty;
                Gender.Text = string.Empty;
                Position.Text = string.Empty;
                EmployeeUsername.Text = string.Empty;
                EmployeePassword.Text = string.Empty;
                return false;
            }

            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery2 = @"SELECT * FROM "+companyNameNoSpaces+"Users WHERE Username = '"+username+"'";
            cmd2.CommandText = insertQuery2;
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            int count2 = 0;
            while (reader2.Read())
            {
                if(reader2["Username"].ToString() == username)
                {
                    count2++;
                }
            }

            if(count2 > 0)
            {
                MessageBox.Show("The username you entered is already in use for this company, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                EmployeeUsername.Text = string.Empty;
                return false;
            }

            if (Regex.IsMatch(firstName, @"^[a-zA-Z]+$") == false)
            {
                MessageBox.Show("First name can only contain letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                FirstName.Text = string.Empty;
                return false;
            }
            if (Regex.IsMatch(lastName, @"^[a-zA-Z]+$") == false)
            {
                MessageBox.Show("Last name can only contain letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LastName.Text = string.Empty;
                return false;
            }
            if (Regex.IsMatch(gender, @"^[a-zA-Z]+$") == false)
            {
                MessageBox.Show("Gender can only contain letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Gender.Text = string.Empty;
                return false;
            }
            if (Regex.IsMatch(position, @"^[a-zA-Z]+$") == false)
            {
                MessageBox.Show("Position can only contain letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Position.Text = string.Empty;
                return false;
            }

            if (Regex.IsMatch(age, @"^[0-9]+$") == false)
            {
                MessageBox.Show("Age must be inputted as a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Age.Text = string.Empty;
                return false;
            }

            int empAge = Int32.Parse(age);
            if(empAge < 18)
            {
                MessageBox.Show("Sorry, you are too young to work for this company", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Age.Text = string.Empty;
                return false;

            }
            if(empAge > 110)
            {
                MessageBox.Show("Sorry, you are too old to work for this company", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Age.Text = string.Empty;
                return false;
            }

            if (Regex.IsMatch(username, @"^[a-z0-9._!@#$%&?]+$") == false)
            {
                MessageBox.Show("Username can only contain lowercase letters, numbers and these symbols => ._!@#$%&?", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                EmployeeUsername.Text = string.Empty;
                return false;
            }
            if (Regex.IsMatch(password, @"^[a-zA-Z0-9._!@#$%&?]+$") == false)
            {
                MessageBox.Show("Password can only contain lowercase letters, uppercase letters, numbers and these symbols => ._!@#$%&?", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                EmployeePassword.Text = string.Empty;
                return false;
            }

            int lower = 0;
            int upper = 0;
            int number = 0;
            int special = 0;
            
            foreach(char c in password)
            {
                if(Char.IsUpper(c))
                {
                    upper++;
                }
                if (Char.IsNumber(c))
                {
                    number++;
                }
                if (Char.IsLower(c))
                {
                    lower++;
                }
                if (Char.Equals(c,'.') || Char.Equals(c, '_') || Char.Equals(c, '!') || Char.Equals(c, '@') || Char.Equals(c, '#') || Char.Equals(c, '$') || Char.Equals(c, '%') || Char.Equals(c, '&') || Char.Equals(c, '?'))
                {
                    special++;
                }
            }

            if (lower == 0 || upper == 0 || number == 0 || special == 0)
            {
                MessageBox.Show("Password must contain at least one lowercase letter, one uppercase letter, one number and one of these symbols => ._!@#$%&?", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                EmployeePassword.Text = string.Empty;
                return false;
            }
            reader2.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm form = new RegistrationForm();
            this.Close();
            form.Show();
        }
    }
}
