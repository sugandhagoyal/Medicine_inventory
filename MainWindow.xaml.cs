using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Inventory.Controller;
using Inventory.Model;
using static Inventory.Controller.SearchController;

namespace Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SearchController search = new SearchController();
        AddController add = new AddController();
        public MainWindow()
        {

            InitializeComponent();
            comboBox.Items.Add("=");
            comboBox.Items.Add("<=");
            comboBox.Items.Add(">=");
            comboBox.SelectedValue = "=";

        }

        


        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // if( ((TabItem)((TabControl)sender).SelectedItem).Header.ToString().ToUpper() == "SEARCH") {
             //   refreshMedGrid();
           // }
        }

        private void TabItem_onSelected(object sender,  RoutedEventArgs e) {
             if( ((TabItem)sender).Header.ToString().ToUpper() == "SEARCH") {
                dataGrid.ItemsSource = search.refreshMedGrid().AsDataView();
            }
        }
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(MedName_TextBox.Text))
            {
                MessageBox.Show("Enter Medicine Name");
                return;
            }

            if (String.IsNullOrEmpty( DatePickerAdd.Text)) {
                MessageBox.Show("Enter Expiration date");
                return;
            }
            add.AddMedicine(new MedRowFormat(CompanyNm_TextBox.Text, MedName_TextBox.Text, DatePickerAdd.DisplayDate, 0));


        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            compareOP op= compareOP.EQ;
            if (comboBox.SelectedValue == "=")
                op = compareOP.EQ;
            else if (comboBox.SelectedValue == "<=")
                op = compareOP.LT;
            else if (comboBox.SelectedValue == ">=")
                op = compareOP.GT;
            dataGrid.ItemsSource = search.refreshMedGrid("Search",MedNm_Search_TextBox.Text,DatePickerSearch.Text,op).AsDataView();
        }

        private void Refresh_button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = search.refreshMedGrid().AsDataView();
            MedNm_Search_TextBox.Text = "";
            DatePickerSearch.Text ="" ;
            comboBox.SelectedValue = "=";
        }

        private void Download_button_Click(object sender, RoutedEventArgs e)
        {
            compareOP op = compareOP.EQ;
            if (comboBox.SelectedValue == "=")
                op = compareOP.EQ;
            else if (comboBox.SelectedValue == "<=")
                op = compareOP.LT;
            else if (comboBox.SelectedValue == ">=")
                op = compareOP.GT;
            dataGrid.ItemsSource = search.refreshMedGrid("Search", MedNm_Search_TextBox.Text, DatePickerSearch.Text, op).AsDataView();

        }
    }

    
}
