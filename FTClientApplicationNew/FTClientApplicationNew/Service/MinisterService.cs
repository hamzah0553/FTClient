using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OdataFtClientConsumer.System;
using OdataFtClientConsumer.System.Model;

namespace OdataFtClientConsumer.Service
{
    class MinisterService
    {
        FTContext db = new FTContext();

        public Minister GetMinister(int id)
        {
            Minister minister;
            var result = from min in db.Ministers
                         where min.Id == id
                         select min;
            if (result.Any())
            {
                minister = result.First();
                return minister;
            }
            else
            {
                return null;
            }
        }

        public bool CheckIfMinisterExist(Minister minister)
        {
            var newMinister = GetMinister(minister.Id);
            if (newMinister == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateNewMinister(Minister minister)
        {
            if (!CheckIfMinisterExist(minister))
            {
                db.Ministers.InsertOnSubmit(minister);
                db.SubmitChanges();
            }
        }
    }
}
