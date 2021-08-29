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
    /// Interaction logic for SinglePurchase.xaml
    /// </summary>
    public partial class SinglePurchase : Window
    {
        string company;
        public SinglePurchase(string company)
        {
            InitializeComponent();
            this.company = company;
            InitializeComponent();
            this.company = company;
            Title.Text = company;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string comNameNoSpaces = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertquery = @"SELECT * FROM " + comNameNoSpaces + "Purchases";
            cmd.CommandText = insertquery;
            MySqlDataReader re = cmd.ExecuteReader();
            double final = 0;
            while (re.Read())
            {
                TextBlock block = new TextBlock();
                block.Foreground = Brushes.Black;
                block.FontFamily = new FontFamily("Times New Roman");
                block.FontSize = 15;
                block.TextAlignment = TextAlignment.Center;
                Thickness thick = new Thickness(10, 10, 10, 10);
                block.Margin = thick;
                block.Text = re["Transaction"].ToString();
                ID.Children.Add(block);

                TextBlock block2 = new TextBlock();
                block2.Foreground = Brushes.Black;
                block2.FontFamily = new FontFamily("Times New Roman");
                block2.FontSize = 15;
                block2.TextAlignment = TextAlignment.Center;
                block2.Margin = thick;
                block2.Text = re["Day"].ToString() + "/" + re["Month"].ToString() + "/" + re["Year"].ToString() + "";
                Date.Children.Add(block2);

                TextBlock block3 = new TextBlock();
                block3.Foreground = Brushes.Black;
                block3.FontFamily = new FontFamily("Times New Roman");
                block3.FontSize = 15;
                block3.TextAlignment = TextAlignment.Center;
                block3.Margin = thick;
                block3.Text = re["Hour"].ToString() + ":" + re["Minute"].ToString() + "";
                Time.Children.Add(block3);

                TextBlock block9 = new TextBlock();
                block9.Foreground = Brushes.Black;
                block9.FontFamily = new FontFamily("Times New Roman");
                block9.FontSize = 15;
                block9.TextAlignment = TextAlignment.Center;
                block9.Margin = thick;
                block9.Text = re["Product"].ToString();
                Product.Children.Add(block9);

                TextBlock block4 = new TextBlock();
                block4.Foreground = Brushes.Black;
                block4.FontFamily = new FontFamily("Times New Roman");
                block4.FontSize = 15;
                block4.TextAlignment = TextAlignment.Center;
                block4.Margin = thick;
                block4.Text = re["Supplier"].ToString();
                Supplier.Children.Add(block4);

                TextBlock block5 = new TextBlock();
                block5.Foreground = Brushes.Black;
                block5.FontFamily = new FontFamily("Times New Roman");
                block5.FontSize = 15;
                block5.TextAlignment = TextAlignment.Center;
                block5.Margin = thick;
                block5.Text = re["Quantity"].ToString();
                Quantity.Children.Add(block5);

                TextBlock block6 = new TextBlock();
                block6.Foreground = Brushes.Black;
                block6.FontFamily = new FontFamily("Times New Roman");
                block6.FontSize = 15;
                block6.TextAlignment = TextAlignment.Center;
                block6.Margin = thick;
                block6.Text = "$" + re["PPI"].ToString();
                PPI.Children.Add(block6);

                TextBlock block7 = new TextBlock();
                block7.Foreground = Brushes.Black;
                block7.FontFamily = new FontFamily("Times New Roman");
                block7.FontSize = 15;
                block7.TextAlignment = TextAlignment.Center;
                block7.Margin = thick;
                block7.Text = "$" + re["Total"].ToString();
                Total.Children.Add(block7);

                final += Double.Parse(re["Total"].ToString());

            }
            Final.Text = "$" + final.ToString();
            re.Close();
            mySqlConnection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Statements st = new Statements(company);
            this.Close();
            st.Show();
        }
    }
}
