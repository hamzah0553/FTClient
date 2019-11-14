using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTClientApplication.OdataConsumer;
using FTClientApplication.View.Dk;

namespace FTClientApplication.ViewModel
{
    class MainApp
    {
        public void ChangeToMainDKDashBoard(MainWindow window)
        {
            //SelectionScraper selection = new SelectionScraper(1);
            //selection.GetAllMembers();
            Dashboard dashboard = new Dashboard();
            App.Current.MainWindow = dashboard;
            window.Close();
            dashboard.Show();
            
        }
    }
}
