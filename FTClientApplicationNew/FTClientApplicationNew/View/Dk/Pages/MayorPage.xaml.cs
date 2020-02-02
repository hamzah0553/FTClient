using FTClientApplication.Service;
using FTClientApplication.ViewModel.Dk;
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


namespace FTClientApplication.View.Dk.Pages
{
    /// <summary>
    /// Interaction logic for MayorPage.xaml
    /// </summary>
    public partial class MayorPage : Page
    {
        MayorVM mayorVM = new MayorVM();
        public MayorPage()
        {
            InitializeComponent();
            LoadData();
           
        }

        private void LoadData()
        {
            List<CustomMayor> mayors = mayorVM.GetMayors();
            foreach (var item in mayors)
            {
                mayorGrid.Items.Add(item);
            }
        }

        private void parliamentBox_DropDownClosed(object sender, EventArgs e)
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

        private void parliamentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            ExcelAssembler assembler = new ExcelAssembler();
            assembler.OutputToExcel();
        }
    }
}
