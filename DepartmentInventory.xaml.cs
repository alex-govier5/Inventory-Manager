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
    /// Interaction logic for DepartmentInventory.xaml
    /// </summary>
    public partial class DepartmentInventory : Window
    {
        string departmentName;
        string company;
        public DepartmentInventory(string departmentName, string company)
        {
            InitializeComponent();
            this.departmentName = departmentName;
            this.company = company;
            Title.Text = departmentName;
            Title2.Text = "Inventory";
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string depNoSpace = String.Concat(departmentName.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM "+depNoSpace+"Inventory ORDER BY ID ASC";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            TextBlock block2 = new TextBlock();
            block2.FontSize = 15;
            block2.FontFamily = new FontFamily("Rockwell");
            block2.TextAlignment = TextAlignment.Center;
            block2.Background = Brushes.Black;
            block2.Foreground = Brushes.MintCream;
            block2.Text = "Product ID:";
            Thickness margin = new Thickness(10, 10, 10, 10);
            block2.Margin = margin;
            block2.Height = 25;
            block2.Width = 200;
            ID.Children.Add(block2);
            while (reader.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader["ID"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
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
            block3.FontFamily = new FontFamily("Rockwell");
            block3.TextAlignment = TextAlignment.Center;
            block3.Background = Brushes.Black;
            block3.Foreground = Brushes.MintCream;
            block3.Text = "Product Name:";
            block3.Margin = margin;
            block3.Height = 25;
            block3.Width = 200;
            ProductName.Children.Add(block3);
            int prodCount = 0;
            while (reader2.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader2["ProductName"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                ProductName.Children.Add(block);
                prodCount++;

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                ProductName.Children.Add(text1);
            }
            TotalProduct.Text = prodCount.ToString();
            reader2.Close();

            MySqlDataReader reader3 = cmd.ExecuteReader();
            TextBlock block4 = new TextBlock();
            block4.FontSize = 15;
            block4.FontFamily = new FontFamily("Rockwell");
            block4.TextAlignment = TextAlignment.Center;
            block4.Background = Brushes.Black;
            block4.Foreground = Brushes.MintCream;
            block4.Text = "Department:";
            block4.Margin = margin;
            block4.Height = 25;
            block4.Width = 200;
            Department.Children.Add(block4);
            while (reader3.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader3["Department"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Department.Children.Add(block);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Department.Children.Add(text1);
            }
            reader3.Close();

            MySqlDataReader reader4 = cmd.ExecuteReader();
            TextBlock block5 = new TextBlock();
            block5.FontSize = 15;
            block5.FontFamily = new FontFamily("Rockwell");
            block5.TextAlignment = TextAlignment.Center;
            block5.Background = Brushes.Black;
            block5.Foreground = Brushes.MintCream;
            block5.Text = "Quantity:";
            block5.Height = 25;
            block5.Width = 200;
            Thickness mar = new Thickness(10, 5, 10, 10);
            block5.Margin = mar;
            Quantity.Children.Add(block5);
            int totalQuan = 0;
            while (reader4.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader4["Quantity"].ToString();
                
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Quantity.Children.Add(block);
                totalQuan += Int32.Parse(reader4["Quantity"].ToString());

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Quantity.Children.Add(text1);
            }
            TotalQuan.Text = totalQuan.ToString();
            reader4.Close();

            MySqlDataReader reader8 = cmd.ExecuteReader();
            TextBlock block9 = new TextBlock();
            block9.FontSize = 15;
            block9.FontFamily = new FontFamily("Rockwell");
            block9.TextAlignment = TextAlignment.Center;
            block9.Background = Brushes.Black;
            block9.Foreground = Brushes.MintCream;
            block9.Text = "Supplier:";
            block9.Height = 25;
            block9.Width = 200;
            block9.Margin = margin;
            Supplier.Children.Add(block9);
            while (reader8.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = reader8["Supplier"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Supplier.Children.Add(block);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Supplier.Children.Add(text1);
            }
            reader8.Close();

            MySqlDataReader reader5 = cmd.ExecuteReader();
            TextBlock block6 = new TextBlock();
            block6.FontSize = 15;
            block6.FontFamily = new FontFamily("Rockwell");
            block6.TextAlignment = TextAlignment.Center;
            block6.Background = Brushes.Black;
            block6.Foreground = Brushes.MintCream;
            block6.Text = "Price Per Item:";
            block6.Height = 25;
            block6.Width = 200;
            block6.Margin = margin;
            Price.Children.Add(block6);
            while (reader5.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = "$"+reader5["Price"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Price.Children.Add(block);

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Price.Children.Add(text1);
            }
            reader5.Close();

            MySqlDataReader reader6 = cmd.ExecuteReader();
            TextBlock block7 = new TextBlock();
            block7.FontSize = 15;
            block7.FontFamily = new FontFamily("Rockwell");
            block7.TextAlignment = TextAlignment.Center;
            block7.Background = Brushes.Black;
            block7.Foreground = Brushes.MintCream;
            block7.Text = "Total Price:";
            block7.Margin = margin;
            block7.Height = 25;
            block7.Width = 200;
            Total.Children.Add(block7);
            double totalValue = 0;
            while (reader6.Read())
            {
                TextBlock block = new TextBlock();
                block.FontSize = 15;
                block.FontFamily = new FontFamily("Rockwell");
                block.TextAlignment = TextAlignment.Center;
                block.Background = Brushes.Black;
                block.Foreground = Brushes.MintCream;
                block.Text = "$"+reader6["Total"].ToString();
                Thickness margin2 = new Thickness(10, 10, 10, 10);
                block.Margin = margin2;
                block.Height = 25;
                block.Width = 200;
                Total.Children.Add(block);
                totalValue += Double.Parse(reader6["Total"].ToString());

                TextBlock text1 = new TextBlock();
                text1.FontSize = 15;
                text1.FontFamily = new FontFamily("Rockwell");
                text1.TextAlignment = TextAlignment.Center;
                text1.Background = Brushes.Black;
                text1.Foreground = Brushes.MintCream;
                text1.Text = "___________________________________________________________";
                Total.Children.Add(text1);
            }
            TotalValue.Text = "$"+totalValue.ToString();
            reader6.Close();

            MySqlDataReader reader7 = cmd.ExecuteReader();
            TextBlock block8 = new TextBlock();
            block8.FontSize = 15;
            block8.FontFamily = new FontFamily("Rockwell");
            block8.TextAlignment = TextAlignment.Center;
            block8.Background = Brushes.Black;
            block8.Foreground = Brushes.MintCream;
            block8.Text = "Edit Product:";
            block8.Height = 25;
            block8.Width = 200;
            block8.Margin = margin;
            Edit.Children.Add(block8);

            while (reader7.Read())
            {



                Button button = new Button();
                button.Width = 120;
                button.Height = 25;
                button.FontFamily = new FontFamily("Rockwell");
                button.FontSize = 14;
                button.Background = Brushes.MintCream;
                button.Foreground = Brushes.Black;
                button.Content = "Edit";
                button.Tag = reader7["ProductName"].ToString();
                button.ContentStringFormat = reader7["Supplier"].ToString();
                button.DataContext = Double.Parse(reader7["Quantity"].ToString());

                button.Click += Edit_Button_Click;
                button.Margin = new Thickness(0, 10, 0, 10);
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

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            InventoryPage page = new InventoryPage(company);
            this.Close();
            page.Show();

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            AddProduct add = new AddProduct(departmentName, company);
            this.Close();
            add.Show();

        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string name = (string)b.Tag;
            string supplier = (string)b.ContentStringFormat;
            string quantity = b.DataContext.ToString();
            EditProduct ed = new EditProduct(name, supplier, departmentName, company,quantity);
            this.Close();
            ed.Show();
        }
    }
}
