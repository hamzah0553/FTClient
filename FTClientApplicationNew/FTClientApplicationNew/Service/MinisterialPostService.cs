using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;

namespace FTClientApplication.Service
{
    class MinisterialPostService
    {
        FTDatabaseEntities db = new FTDatabaseEntities();

        public MinisterialPost GetMinisterTitle(string title)
        {
            MinisterialPost ministerTitle;
            var result = from minTitle in db.MinisterialPost
                         where minTitle.title.Equals(title)
                         select minTitle;
            if (result.Any())
            {
                ministerTitle = result.First();
                return ministerTitle;
            }
            else
            {
                return null;
            }
           
        }

        public bool CheckIfPartyExist(MinisterialPost ministerTitle)
        {
            var newParty = GetMinisterTitle(ministerTitle.title);
            if (newParty == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateNewMinisterTitle(MinisterialPost title)
        {
            if (!CheckIfPartyExist(title))
            {
                db.MinisterialPost.Add(title);
                db.SaveChanges();
            }

        }
    }
}
