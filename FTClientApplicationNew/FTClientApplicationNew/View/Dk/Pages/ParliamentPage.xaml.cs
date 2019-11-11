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
        ParliamentVM parliament = new ParliamentVM();
        public ParliamentPage()
        {
            InitializeComponent();
            FillComboBox();
        }

        //Fill combo box with parliament data from database
        private void FillComboBox()
        {
            foreach (var item in parliament.GetParliaments())
            {
                parliamentBox.Items.Add(item.startYear);
            }
        }
        private void Load(int selectedYear)
        {
            var politicians = parliament.GetParliamentWithMembers(selectedYear);
            foreach (var politician in politicians)
            {
                parliamentGrid.Items.Add(politician);
            }
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow();
            edit.Show();
        }

        private void parliamentBox_DropDownClosed(object sender, EventArgs e)
        {
            parliamentGrid.Items.Clear();
            Load((int)parliamentBox.SelectedItem);
        }
    }
}
