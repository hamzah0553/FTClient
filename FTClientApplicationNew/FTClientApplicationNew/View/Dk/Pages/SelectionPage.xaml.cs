using FTClientApplication.ViewModel.Dk;
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

namespace FTClientApplication.View.Dk.Pages
{
    /// <summary>
    /// Interaction logic for SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        SelectionVM selectionVM = new SelectionVM();
        int currentYear = 0;
        public SelectionPage()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        //Fills the comboboxes from selectionVM
        private void FillComboBoxes()
        {
            foreach (var item in selectionVM.GetSelections())
            {
                selectionBox.Items.Add(item.name);
            }
        }
        private void LoadData(string selection)
        {
            selectionVM = null;
            selectionVM = new SelectionVM();
            var members = selectionVM.GetSelectionMembers(selection);
            foreach (var member in members)
            {
                if (member != null)
                {
                    selectionGrid.Items.Add(member);
                }
            }
        }

        private void RefreshParliamentGrid()
        {
            selectionGrid.Items.Clear();
            LoadData(selectionBox.Text);
        }

        //events
        private void selectionBox_DropDownClosed(object sender, EventArgs e)
        {
            selectionGrid.Items.Clear();
            LoadData(selectionBox.Text);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void selectionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
