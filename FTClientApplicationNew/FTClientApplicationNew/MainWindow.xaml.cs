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
using FTClientApplication.ViewModel;

namespace FTClientApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Danish_Parliament(object sender, RoutedEventArgs e)
        {
            MainApp appVM = new MainApp();
            appVM.ChangeToMainDKDashBoard(this);
        }
        private void Open_EU_Parliament(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("EU parliament dashboard is opened");
        }
    }
}
