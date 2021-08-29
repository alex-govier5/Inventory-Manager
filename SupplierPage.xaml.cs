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
    /// Interaction logic for SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : Window
    {
        string company;
        public SupplierPage(string company)
        {
            InitializeComponent();
            this.company = company;
            Title.Text = company;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM " + companyNameNoSpaces + "Suppliers ORDER BY ID ASC";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            TextBlock block2 = new TextBlock();
            block2.FontSize = 15;
            block2.Height = 25;
            block2.Width = 200;
            block2.FontFamily = new FontFamily("Rockwell");
            block2.TextAlignment = TextAlignment.Center;
            block2.Background = Brushes.Black;
            block2.Foreground = Brushes.MintCream;
            block2.Text = "Supplier ID:";
            Thickness margin = new Thickness(10, 10, 10, 10);
            block2.Margin = margin;
            ID.Children.Add(block2);
            while (reader.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.Height = 25;
                block.Width = 200;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader["ID"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                ID.Children.Add(block);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                ID.Children.Add(text1);
            }
            reader.Close();

            MySqlDataReader reader2 = cmd.ExecuteReader();
            TextBlock block3 = new TextBlock();
            block3.FontSize = 15;
            block3.Height = 25;
            block3.Width = 200;
            block3.FontFamily = new FontFamily("Rockwell");
            block3.TextAlignment = TextAlignment.Center;
            block3.Background = Brushes.Black;
            block3.Foreground = Brushes.MintCream;
            block3.Text = "Supplier Name:";
            block3.Margin = margin;
            Name.Children.Add(block3);
            while (reader2.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader2["Name"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Name.Children.Add(block);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Name.Children.Add(text1);
            }
            reader2.Close();

            MySqlDataReader reader3 = cmd.ExecuteReader();
            TextBlock block4 = new TextBlock();
            block4.FontSize = 15;
            block4.FontFamily = new FontFamily("Rockwell");
            block4.TextAlignment = TextAlignment.Center;
            block4.Background = Brushes.Black;
            block4.Foreground = Brushes.MintCream;
            block4.Text = "Products Supplied:";
            block4.Margin = margin;
            block4.Height = 25;
            block4.Width = 200;
            Number.Children.Add(block4);
            while (reader3.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader3["ProductsSupplied"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Number.Children.Add(block);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Number.Children.Add(text1);
            }
            reader3.Close();

            MySqlDataReader reader7 = cmd.ExecuteReader();
            TextBlock block8 = new TextBlock();
            block8.FontSize = 15;
            block8.FontFamily = new FontFamily("Rockwell");
            block8.TextAlignment = TextAlignment.Center;
            block8.Background = Brushes.Black;
            block8.Foreground = Brushes.MintCream;
            block8.Text = "Edit Supplier:";
            Thickness thick = new Thickness(10, 10, 10, 10);
            block8.Margin = thick;
            block8.Height = 25;
            block8.Width = 200;
            Edit.Children.Add(block8);
            while (reader7.Read())
            {
                Button button = new Button();
                button.Width = 180;
                button.Height = 25;
                button.FontFamily = new FontFamily("Rockwell");
                button.FontSize = 15;
                button.Background = Brushes.MintCream;
                button.Foreground = Brushes.Black;
                button.Content = "Edit";
                button.Tag = reader7["Name"].ToString();
                Thickness margin2 = new Thickness(5, 7, 5, 5);
                button.Margin = margin;
                button.Click += Edit_Button_Click;
                Edit.Children.Add(button);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Edit.Children.Add(text1);
            }
            reader7.Close();

            mySqlConnection.Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            AddSupplier ad = new AddSupplier(company);
            this.Close();
            ad.Show();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            InventoryPage ip = new InventoryPage(company);
            this.Close();
            ip.Show();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string name = (string)b.Tag;
            EditSupplier ed = new EditSupplier(company, name);
            this.Close();
            ed.Show();
        }
    }
}
