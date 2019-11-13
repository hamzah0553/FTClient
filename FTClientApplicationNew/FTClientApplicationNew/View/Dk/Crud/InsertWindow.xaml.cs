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
using System.Windows.Shapes;

namespace FTClientApplication.View.Dk.Crud
{
    /// <summary>
    /// Interaction logic for InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        ParliamentVM parliamentVM = new ParliamentVM();
        private CustomPolitcian customPolitcian;
        public InsertWindow()
        {

            InitializeComponent();
            FillPartyBox();
        }
        private void FillPartyBox()
        {
            foreach (var item in parliamentVM.GetParties())
            {
                partyBox.Items.Add(item.name);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
