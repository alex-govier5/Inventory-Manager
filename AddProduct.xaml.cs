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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        string department;
        string company;
        public AddProduct(string department, string company)
        {
            InitializeComponent();
            this.department = department;
            this.company = company;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM " + comNameNoSpaces + "Suppliers";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Supplier.Items.Add(reader["Name"].ToString());
            }
            reader.Close();
            mySqlConnection.Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyInfo(ProductName.Text, ProductQuantity.Text, PricePerItem.Text, ProductID.Text, Supplier.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
                double ppi = Double.Parse(PricePerItem.Text);
                double qua = Double.Parse(ProductQuantity.Text);
                double total = ppi * qua;
                string tot = total.ToString();
                string insertQuery = @"INSERT INTO "+depNameNoSpaces+"Inventory VALUES ("+ProductID.Text+", '"+ProductName.Text+"', '"+department+"', "+ProductQuantity.Text+", "+PricePerItem.Text+", "+tot+", '"+Supplier.Text+"')";
                cmd.CommandText = insertQuery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your item has been added to the inventory, you will now be taken back to the inventory page", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                DepartmentInventory depo = new DepartmentInventory(department, company);
                this.Close();
                depo.Show();
            }
        }

        private bool VerifyInfo(string name, string quantity, string price, string id, string supplier)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(supplier))
            {
                MessageBox.Show("One of the required fields was empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductName.Text = string.Empty;
                ProductQuantity.Text = string.Empty;
                PricePerItem.Text = string.Empty;
                Supplier.Text = string.Empty;
                return false;
            }

            if(name.Length > 45 || supplier.Length > 45)
            {
                MessageBox.Show("One of the required fields was too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductName.Text = string.Empty;
                Supplier.Text = string.Empty;
                return false;
            }

            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM "+depNameNoSpaces+"Inventory";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader["ProductName"].ToString() == name)
                {
                    MessageBox.Show("There is already a product with this name in this inventory, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ProductName.Text = string.Empty;
                    return false;
                }
                if(reader["ID"].ToString() == id)
                {
                    MessageBox.Show("There is already a product with this ID in this inventory, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ProductID.Text = string.Empty;
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
                MessageBox.Show("The quantity must be an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductQuantity.Text = string.Empty;
                return false;
            }
            if(quan < 0)
            {
                MessageBox.Show("The quantity must be a positive number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductQuantity.Text = string.Empty;
                return false;
            }

            double pric = 0;
            try
            {
                pric = Double.Parse(price);
            }
            catch (FormatException e)
            {
                MessageBox.Show("The price must be a decimal value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PricePerItem.Text = string.Empty;
                return false;
            }
            if (pric < 0)
            {
                MessageBox.Show("The price must be a positive number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PricePerItem.Text = string.Empty;
                return false;
            }

            int id2 = 0;
            try
            {
                id2 = Int32.Parse(ProductID.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("The product ID must be an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductID.Text = string.Empty;
                return false;
            }
            if (id2 < 0)
            {
                MessageBox.Show("The price must be a positive number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductID.Text = string.Empty;
                return false;
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            DepartmentInventory dep = new DepartmentInventory(department, company);
            this.Close();
            dep.Show();
        }


    }
}
