using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;

namespace FTClientApplication.ViewModel.Dk
{
    class ParliamentVM
    {

        FTDatabaseEntities entities = new FTDatabaseEntities();
        public void AddParliament()
        {
            Parliament parliament = new Parliament();
            parliament.startYear = 2015;
            parliament.endYear = 2019;
            entities.Parliament.Add(parliament);
            entities.SaveChanges();
        }

    }
}
