using FTClientApplication.Model;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        ParliamentVM parliamentVM = new ParliamentVM();
        private CustomPolitcian customPolitcian;
        public EditWindow(CustomPolitcian politcian)
        {
            customPolitcian = politcian;

            InitializeComponent();
            FillPartyBox();
            FillTextBoxes();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            customPolitcian.Firstname = firstname.Text;
            customPolitcian.Lastname = lastname.Text;
            customPolitcian.Email = email.Text;
            customPolitcian.Phone = phone.Text;
            parliamentVM.EditMember(customPolitcian);
            this.Close();
            
        }

        private void FillPartyBox()
        {
            foreach (var item in parliamentVM.GetParties())
            {
                partyBox.Items.Add(item.name);
            }
        }

        //fill out the text boxes with the included politician
        private void FillTextBoxes()
        {
            firstname.Text = customPolitcian.Firstname;
            lastname.Text = customPolitcian.Lastname;
            partyBox.SelectedIndex = partyBox.Items.IndexOf(customPolitcian.Party);
            email.Text = customPolitcian.Email;
            phone.Text = customPolitcian.Phone;
        }
    }
}
