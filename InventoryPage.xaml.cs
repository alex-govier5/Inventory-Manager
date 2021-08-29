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
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Window
    {
        string companyName;
        public InventoryPage(string company)
        {
            InitializeComponent();
            this.companyName = company;
            Title.Text = companyName;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM "+companyNameNoSpaces+"Departments ORDER BY Name ASC";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
                if(count == 1)
                {
                    TextBlock block = new TextBlock();
                    block.FontSize = 15;
                    block.FontFamily = new FontFamily("Rockwell");
                    block.TextAlignment = TextAlignment.Center;
                    block.Background = Brushes.Black;
                    block.Foreground = Brushes.MintCream;
                    block.Text = reader["Name"].ToString();
                    Thickness margin = new Thickness(10, 10, 10, 10);
                    block.Margin = margin;

                    Departments1.Children.Add(block);

                    Button button = new Button();
                    string dep = reader["Name"].ToString();
                    string depNoSpace = String.Concat(dep.Where(c => !Char.IsWhiteSpace(c)));
                    button.Name = depNoSpace;
                    button.Width = 160;
                    button.Height = 30;
                    button.FontFamily = new FontFamily("Rockwell");
                    button.Tag = dep;
                    button.FontSize = 15;
                    button.Background = Brushes.MintCream;
                    button.Foreground = Brushes.Black;
                    button.Content = "Click here to view";
                    button.Click += DepartmentInventory_Button_Click;
                    Thickness margin2 = new Thickness(10, 10, 10, 10);
                    button.Margin = margin2;

                    Button button2 = new Button();

                    button2.Name = depNoSpace;
                    button2.Width = 160;
                    button2.Height = 30;
                    button2.FontFamily = new FontFamily("Rockwell");
                    button2.FontSize = 15;
                    button2.Background = Brushes.MintCream;
                    button2.Foreground = Brushes.Black;
                    button2.Content = "Remove Department";
                    button2.Tag = dep;
                    button2.Click += Remove_Button_Click;
                    Thickness marg = new Thickness(10, 10, 10, 10);
                    button2.Margin = marg;

                    Departments1.Children.Add(button);
                    Departments1.Children.Add(button2);
                }
                else if(count == 2)
                {
                    TextBlock block = new TextBlock();
                    block.FontSize = 15;
                    block.FontFamily = new FontFamily("Rockwell");
                    block.TextAlignment = TextAlignment.Center;
                    block.Background = Brushes.Black;
                    block.Foreground = Brushes.MintCream;
                    block.Text = reader["Name"].ToString();
                    Thickness margin = new Thickness(10, 10, 10, 10);
                    block.Margin = margin;

                    Departments2.Children.Add(block);

                    Button button = new Button();
                    string dep = reader["Name"].ToString();
                    string depNoSpace = String.Concat(dep.Where(c => !Char.IsWhiteSpace(c)));
                    button.Name = depNoSpace;
                    button.Width = 160;
                    button.Height = 30;
                    button.FontFamily = new FontFamily("Rockwell");
                    button.Tag = dep;
                    button.FontSize = 15;
                    button.Background = Brushes.MintCream;
                    button.Foreground = Brushes.Black;
                    button.Content = "Click here to view";
                    button.Click += DepartmentInventory_Button_Click;
                    Thickness margin2 = new Thickness(10, 10, 10, 10);
                    button.Margin = margin2;

                    Button button2 = new Button();

                    button2.Name = depNoSpace;
                    button2.Width = 160;
                    button2.Height = 30;
                    button2.FontFamily = new FontFamily("Rockwell");
                    button2.FontSize = 15;
                    button2.Background = Brushes.MintCream;
                    button2.Foreground = Brushes.Black;
                    button2.Content = "Remove Department";
                    button2.Tag = dep;
                    button2.Click += Remove_Button_Click;
                    Thickness marg = new Thickness(10, 10, 10, 10);
                    button2.Margin = marg;

                    Departments2.Children.Add(button);
                    Departments2.Children.Add(button2);
                    
                }
                else if(count == 3)
                {
                    TextBlock block = new TextBlock();
                    block.FontSize = 15;
                    block.FontFamily = new FontFamily("Rockwell");
                    block.TextAlignment = TextAlignment.Center;
                    block.Background = Brushes.Black;
                    block.Foreground = Brushes.MintCream;
                    block.Text = reader["Name"].ToString();
                    Thickness margin = new Thickness(10, 10, 10, 10);
                    block.Margin = margin;

                    Departments3.Children.Add(block);

                    Button button = new Button();
                    string dep = reader["Name"].ToString();
                    string depNoSpace = String.Concat(dep.Where(c => !Char.IsWhiteSpace(c)));
                    button.Name = depNoSpace;
                    button.Width = 160;
                    button.Height = 30;
                    button.FontFamily = new FontFamily("Rockwell");
                    button.Tag = dep;
                    button.FontSize = 15;
                    button.Background = Brushes.MintCream;
                    button.Foreground = Brushes.Black;
                    button.Content = "Click here to view";
                    button.Click += DepartmentInventory_Button_Click;
                    Thickness margin2 = new Thickness(10, 10, 10, 10);
                    button.Margin = margin2;

                    Button button2 = new Button();

                    button2.Name = depNoSpace;
                    button2.Width = 160;
                    button2.Height = 30;
                    button2.FontFamily = new FontFamily("Rockwell");
                    button2.FontSize = 15;
                    button2.Background = Brushes.MintCream;
                    button2.Foreground = Brushes.Black;
                    button2.Content = "Remove Department";
                    button2.Tag = dep;
                    button2.Click += Remove_Button_Click;
                    Thickness marg = new Thickness(10, 10, 10, 10);
                    button2.Margin = marg;

                    Departments3.Children.Add(button);
                    Departments3.Children.Add(button2);
                }
                else
                {
                    count = 1;
                    TextBlock block = new TextBlock();
                    block.FontSize = 15;
                    block.FontFamily = new FontFamily("Rockwell");
                    block.TextAlignment = TextAlignment.Center;
                    block.Background = Brushes.Black;
                    block.Foreground = Brushes.MintCream;
                    block.Text = reader["Name"].ToString();
                    Thickness margin = new Thickness(10, 10, 10, 10);
                    block.Margin = margin;

                    Departments1.Children.Add(block);

                    Button button = new Button();
                    string dep = reader["Name"].ToString();
                    string depNoSpace = String.Concat(dep.Where(c => !Char.IsWhiteSpace(c)));
                    button.Name = depNoSpace;
                    button.Width = 160;
                    button.Height = 30;
                    button.FontFamily = new FontFamily("Rockwell");
                    button.Tag = dep;
                    button.FontSize = 15;
                    button.Background = Brushes.MintCream;
                    button.Foreground = Brushes.Black;
                    button.Content = "Click here to view";
                    button.Click += DepartmentInventory_Button_Click;
                    Thickness margin2 = new Thickness(10, 10, 10, 10);
                    button.Margin = margin2;

                    Button button2 = new Button();

                    button2.Name = depNoSpace;
                    button2.Width = 160;
                    button2.Height = 30;
                    button2.FontFamily = new FontFamily("Rockwell");
                    button2.FontSize = 15;
                    button2.Background = Brushes.MintCream;
                    button2.Foreground = Brushes.Black;
                    button2.Content = "Remove Department";
                    button2.Tag = dep;
                    button2.Click += Remove_Button_Click;
                    Thickness marg = new Thickness(10, 10, 10, 10);
                    button2.Margin = marg;

                    Departments1.Children.Add(button);
                    Departments1.Children.Add(button2);
                }
                
            }
            reader.Close();
            mySqlConnection.Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment department = new AddDepartment(companyName);
            this.Close();
            department.Show();
        }

        private void DepartmentInventory_Button_Click(object sender, RoutedEventArgs e)
        {
            Button butt = (Button)sender;
            string st = (string)butt.Tag;
            DepartmentInventory inv = new DepartmentInventory(st,companyName);
            this.Close();
            inv.Show();
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            Button butt = (Button)sender;
            string rem = (string)butt.Tag;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(companyName.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"DELETE FROM "+companyNameNoSpaces+"Departments WHERE Name = '"+rem+"'";
            cmd.CommandText = insertQuery;
            cmd.ExecuteNonQuery();
            InventoryPage page = new InventoryPage(companyName);
            this.Close();
            page.Show();
            mySqlConnection.Close();
            
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            SupplierPage sp = new SupplierPage(companyName);
            this.Close();
            sp.Show();
        }

        private void Statements_Click(object sender, RoutedEventArgs e)
        {
            Statements st = new Statements(companyName);
            this.Close();
            st.Show();
        }
    }
}
