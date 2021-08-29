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
    /// Interaction logic for EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Window
    {
        string company;
        string supplier;
        public EditSupplier(string company, string supplier)
        {
            InitializeComponent();
            this.company = company;
            this.supplier = supplier;
            SupplierName.Text = supplier;
            IDText.Visibility = Visibility.Hidden;
            IDTextBox.Visibility = Visibility.Hidden;
            IDButton.Visibility = Visibility.Hidden;
            NameText.Visibility = Visibility.Hidden;
            NameTextBox.Visibility = Visibility.Hidden;
            NameButton.Visibility = Visibility.Hidden;
            NumberText.Visibility = Visibility.Hidden;
            NumberTextBox.Visibility = Visibility.Hidden;
            NumberButton.Visibility = Visibility.Hidden;
           
        }

        private void ID_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IDText.Visibility == Visibility.Hidden && IDTextBox.Visibility == Visibility.Hidden && IDButton.Visibility == Visibility.Hidden)
            {
                IDText.Visibility = Visibility.Visible;
                IDTextBox.Visibility = Visibility.Visible;
                IDButton.Visibility = Visibility.Visible;
            }
            else
            {
                IDText.Visibility = Visibility.Hidden;
                IDTextBox.Visibility = Visibility.Hidden;
                IDButton.Visibility = Visibility.Hidden;
            }

        }

        private void Finish_ID_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyID(IDTextBox.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + comNameNoSpaces + "Suppliers SET ID = " + IDTextBox.Text + " WHERE Name = '" + supplier + "'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The supplier's ID has successfully been changed, you will now be taken back to the supplier page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                SupplierPage de = new SupplierPage(company);
                this.Close();
                de.Show();
            }
        }

        private bool VerifyID(string ID)
        {
            if (String.IsNullOrEmpty(IDTextBox.Text))
            {
                MessageBox.Show("ID cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IDTextBox.Text = string.Empty;
                return false;
            }
            int id = 0;
            try
            {
                id = Int32.Parse(IDTextBox.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("ID must be an integer, not a decimal or letters or symbols", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IDTextBox.Text = string.Empty;
                return false;
            }
            if (id < 0)
            {
                MessageBox.Show("ID must be a positive integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IDTextBox.Text = string.Empty;
                return false;
            }
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM " + comNameNoSpaces + "Suppliers";
            cmd.CommandText = insertquery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["ID"].ToString() == ID)
                {
                    MessageBox.Show("There is already a supplier with this ID, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    IDTextBox.Text = string.Empty;
                    return false;
                }
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Name_Button_Click(object sender, RoutedEventArgs e)
        {
            if (NameText.Visibility == Visibility.Hidden && NameTextBox.Visibility == Visibility.Hidden && NameButton.Visibility == Visibility.Hidden)
            {
                NameText.Visibility = Visibility.Visible;
                NameTextBox.Visibility = Visibility.Visible;
                NameButton.Visibility = Visibility.Visible;
            }
            else
            {
                NameText.Visibility = Visibility.Hidden;
                NameTextBox.Visibility = Visibility.Hidden;
                NameButton.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_Name_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyName(NameTextBox.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + comNameNoSpaces + "Suppliers SET Name = '" + NameTextBox.Text + "' WHERE Name = '" + supplier + "'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The supplier's name has successfully been changed, you will now be taken back to the supplier page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                SupplierPage de = new SupplierPage(company);
                this.Close();
                de.Show();
            }
        }

        private bool VerifyName(string name)
        {
            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Name cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NameTextBox.Text = string.Empty;
                return false;
            }
            if(name.Length > 45)
            {
                MessageBox.Show("Name is too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NameTextBox.Text = string.Empty;
                return false;
            }

            if (Regex.IsMatch(name, @"^[a-zA-Z0-9 ]+$") == false)
            {
                MessageBox.Show("Name can only contain letters and numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NameTextBox.Text = string.Empty;
                return false;
            }

            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM " + comNameNoSpaces + "Suppliers";
            cmd.CommandText = insertquery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Name"].ToString() == name)
                {
                    MessageBox.Show("There is already a supplier with this name, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    NameTextBox.Text = string.Empty;
                    return false;
                }
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Number_Button_Click(object sender, RoutedEventArgs e)
        {
            if (NumberText.Visibility == Visibility.Hidden && NumberTextBox.Visibility == Visibility.Hidden && NumberButton.Visibility == Visibility.Hidden)
            {
                NumberText.Visibility = Visibility.Visible;
                NumberTextBox.Visibility = Visibility.Visible;
                NumberButton.Visibility = Visibility.Visible;
            }
            else
            {
                NumberText.Visibility = Visibility.Hidden;
                NumberTextBox.Visibility = Visibility.Hidden;
                NumberButton.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_Number_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyNumber(NumberTextBox.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + comNameNoSpaces + "Suppliers SET ProductsSupplied = " + NumberTextBox.Text + " WHERE Name = '" + supplier + "'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The supplier's number of supplied products has successfully been changed, you will now be taken back to the supplier page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                SupplierPage de = new SupplierPage(company);
                this.Close();
                de.Show();
            }
        }

        private bool VerifyNumber(string number)
        {
            if (String.IsNullOrEmpty(NumberTextBox.Text))
            {
                MessageBox.Show("Number cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NumberTextBox.Text = string.Empty;
                return false;
            }
            int num = 0;
            try
            {
                num = Int32.Parse(NumberTextBox.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("The number must be an integer, not a decimal or letters or symbols", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NumberTextBox.Text = string.Empty;
                return false;
            }
            if (num < 0)
            {
                MessageBox.Show("The number must be a positive integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NumberTextBox.Text = string.Empty;
                return false;
            }
            return true;
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"DELETE FROM " + comNameNoSpaces + "Suppliers WHERE Name = '" + supplier + "'";
            cmd.CommandText = insertquery;
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("This supplier has successfully been removed, you will now be taken back to the supplier page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            SupplierPage de = new SupplierPage(company);
            this.Close();
            de.Show();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            SupplierPage de = new SupplierPage(company);
            this.Close();
            de.Show();
        }


    }
}
