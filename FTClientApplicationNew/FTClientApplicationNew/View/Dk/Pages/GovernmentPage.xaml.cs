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

using FTClientApplication.ViewModel.Dk;

namespace FTClientApplication.View.Dk.Pages
{
    /// <summary>
    /// Interaction logic for GovernmentPage.xaml
    /// </summary>
    public partial class GovernmentPage : Page
    {
        GovernmentVM governmentVM = new GovernmentVM();
        public GovernmentPage()
        {
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            List<CustomMinister> ministers = governmentVM.GetMinisters();
            foreach (var item in ministers)
            {
                governmentGrid.Items.Add(item);
            }
        }

        private void governmentBox_DropDownClosed(object sender, EventArgs e)
        {

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

        private void governmentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
