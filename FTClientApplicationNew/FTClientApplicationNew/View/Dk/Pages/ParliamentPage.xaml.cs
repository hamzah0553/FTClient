using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

using FTClientApplication;
using FTClientApplication.Model;
using FTClientApplication.View.Dk.Crud;
using FTClientApplication.ViewModel.Dk;

namespace FTClientApplication.View.Dk.Pages
{
    /// <summary>
    /// Interaction logic for Parliament.xaml
    /// </summary>
    public partial class ParliamentPage : Page
    {
        int currentYear = 0;
        ParliamentVM parliament = new ParliamentVM();
        public ParliamentPage()
        {
            InitializeComponent();
            editBtn.IsEnabled = false;
            insertBtn.IsEnabled = false;
            deleteBtn.IsEnabled = false;
            RefreshParliamentGrid();
        }

        //load the members to the datagrid
        private void Load(int selectedYear)
        {
            parliament = null;
            parliament = new ParliamentVM();
            var politicians = parliament.GetParliamentWithMembers();
            foreach (var politician in politicians)
            {
                if (politician != null)
                {
                    parliamentGrid.Items.Add(politician);
                    insertBtn.IsEnabled = true;
                }
            }
        }

        private void RefreshParliamentGrid()
        {
            parliamentGrid.Items.Clear();
            Load(currentYear);
        }

        //Events

        private void Edit_Unloaded(object sender, RoutedEventArgs e)
        {
            RefreshParliamentGrid();
        }

        private void parliamentBox_DropDownClosed(object sender, EventArgs e)
        {
            parliamentGrid.Items.Clear();
            editBtn.IsEnabled = false;
            deleteBtn.IsEnabled = false;
            Load(currentYear);
        }

        private void parliamentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editBtn.IsEnabled = true;
            deleteBtn.IsEnabled = true;
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomPolitcian politcian = parliamentGrid.SelectedItem as CustomPolitcian;
            InsertWindow insertWindow = new InsertWindow();
            insertWindow.Show();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomPolitcian politcian = parliamentGrid.SelectedItem as CustomPolitcian;
            EditWindow edit = new EditWindow(politcian);

            edit.Show();
            edit.Unloaded += Edit_Unloaded; ;
        }
    }
}
