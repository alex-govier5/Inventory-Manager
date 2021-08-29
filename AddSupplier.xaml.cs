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
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Window
    {
        string company;
        public AddSupplier(string company)
        {
            InitializeComponent();
            this.company = company;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if(VerifyInfo(SupplierID.Text, SupplierName.Text, SupplierQuantity.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
                string insertQuery = @"INSERT INTO "+companyNameNoSpaces+"Suppliers VALUES ("+SupplierID.Text+", '"+SupplierName.Text+"', "+SupplierQuantity.Text+")";
                cmd.CommandText = insertQuery;
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();
                MessageBox.Show("Your new supplier has been added to the list! You will now be taken back to the supplier page", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                SupplierPage pg = new SupplierPage(company);
                this.Close();
                pg.Show();
            }
        }

        private bool VerifyInfo(string id, string name, string quantity)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("One of the required fields was empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierID.Text = string.Empty;
                SupplierName.Text = string.Empty;
                SupplierQuantity.Text = string.Empty;
                return false;
            }

            if(name.Length > 45)
            {
                MessageBox.Show("Name is too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierName.Text = string.Empty;
                return false;
            }

            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM " + companyNameNoSpaces + "Suppliers";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader["ID"].ToString() == id)
                {
                    MessageBox.Show("There is already a supplier with this ID, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    SupplierID.Text = string.Empty;
                    return false;
                }
                if(reader["Name"].ToString() == name)
                {
                    MessageBox.Show("There is already a supplier with this name, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    SupplierName.Text = string.Empty;
                    return false;
                }

            }
            int quan = 0;
            try
            {
                quan = Int32.Parse(quantity);
            }
            catch(FormatException e)
            {
                MessageBox.Show("The number of products supplied must be an integer value, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierQuantity.Text = string.Empty;
                return false;
            }

            if (quan < 0)
            {
                MessageBox.Show("The number of products supplied must be a positive integer value, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierQuantity.Text = string.Empty;
                return false;
            }

            int ID = 0;
            try
            {
                ID = Int32.Parse(id);
            }
            catch (FormatException e)
            {
                MessageBox.Show("The supplier ID must be an integer value, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierID.Text = string.Empty;
                return false;
            }

            if (ID < 0)
            {
                MessageBox.Show("The supplier ID must be a positive integer value, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierID.Text = string.Empty;
                return false;
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            SupplierPage pg = new SupplierPage(company);
            this.Close();
            pg.Show();
        }
    }
}
