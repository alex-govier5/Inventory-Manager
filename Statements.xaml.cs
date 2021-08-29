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
    /// Interaction logic for Statements.xaml
    /// </summary>
    public partial class Statements : Window
    {
        string company;
        public Statements(string company)
        {
            InitializeComponent();
            this.company = company;
            Title.Text = company;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            InventoryPage iv = new InventoryPage(company);
            this.Close();
            iv.Show();
        }

        private void RecPurchase_Button_Click(object sender, RoutedEventArgs e)
        {
            SinglePurchase sp = new SinglePurchase(company);
            this.Close();
            sp.Show();
        }

        private void RecSale_Button_Click(object sender, RoutedEventArgs e)
        {
            SingleSale ss = new SingleSale(company);
            this.Close();
            ss.Show();
        }

        private void Stock_Button_Click(object sender, RoutedEventArgs e)
        {
            StockInfo si = new StockInfo(company);
            this.Close();
            si.Show();
        }
    }
}
