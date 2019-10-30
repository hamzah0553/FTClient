using System.Linq;

using OdataFtClientConsumer.System;
using OdataFtClientConsumer.System.Model;

namespace OdataFtClientConsumer.Service
{
    class PoliticianService
    {
        FTContext db = new FTContext();
        public Politician GetPolitician(Politician politician)
        {
            Politician newPolitician;
            var result = from pol in db.Politicians
                         where pol.Firstname.Equals(politician.Firstname) && pol.Lastname.Equals(politician.Lastname)
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
            var result = from pol in db.Politicians
                         where pol.Id == id
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
            var result = from pol in db.Politicians
                         where pol.Firstname.Equals(firstname) && pol.Lastname.Equals(lastname)
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
            var newPol = GetPolitician(politician.Firstname, politician.Lastname);
            if (newPol == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateNewPolitician(Politician politician, Contact contact)
        {
            if (!CheckPoliticianExist(politician))
            {
                db.Politicians.InsertOnSubmit(politician);
                db.SubmitChanges();
            }
            if (true)
            {

            }

        }
        public void CreateNewPolitician(Politician politician)
        {
            if (!CheckPoliticianExist(politician))
            {
                db.Politicians.InsertOnSubmit(politician);
                db.SubmitChanges();
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
