using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;

namespace FTClientApplication.Service
{
    class MinisterService
    {
        FTDatabaseEntities db = new FTDatabaseEntities();

        public Minister GetMinister(int id)
        {
            Minister minister;
            var result = from min in db.Minister
                         where min.id == id
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
            var newMinister = GetMinister(minister.id);
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
                db.Minister.Add(minister);
                db.SaveChanges();
            }
        }
    }
}
