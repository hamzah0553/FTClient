using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;
using FTClientApplication.OdataConsumer;
using FTClientApplication.Service;
using FTClientApplication.View.Dk;
using FTClientApplication.ViewModel.Dk;

namespace FTClientApplication.ViewModel
{
    class MainApp
    {
        public void ChangeToMainDKDashBoard(MainWindow window)
        {
            Dashboard dashboard = new Dashboard();
            App.Current.MainWindow = dashboard;
            window.Close();
            dashboard.Show();
            
        }

    }
}
