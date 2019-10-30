using System.Diagnostics;
using System.Linq;

using OdataFtClientConsumer.System;
using OdataFtClientConsumer.System.Model;

namespace OdataFtClientConsumer.Service
{
    class PartyService
    {
        FTContext db = new FTContext();
        public PartyService()
        {
            Debug.WriteLine("PartyService is running");
        }

        public Party GetSpecificParty(string name)
        {
            Party party;
            var result = from pars in db.Parties
                          where pars.Name.Equals(name)
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
            var result = from pars in db.Parties
                         where pars.Id == id
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
            var newParty = GetSpecificParty(party.Name);
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
                db.Parties.InsertOnSubmit(party);
                db.SubmitChanges();
            }
            
        }
    }
}
