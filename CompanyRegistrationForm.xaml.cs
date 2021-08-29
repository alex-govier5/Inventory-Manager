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
    /// Interaction logic for CompanyRegistrationForm.xaml
    /// </summary>
    public partial class CompanyRegistrationForm : Window
    {
        public CompanyRegistrationForm()
        {
            InitializeComponent();
            CompanyIndustry.Items.Add("Aerospace");
            CompanyIndustry.Items.Add("Automotive");
            CompanyIndustry.Items.Add("Chemical and Biochemical");
            CompanyIndustry.Items.Add("Cleantech");
            CompanyIndustry.Items.Add("Cybersecurity");
            CompanyIndustry.Items.Add("Financial Services");
            CompanyIndustry.Items.Add("Food and Beverage Manufacturing");
            CompanyIndustry.Items.Add("Forestry");
            CompanyIndustry.Items.Add("Industrial Automation and Robotics");
            CompanyIndustry.Items.Add("Information Technology");
            CompanyIndustry.Items.Add("Life Sciences");
            CompanyIndustry.Items.Add("Mining");
            CompanyIndustry.Items.Add("Tourism");
            CompanyIndustry.Items.Add("Other");


        }

        private void Register_Company_Button_Click(object sender, RoutedEventArgs e)
        {
            if(VerifyInfo(CompanyName.Text, CompanyIndustry.Text, CompanyCEO.Text, CompanySlogan.Text) == true)
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string insertQuery = @"INSERT INTO CompanyTable VALUES (DEFAULT, '"+CompanyName.Text+"', '"+CompanyIndustry.Text+"', '"+CompanyCEO.Text+"', '"+CompanySlogan.Text+"', 1)";
                cmd.CommandText = insertQuery;
                cmd.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = mySqlConnection;
                string companyNameNoSpaces = String.Concat(CompanyName.Text.Where(c => !Char.IsWhiteSpace(c)));
                string query = @"CREATE TABLE "+companyNameNoSpaces+ "Users (ID int AUTO_INCREMENT KEY, CompanyName varchar(50), FirstName varchar(50), LastName varchar(50), Age int, Gender varchar(50), PositionTitle varchar(100), Username varchar(50), Password varchar(50), Status int)";
                cmd2.CommandText = query;
                cmd2.ExecuteNonQuery();
                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.Connection = mySqlConnection;
                string query2 = @"CREATE TABLE " + companyNameNoSpaces + "Departments (Name varchar(50))";
                cmd3.CommandText = query2;
                cmd3.ExecuteNonQuery();
                string query3 = @"CREATE TABLE " + companyNameNoSpaces + "Suppliers (ID int, Name varchar(50), ProductsSupplied int)";
                cmd3.CommandText = query3;
                cmd3.ExecuteNonQuery();
                string query4 = @"CREATE TABLE "+companyNameNoSpaces+"Purchases (Transaction int AUTO_INCREMENT KEY, Hour int, Minute int, Day int, Month int, Year int, Product varchar(50), Supplier varchar(50), Quantity int, PPI double, Total double)";
                string query5 = @"CREATE TABLE " + companyNameNoSpaces + "Sales (Transaction int AUTO_INCREMENT KEY, Hour int, Minute int, Day int, Month int, Year int, Product varchar(50), Supplier varchar(50), Quantity int, PPI double, Total double)";
                cmd.CommandText = query4;
                cmd.ExecuteNonQuery();
                cmd.CommandText = query5;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your company has now been registered in our database! Now please register as a new user for your company", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                NewUserRegistration newUser = new NewUserRegistration(CompanyName.Text);
                this.Close();
                newUser.Show();
            }


        }

        private bool VerifyInfo(string name, string industry, string ceo, string slogan)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(industry) || string.IsNullOrEmpty(ceo) || string.IsNullOrEmpty(slogan))
            {
                MessageBox.Show("One of the required fields was empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyName.Text = string.Empty;
                CompanyIndustry.Text = string.Empty;
                CompanyCEO.Text = string.Empty;
                CompanySlogan.Text = string.Empty;
                return false;
            }

            if(name.Length > 45 || industry.Length > 45 || ceo.Length > 45 || slogan.Length > 95)
            {
                MessageBox.Show("One of the required fields was too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyName.Text = string.Empty;
                CompanyIndustry.Text = string.Empty;
                CompanyCEO.Text = string.Empty;
                CompanySlogan.Text = string.Empty;
                return false;
            }


            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string insertQuery = @"SELECT * FROM CompanyTable WHERE Name ='"+name+"'";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                if(reader["Name"].ToString() == name)
                {
                    count++;
                }
            }

            if(count > 0)
            {
                MessageBox.Show("The company you inputted already exists, please go back and log in or register as a new user for your company", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyName.Text = string.Empty;
                CompanyIndustry.Text = string.Empty;
                CompanyCEO.Text = string.Empty;
                CompanySlogan.Text = string.Empty;
                return false;
            }

            if (Regex.IsMatch(ceo, @"^[a-zA-Z ]+$") == false)
            { 
                MessageBox.Show("The CEO can contain only letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CompanyIndustry.Text = string.Empty;
                CompanyCEO.Text = string.Empty;
                return false;
            }
            reader.Close();
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
