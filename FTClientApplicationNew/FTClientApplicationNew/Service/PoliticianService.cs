using System.Linq;

using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;

namespace FTClientApplication.Service
{
    class PoliticianService
    {
        FTDatabaseEntities db = new FTDatabaseEntities();
        public Politician GetPolitician(Politician politician)
        {
            Politician newPolitician;
            var result = from pol in db.Politician
                         where pol.firstname.Equals(politician.firstname) && pol.lastname.Equals(politician.lastname)
                         select pol;
            if (result.Any())
            {
                newPolitician = result.First();
                return newPolitician;
            }
            else
            {
                return null;
            }
        }
        public Politician GetPolitician(int id)
        {
            Politician newPolitician;
            var result = from pol in db.Politician
                         where pol.id == id
                         select pol;
            if (result.Any())
            {
                newPolitician = result.First();
                return newPolitician;
            }
            else
            {
                return null;
            }
        }
        public Politician GetPolitician(string firstname, string lastname)
        {
            Politician newPolitician;
            var result = from pol in db.Politician
                         where pol.firstname.Equals(firstname) && pol.lastname.Equals(lastname)
                         select pol;
            if (result.Any())
            {
                newPolitician = result.First();
                return newPolitician;
            }
            else
            {
                return null;
            }
        }

        public bool CheckPoliticianExist(Politician politician)
        {
            var newPol = GetPolitician(politician.firstname, politician.lastname);
            if (newPol == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateNewPolitician(Politician politician, ContactInfo contact)
        {
            if (!CheckPoliticianExist(politician))
            {
                db.Politician.Add(politician);
                db.SaveChanges();
            }
            if (true)
            {

            }

        }
        public void CreateNewPolitician(Politician politician)
        {
            if (!CheckPoliticianExist(politician))
            {
                db.Politician.Add(politician);
                db.SaveChanges();
            }
            else
            {
                /*if (!(politician.Contact == null))
                {
                    if (politician.Contact.Phone == null)
                    {
                        politician.Contact.Phone = "";
                    }
                    if (politician.Contact.Email == null)
                    {
                        politician.Contact.Email = "";
                    }
                    Politician politician1 = GetPolitician(politician);
                    politician.Contact.PoliticianId = politician1.Id;
                    db.ContactInfos.InsertOnSubmit(politician.Contact);
                    db.SubmitChanges();*/
                
            }
        }
    }
}
