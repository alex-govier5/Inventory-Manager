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
    /// Interaction logic for StockInfo.xaml
    /// </summary>
    public partial class StockInfo : Window
    {
        string company;
        public StockInfo(string company)
        {
            InitializeComponent();
            this.company = company;
            Title.Text = company;
            string connectionString = "server = sql5.freemysqlhosting.net; user=sql5430414; database= sql5430414; password=gjj6LGuzc7;SSL Mode=none";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mySqlConnection;
            string companyNameNoSpace = String.Concat(company.Where(c => !Char.IsWhiteSpace(c)));
            string insertQuery = @"SELECT * FROM "+companyNameNoSpace+"Departments";
            cmd.CommandText = insertQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            Thickness margin = new Thickness(30, 5, 10, 0);
            string[] departments = new string[5000];
            int departmentCount = 0;
            while (reader.Read())
            {
                TextBlock block = new TextBlock();
                block.FontFamily = new FontFamily("Times New Roman");
                block.FontSize = 15;
                block.Height = 25;
                block.Foreground = Brushes.Black;
                block.Background = Brushes.White;
                block.Margin = margin;
                block.TextAlignment = TextAlignment.Left;
                block.Text = reader["Name"].ToString();
                
                Departments.Children.Add(block);
                departments[departmentCount] = reader["Name"].ToString();
                departmentCount++;
            }

            string[] finalDepartments = new string[departmentCount];
            for(int i = 0; i<departmentCount; i++)
            {
                finalDepartments[i] = departments[i];
            }

            TextBlock block2 = new TextBlock();
            block2.FontFamily = new FontFamily("Times New Roman");
            block2.FontSize = 15;
            block2.Foreground = Brushes.Black;
            block2.Background = Brushes.White;
            block2.Margin = new Thickness(5,5,10,5);
            block2.TextAlignment = TextAlignment.Right;
            block2.Text = "________________________________";
            Departments.Children.Add(block2);

            TextBlock block3 = new TextBlock();
            block3.FontFamily = new FontFamily("Times New Roman");
            block3.FontSize = 15;
            block3.Foreground = Brushes.Black;
            block3.Background = Brushes.White;
            block3.Margin = new Thickness(5,5,10,5);
            block3.TextAlignment = TextAlignment.Right;
            block3.Text = "Number of Departments: "+departmentCount.ToString();
            Departments.Children.Add(block3);
            reader.Close();


            string query2 = @"SELECT * FROM "+companyNameNoSpace+"Suppliers";
            cmd.CommandText = query2;
            reader = cmd.ExecuteReader();
            int supplierCount = 0;
            while (reader.Read())
            {
                TextBlock block = new TextBlock();
                block.FontFamily = new FontFamily("Times New Roman");
                block.FontSize = 15;
                block.Height = 25;
                block.Foreground = Brushes.Black;
                block.Background = Brushes.White;
                block.Margin = margin;
                block.TextAlignment = TextAlignment.Left;
                block.Text = reader["Name"].ToString();

                Suppliers.Children.Add(block);
                supplierCount++;
            }
            TextBlock block4 = new TextBlock();
            block4.FontFamily = new FontFamily("Times New Roman");
            block4.FontSize = 15;
            block4.Foreground = Brushes.Black;
            block4.Background = Brushes.White;
            block4.Margin = new Thickness(5, 5, 10, 5);
            block4.TextAlignment = TextAlignment.Right;
            block4.Text = "________________________________";
            Suppliers.Children.Add(block4);

            TextBlock block5 = new TextBlock();
            block5.FontFamily = new FontFamily("Times New Roman");
            block5.FontSize = 15;
            block5.Foreground = Brushes.Black;
            block5.Background = Brushes.White;
            block5.Margin = new Thickness(5, 5, 10, 5);
            block5.TextAlignment = TextAlignment.Right;
            block5.Text = "Number of Suppliers: " + supplierCount.ToString();
            Suppliers.Children.Add(block5);
            reader.Close();

            int productCount = 0;
            int totalProductCount = 0;


            for (int x = 0; x<finalDepartments.Length; x++)
            {

                TextBlock block = new TextBlock();
                block.FontFamily = new FontFamily("Times New Roman");
                block.FontSize = 15;
                block.Height = 25;
                block.Foreground = Brushes.Black;
                block.Background = Brushes.White;
                block.Margin = margin;
                block.TextAlignment = TextAlignment.Left;
                block.Text = finalDepartments[x];
                Product.Children.Add(block);
                DepartmentInventory di = new DepartmentInventory(finalDepartments[x], company);
                productCount = Int32.Parse(di.TotalProduct.Text);
                totalProductCount += Int32.Parse(di.TotalProduct.Text);

                TextBlock block0 = new TextBlock();
                block0.FontFamily = new FontFamily("Times New Roman");
                block0.FontSize = 15;
                block0.Height = 25;
                block0.Foreground = Brushes.Black;
                block0.Background = Brushes.White;
                block0.Margin = margin;
                block0.TextAlignment = TextAlignment.Right;
                block0.Text = productCount.ToString();
                ProductAmount.Children.Add(block0);
                di.Close();
            }

            TextBlock block7 = new TextBlock();
            block7.FontFamily = new FontFamily("Times New Roman");
            block7.FontSize = 15;
            block7.Foreground = Brushes.Black;
            block7.Background = Brushes.White;
            block7.Margin = new Thickness(0,5,10,5);
            block7.TextAlignment = TextAlignment.Right;
            block7.Text = "________________________________";
            ProductAmount.Children.Add(block7);


            TextBlock block6 = new TextBlock();
            block6.FontFamily = new FontFamily("Times New Roman");
            block6.FontSize = 15;
            block6.Foreground = Brushes.Black;
            block6.Background = Brushes.White;
            block6.Margin = new Thickness(0,5,10,5);
            block6.TextAlignment = TextAlignment.Right;
            block6.Text = "Total number of products: "+totalProductCount.ToString();
            ProductAmount.Children.Add(block6);


            int quantityCount = 0;
            int totalQuantityCount = 0;


            for (int x = 0; x < finalDepartments.Length; x++)
            {

                TextBlock block = new TextBlock();
                block.FontFamily = new FontFamily("Times New Roman");
                block.FontSize = 15;
                block.Height = 25;
                block.Foreground = Brushes.Black;
                block.Background = Brushes.White;
                block.Margin = margin;
                block.TextAlignment = TextAlignment.Left;
                block.Text = finalDepartments[x];
                Department.Children.Add(block);
                DepartmentInventory di = new DepartmentInventory(finalDepartments[x], company);
                quantityCount = Int32.Parse(di.TotalQuan.Text);
                totalQuantityCount += Int32.Parse(di.TotalQuan.Text);

                TextBlock block0 = new TextBlock();
                block0.FontFamily = new FontFamily("Times New Roman");
                block0.FontSize = 15;
                block0.Height = 25;
                block0.Foreground = Brushes.Black;
                block0.Background = Brushes.White;
                block0.Margin = margin;
                block0.TextAlignment = TextAlignment.Right;
                block0.Text = quantityCount.ToString();
                DepartmentQuantity.Children.Add(block0);
                di.Close();
            }

            TextBlock block8 = new TextBlock();
            block8.FontFamily = new FontFamily("Times New Roman");
            block8.FontSize = 15;
            block8.Foreground = Brushes.Black;
            block8.Background = Brushes.White;
            block8.Margin = new Thickness(0,5,10,5);
            block8.TextAlignment = TextAlignment.Right;
            block8.Text = "________________________________";
            DepartmentQuantity.Children.Add(block8);


            TextBlock block9 = new TextBlock();
            block9.FontFamily = new FontFamily("Times New Roman");
            block9.FontSize = 15;
            block9.Foreground = Brushes.Black;
            block9.Background = Brushes.White;
            block9.Margin = new Thickness(0,5,10,5);
            block9.TextAlignment = TextAlignment.Right;
            block9.Text = "Total inventory quantity: " + totalQuantityCount.ToString();
            DepartmentQuantity.Children.Add(block9);

            int valueCount = 0;
            int totalValueCount = 0;


            for (int x = 0; x < finalDepartments.Length; x++)
            {

                TextBlock block = new TextBlock();
                block.FontFamily = new FontFamily("Times New Roman");
                block.FontSize = 15;
                block.Height = 25;
                block.Foreground = Brushes.Black;
                block.Background = Brushes.White;
                block.Margin = margin;
                block.TextAlignment = TextAlignment.Left;
                block.Text = finalDepartments[x];
                Department1.Children.Add(block);
                DepartmentInventory di = new DepartmentInventory(finalDepartments[x], company);
                string tot = di.TotalValue.Text;
                string last = "";
                foreach(char c in tot)
                {
                    if(Regex.IsMatch(c.ToString(), @"^[0-9]+$") == true)
                    {
                        last += c.ToString();
                    }
                    else
                    {
                        continue;
                    }
                }
                valueCount = Int32.Parse(last);
                totalValueCount += Int32.Parse(last);

                TextBlock block0 = new TextBlock();
                block0.FontFamily = new FontFamily("Times New Roman");
                block0.FontSize = 15;
                block0.Height = 25;
                block0.Foreground = Brushes.Black;
                block0.Background = Brushes.White;
                block0.Margin = margin;
                block0.TextAlignment = TextAlignment.Right;
                block0.Text = "$"+valueCount.ToString();
                DepartmentValue.Children.Add(block0);
                di.Close();
            }

            TextBlock block10 = new TextBlock();
            block10.FontFamily = new FontFamily("Times New Roman");
            block10.FontSize = 15;
            block10.Foreground = Brushes.Black;
            block10.Background = Brushes.White;
            block10.Margin = new Thickness(0,5,10,5);
            block10.TextAlignment = TextAlignment.Right;
            block10.Text = "________________________________";
            DepartmentValue.Children.Add(block10);


            TextBlock block11 = new TextBlock();
            block11.FontFamily = new FontFamily("Times New Roman");
            block11.FontSize = 15;
            block11.Foreground = Brushes.Black;
            block11.Background = Brushes.White;
            block11.Margin = new Thickness(0,5,10,5);
            block11.TextAlignment = TextAlignment.Right;
            block11.Text = "Total inventory value: $" + totalValueCount.ToString();
            DepartmentValue.Children.Add(block11);

            mySqlConnection.Close();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Statements st = new Statements(company);
            this.Close();
            st.Show();
        }
    }
}
