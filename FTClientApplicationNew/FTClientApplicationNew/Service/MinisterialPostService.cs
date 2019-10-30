using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OdataFtClientConsumer.System;
using OdataFtClientConsumer.System.Model;

namespace OdataFtClientConsumer.Service
{
    class MinisterialPostService
    {
        FTContext db = new FTContext();

        public MinisterialPost GetMinisterTitle(string title)
        {
            MinisterialPost ministerTitle;
            var result = from minTitle in db.MinisterTitles
                         where minTitle.Title.Equals(title)
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
            var newParty = GetMinisterTitle(ministerTitle.Title);
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
                db.MinisterTitles.InsertOnSubmit(title);
                db.SubmitChanges();
            }

        }
    }
}
