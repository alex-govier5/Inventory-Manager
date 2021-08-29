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
    /// Interaction logic for AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        string companyName;
        public AddDepartment(string name)
        {
            InitializeComponent();
            this.companyName = name;
        }

        private bool VerifyInfo(string department)
        {
            if (string.IsNullOrEmpty(department))
            {
                MessageBox.Show("Required field was empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NewDepartment.Text = string.Empty;
                return false;
            }
            if(department.Length > 45)
            {
                MessageBox.Show("Name too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NewDepartment.Text = string.Empty;
                return false;
            }

            if (Regex.IsMatch(department, @"^[a-zA-Z ]+$") == false)
            {
                MessageBox.Show("Department can only contain letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NewDepartment.Text = string.Empty;
                return false;
            }
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(companyName.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM "+companyNameNoSpaces+"Departments";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader["Name"].ToString() == department)
                {
                    MessageBox.Show("There is already a department with this name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    NewDepartment.Text = string.Empty;
                    return false;
                }
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyInfo(NewDepartment.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string companyNameNoSpaces = String.Concat(companyName.Where(c => !Char.IsWhiteSpace(c)));
                string insertQuery = @"INSERT INTO "+companyNameNoSpaces+"Departments VALUES ('"+NewDepartment.Text+"')";
                cmd.CommandText = insertQuery;
                cmd.ExecuteNonQuery();
                string depNameNoSpaces = String.Concat(NewDepartment.Text.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery2 = @"CREATE TABLE "+depNameNoSpaces+"Inventory (ID int, ProductName varchar(50), Department varchar(50), Quantity int, Price double, Total double, Supplier varchar(50))";
                cmd.CommandText = insertquery2;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your new department has been added", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                InventoryPage page = new InventoryPage(companyName);
                this.Close();
                page.Show();
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            InventoryPage ip = new InventoryPage(companyName);
            this.Close();
            ip.Show();
        }

       
    }
}
