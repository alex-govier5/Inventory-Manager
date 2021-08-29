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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        string product;
        string department;
        string company;
        string supplier;
        string quantity;
        public EditProduct(string product, string supplier, string department, string company, string quantity)
        {
            InitializeComponent();
            this.product = product;
            this.department = department;
            this.company = company;
            this.supplier = supplier;
            this.quantity = quantity;
            Plus.Items.Add("+");
            Plus.Items.Add("-");
            int quan = Int32.Parse(quantity);
            if(quan < 10)
            {
                MessageBox.Show("This product has a quantity of less than 10, it is perhaps time for a restock!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            ProductName.Text = product;
            IDText.Visibility = Visibility.Hidden;
            IDTextBox.Visibility = Visibility.Hidden;
            IDButton.Visibility = Visibility.Hidden;
            PPIText.Visibility = Visibility.Hidden;
            PPITextBox.Visibility = Visibility.Hidden;
            PPIButton.Visibility = Visibility.Hidden;
            NameText.Visibility = Visibility.Hidden;
            NameTextBox.Visibility = Visibility.Hidden;
            NameButton.Visibility = Visibility.Hidden;
            DepartmentText.Visibility = Visibility.Hidden;
            DepartmentComboBox.Visibility = Visibility.Hidden;
            DepartmentButton.Visibility = Visibility.Hidden;
            QuantityText.Visibility = Visibility.Hidden;
            QuantityTextBox.Visibility = Visibility.Hidden;
            QuantityButton.Visibility = Visibility.Hidden;
            Plus.Visibility = Visibility.Hidden;
            SupplierText.Visibility = Visibility.Hidden;
            SupplierComboBox.Visibility = Visibility.Hidden;
            SupplierButton.Visibility = Visibility.Hidden;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM "+companyNameNoSpaces+"Departments";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader["Name"].ToString() != department)
                {
                    DepartmentComboBox.Items.Add(reader["Name"].ToString());
                }

                
            }
            reader.Close();
            string insertquer2 = @"SELECT * FROM "+companyNameNoSpaces+"Suppliers";
            cmd.CommandText = insertquer2;
            MySqlDataReader reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {
                if(reader2["Name"].ToString() != supplier)
                {
                    SupplierComboBox.Items.Add(reader2["Name"].ToString());
                }
            }
            reader2.Close();
            mySqlConnection.Close();
        }

        private void ID_Button_Click(object sender, RoutedEventArgs e)
        {
            if(IDText.Visibility == Visibility.Hidden && IDTextBox.Visibility == Visibility.Hidden && IDButton.Visibility == Visibility.Hidden)
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
                string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE "+depNameNoSpaces+"Inventory SET ID = "+IDTextBox.Text+" WHERE ProductName = '"+product+"'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The product's ID has successfully been changed, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                DepartmentInventory de = new DepartmentInventory(department, company);
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
            if(id < 0)
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
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM " + depNameNoSpaces + "Inventory";
            cmd.CommandText = insertquery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader["ID"].ToString() == ID)
                {
                    MessageBox.Show("There is already a product with this ID in this inventory, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + depNameNoSpaces + "Inventory SET ProductName = '" + NameTextBox.Text + "' WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The product's name has successfully been changed, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                DepartmentInventory de = new DepartmentInventory(department, company);
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
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM " + depNameNoSpaces + "Inventory";
            cmd.CommandText = insertquery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["ProductName"].ToString() == name)
                {
                    MessageBox.Show("There is already a product with this name in this inventory, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    NameTextBox.Text = string.Empty;
                    return false;
                }
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Quantity_Button_Click(object sender, RoutedEventArgs e)
        {
            if (QuantityText.Visibility == Visibility.Hidden && QuantityTextBox.Visibility == Visibility.Hidden && QuantityButton.Visibility == Visibility.Hidden && Plus.Visibility == Visibility.Hidden)
            {
                QuantityText.Visibility = Visibility.Visible;
                QuantityTextBox.Visibility = Visibility.Visible;
                QuantityButton.Visibility = Visibility.Visible;
                Plus.Visibility = Visibility.Visible;
            }
            else
            {
                QuantityText.Visibility = Visibility.Hidden;
                QuantityTextBox.Visibility = Visibility.Hidden;
                QuantityButton.Visibility = Visibility.Hidden;
                Plus.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_Quantity_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyQuantity(QuantityTextBox.Text, Plus.Text, product))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + depNameNoSpaces + "Inventory SET Quantity = Quantity "+Plus.Text+"" + QuantityTextBox.Text + " WHERE ProductName = '" + product + "'";

                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                string insertquery2 = @"SELECT * FROM " + depNameNoSpaces + "Inventory WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery2;
                MySqlDataReader re = cmd.ExecuteReader();
                double price = 0;
                int quantity = 0;
                string supplier = "";
                while (re.Read())
                {
                    price = Double.Parse(re["Price"].ToString());
                    quantity = Int32.Parse(re["Quantity"].ToString());
                    supplier = re["Supplier"].ToString();
                }
                re.Close();
                double tot = quantity * price;
                string insertquery3 = @"UPDATE " + depNameNoSpaces + "Inventory SET Total = " + tot.ToString() + " WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery3;
                cmd.ExecuteNonQuery();
                string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
                DateTime now = DateTime.Now;
                double newTotal = Int32.Parse(QuantityTextBox.Text) * price;
                if (Plus.Text == "+")
                {
                    string query0 = @"INSERT INTO "+comNameNoSpaces+"Purchases VALUES (DEFAULT, "+now.Hour+", "+now.Minute+", "+now.Day+", "+now.Month+", "+now.Year+", '"+product+"', '"+supplier+"', "+QuantityTextBox.Text+", "+price.ToString()+", "+newTotal.ToString()+")";
                    cmd.CommandText = query0;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string query0 = @"INSERT INTO " + comNameNoSpaces + "Sales VALUES (DEFAULT, " + now.Hour + ", " + now.Minute + ", " + now.Day + ", " + now.Month + ", " + now.Year + ", '" + product + "', '" + supplier + "', " + QuantityTextBox.Text + ", " + price.ToString() + ", " + newTotal + ")";
                    cmd.CommandText = query0;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The product's quantity has successfully been changed, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                DepartmentInventory de = new DepartmentInventory(department, company);
                this.Close();
                de.Show();
            }
        }

        private bool VerifyQuantity(string quantity, string plus, string prod)
        {
            if (String.IsNullOrEmpty(QuantityTextBox.Text) || String.IsNullOrEmpty(plus))
            {
                MessageBox.Show("Quantity and operation cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                QuantityTextBox.Text = string.Empty;
                return false;
            }

            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM " + depNameNoSpaces + "Inventory WHERE ProductName = '"+prod+"'";
            cmd.CommandText = insertquery;
            MySqlDataReader reader = cmd.ExecuteReader();
            int currentQuan = 0;
            while (reader.Read())
            {
                currentQuan = Int32.Parse(reader["Quantity"].ToString());
            }

            if(plus == "-" && Int32.Parse(quantity) > currentQuan)
            {
                MessageBox.Show("Cannot remove more quantity than the current amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                QuantityTextBox.Text = string.Empty;
                return false;
            }
            reader.Close();
            string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string query = @"SELECT * FROM "+comNameNoSpaces+"Purchases";
            cmd.CommandText = query;
            MySqlDataReader reader2 = cmd.ExecuteReader();
            int count = 0;
            int lowest = 0;
            while (reader2.Read())
            {
                if(count == 0)
                {
                    lowest = Int32.Parse(reader2["Transaction"].ToString());
                }
                else
                {
                    if(Int32.Parse(reader2["Transaction"].ToString()) < lowest)
                    {
                        lowest = Int32.Parse(reader2["Transaction"].ToString());
                    }
                }
                count++;
            }

            if(count >= 20)
            {
                string query0 = @"DELETE FROM "+comNameNoSpaces+"Purchases WHERE Transaction = "+lowest.ToString();
                cmd.CommandText = query0;
                cmd.ExecuteNonQuery();
            }

            reader2.Close();

            string query10 = @"SELECT * FROM " + comNameNoSpaces + "Sales";
            cmd.CommandText = query10;
            MySqlDataReader reader3 = cmd.ExecuteReader();
            int count1 = 0;
            int lowest1 = 0;
            while (reader3.Read())
            {
                if (count1 == 0)
                {
                    lowest1 = Int32.Parse(reader3["Transaction"].ToString());
                }
                else
                {
                    if (Int32.Parse(reader3["Transaction"].ToString()) < lowest1)
                    {
                        lowest1 = Int32.Parse(reader3["Transaction"].ToString());
                    }
                }
                count1++;
            }

            if (count1 >= 20)
            {
                string query0 = @"DELETE FROM " + comNameNoSpaces + "Sales WHERE Transaction = " + lowest1.ToString();
                cmd.CommandText = query0;
                cmd.ExecuteNonQuery();
            }



            reader2.Close();

            int quan = 0;
            try
            {
                quan = Int32.Parse(QuantityTextBox.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("Quantity must be an integer, not a decimal or letters or symbols", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                QuantityTextBox.Text = string.Empty;
                return false;
            }
            if (quan < 0)
            {
                MessageBox.Show("Quantity must be a positive integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                QuantityTextBox.Text = string.Empty;
                return false;
            }
            mySqlConnection.Close();
            return true;
        }

        private void PPI_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PPIText.Visibility == Visibility.Hidden && PPITextBox.Visibility == Visibility.Hidden && PPIButton.Visibility == Visibility.Hidden)
            {
                PPIText.Visibility = Visibility.Visible;
                PPITextBox.Visibility = Visibility.Visible;
                PPIButton.Visibility = Visibility.Visible;
            }
            else
            {
                PPIText.Visibility = Visibility.Hidden;
                PPITextBox.Visibility = Visibility.Hidden;
                PPIButton.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_PPI_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyPPI(PPITextBox.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + depNameNoSpaces + "Inventory SET Price = " + PPITextBox.Text + " WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                string insertquery2 = @"SELECT Quantity FROM " + depNameNoSpaces + "Inventory WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery2;
                MySqlDataReader re = cmd.ExecuteReader();
                int quan = 0;
                while (re.Read())
                {
                    quan = Int32.Parse(re["Quantity"].ToString());
                }
                re.Close();
                double dub = Double.Parse(PPITextBox.Text);
                double tot = dub * quan;
                string insertquery3 = @"UPDATE " + depNameNoSpaces + "Inventory SET Total = " + tot.ToString() + " WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery3;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The product's price per item has successfully been changed, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                DepartmentInventory de = new DepartmentInventory(department, company);
                this.Close();
                de.Show();
            }
        }

        private bool VerifyPPI(string PPI)
        {
            if (String.IsNullOrEmpty(PPITextBox.Text))
            {
                MessageBox.Show("Price per item cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PPITextBox.Text = string.Empty;
                return false;
            }
            double ppi = 0;
            try
            {
                ppi = Double.Parse(PPITextBox.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("Price per item must be a decimal value, not letters or symbols", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PPITextBox.Text = string.Empty;
                return false;
            }
            if (ppi < 0)
            {
                MessageBox.Show("Price per item must be a positive decimal", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PPITextBox.Text = string.Empty;
                return false;
            }
            return true;
        }

        private void Department_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentText.Visibility == Visibility.Hidden && DepartmentComboBox.Visibility == Visibility.Hidden && DepartmentButton.Visibility == Visibility.Hidden)
            {
                DepartmentText.Visibility = Visibility.Visible;
                DepartmentComboBox.Visibility = Visibility.Visible;
                DepartmentButton.Visibility = Visibility.Visible;
            }
            else
            {
                DepartmentText.Visibility = Visibility.Hidden;
                DepartmentComboBox.Visibility = Visibility.Hidden;
                DepartmentButton.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_Department_Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string dep2NameNoSpaces = String.Concat(DepartmentComboBox.Text.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery1 = @"SELECT * FROM " + depNameNoSpaces + "Inventory WHERE ProductName = '" + product + "'";
            cmd.CommandText = insertquery1;
            MySqlDataReader reader = cmd.ExecuteReader();
            string id = "";
            string name = "";
            string department2 = DepartmentComboBox.Text;
            string quantity = "";
            string ppi = "";
            string total = "";
            while (reader.Read())
            {
                id = reader["ID"].ToString();
                name = reader["ProductName"].ToString();
                quantity = reader["Quantity"].ToString();
                ppi = reader["Price"].ToString();
                total = reader["Total"].ToString();
            }
            reader.Close();
            if (VerifyDepartment(id, name, DepartmentComboBox.Text))
            {
                
                string insertquery0 = @"INSERT INTO " + dep2NameNoSpaces + "Inventory VALUES ("+id+", '"+name+"', '"+department2+"', "+quantity+", "+ppi+", "+total+")";
                cmd.CommandText = insertquery0;
                cmd.ExecuteNonQuery();
                string insertquery = @"DELETE FROM "+depNameNoSpaces+"Inventory WHERE ProductName = '"+product+"'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();
                MessageBox.Show("This product has successfully been removed from this department and moved to the new department, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                DepartmentInventory de = new DepartmentInventory(department, company);
                this.Close();
                de.Show();
            }
        }

        private bool VerifyDepartment(string id, string name, string department)
        {
            if (String.IsNullOrEmpty(DepartmentComboBox.Text))
            {
                MessageBox.Show("Department cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DepartmentComboBox.Text = string.Empty;
                return false;
            }

            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string dep2NameNoSpaces = String.Concat(DepartmentComboBox.Text.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM "+dep2NameNoSpaces+"Inventory";
            cmd.CommandText = insertquery;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader["ID"].ToString() == id)
                {
                    MessageBox.Show("There is already a product in this inventory with that ID, please change the ID and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (reader["ProductName"].ToString() == name)
                {
                    MessageBox.Show("There is already a product in this inventory with that name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            reader.Close();
            mySqlConnection.Close();
            return true;
        }

        private void Supplier_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierText.Visibility == Visibility.Hidden && SupplierComboBox.Visibility == Visibility.Hidden && SupplierButton.Visibility == Visibility.Hidden)
            {
                SupplierText.Visibility = Visibility.Visible;
                SupplierComboBox.Visibility = Visibility.Visible;
                SupplierButton.Visibility = Visibility.Visible;
            }
            else
            {
                SupplierText.Visibility = Visibility.Hidden;
                SupplierComboBox.Visibility = Visibility.Hidden;
                SupplierButton.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_Supplier_Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifySupplier(SupplierComboBox.Text))
            {
                string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySqlConnection;
                string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
                string insertquery = @"UPDATE " + depNameNoSpaces + "Inventory SET Supplier = '" + SupplierComboBox.Text + "' WHERE ProductName = '" + product + "'";
                cmd.CommandText = insertquery;
                cmd.ExecuteNonQuery();
                MessageBox.Show("The product's supplier has successfully been changed, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                mySqlConnection.Close();
                DepartmentInventory de = new DepartmentInventory(department, company);
                this.Close();
                de.Show();
            }

        }

        private bool VerifySupplier(string supplier)
        {
            if (String.IsNullOrEmpty(SupplierComboBox.Text))
            {
                MessageBox.Show("Supplier cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierComboBox.Text = string.Empty;
                return false;
            }
            if(supplier.Length > 45)
            {
                MessageBox.Show("Supplier name is too long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SupplierComboBox.Text = string.Empty;
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
            string depNameNoSpaces = String.Concat(department.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"DELETE FROM " + depNameNoSpaces + "Inventory WHERE ProductName = '" + product + "'";
            cmd.CommandText = insertquery;
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("This product has successfully been removed from this inventory, you will now be taken back to the inventory page", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            DepartmentInventory de = new DepartmentInventory(department, company);
            this.Close();
            de.Show();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            DepartmentInventory dep = new DepartmentInventory(department, company);
            this.Close();
            dep.Show();
        }

    }
}
