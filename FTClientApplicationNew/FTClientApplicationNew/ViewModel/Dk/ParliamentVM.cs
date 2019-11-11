using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;
using FTClientApplication.OdataConsumer;

namespace FTClientApplication.ViewModel.Dk
{
    class ParliamentVM
    {

        FTDatabaseEntities entities = new FTDatabaseEntities();
        public void AddParliament()
        {
            Parliament parliament = new Parliament();
            DateTime date = DateTime.Now;
            parliament.startYear = date.Year;
            entities.Parliament.Add(parliament);
            entities.SaveChanges();
        }

        public List<Parliament> GetParliaments()
        {
            return entities.Parliament.ToList();
        }

        public void EditMember(CustomPolitcian customPolitcian)
        {
            var politcian = entities.Politician.SingleOrDefault(pol => pol.id == customPolitcian.PoliticianId);
            politcian.firstname = customPolitcian.Firstname;
            politcian.lastname = customPolitcian.Lastname;
            var party = entities.Party.SingleOrDefault(par => par.name.Equals(customPolitcian.Party));
            politcian.partyId = party.id;

        }

        //gets members from specifik parliament
        public List<CustomPolitcian> GetParliamentWithMembers(int parliamentYear)
        {
            Parliament selectedParliament = entities.Parliament.Where(p => p.startYear == parliamentYear).SingleOrDefault();
            var members = entities.ParliamentMember.Where(p => p.parliamentId == selectedParliament.id).ToList();
            List<CustomPolitcian> politcians = new List<CustomPolitcian>();
            foreach (var member in members)
            {
                politcians.Add(new CustomPolitcian() 
                {
                    PoliticianId = member.Politician.id,
                    ContactId = member.Politician.ContactInfo.SingleOrDefault().id,
                    Firstname = member.Politician.firstname,
                    Lastname = member.Politician.lastname,
                    Email = member.Politician.ContactInfo.SingleOrDefault().email,
                    Phone = member.Politician.ContactInfo.SingleOrDefault().phone,
                    Party = member.Politician.Party.name 
                });
            }
            return politcians;

        }
    }

    //the displayed data in datagrid view
    class CustomPolitcian
    {
        public int PoliticianId { get; set; }
        public int ContactId { get; set; }
        public string Firstname {get; set;}
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Party { get; set; }


    }

}
