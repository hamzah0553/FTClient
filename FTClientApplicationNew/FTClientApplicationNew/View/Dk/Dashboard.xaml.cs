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
using System.Windows.Navigation;

using FTClientApplication.View.Dk.Pages;

namespace FTClientApplication.View.Dk
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void ParliamentButtonClick(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new ParliamentPage());
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new HomePage());
        }

        private void selectionBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new SelectionPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new GovernmentPage());
        }
    }
}
