using System.Diagnostics;
using System.Linq;

using FTClientApplication.Model;

namespace FTClientApplication.Service
{
    class PartyService
    {
        FTDatabaseEntities db = new FTDatabaseEntities();
        public PartyService()
        {
            Debug.WriteLine("PartyService is running");
        }

        public Party GetSpecificParty(string name)
        {
            Party party;
            var result = from pars in db.Party
                          where pars.name.Equals(name)
                          select pars;
            if (result.Any())
            {
                party = result.First();
                return party;
            }
            else
            {
                return null;
            }
        }
        public Party GetSpecificParty(int id)
        {
            Debug.WriteLine("Get party is working");
            Party party;
            var result = from pars in db.Party
                         where pars.id == id
                         select pars;
            if (result.Any())
            {
                party = result.First();
                Debug.WriteLine("Party Exist");
                return party;
            }
            else
            {
                Debug.WriteLine("Party Does not Exist");
                return null;
            }
        }

        public bool CheckIfPartyExist(Party party)
        {
            var newParty = GetSpecificParty(party.name);
            if (newParty == null)
            {
                Debug.WriteLine("new Party is not null");
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateNewParty(Party party)
        {
            if (!CheckIfPartyExist(party))
            {
                db.Party.Add(party);
                db.SaveChanges();
            }
            
        }
    }
}
