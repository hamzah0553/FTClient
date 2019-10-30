using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void EditMember(CustomPolitcian customPolitcian)
        {
            var politcian = entities.Politician.SingleOrDefault(pol => pol.id == customPolitcian.PoliticianId);
            politcian.firstname = customPolitcian.Firstname;
            politcian.lastname = customPolitcian.Lastname;
            var party = entities.Party.SingleOrDefault(par => par.name.Equals(customPolitcian.Party));
            politcian.partyId = party.id;

        }

        public List<CustomPolitcian> GetParliamentWithMembers(int parliamentÏd)
        {
            /*var members = (from politican in entities.Politician
                           join member in entities.ParliamentMember on
                           politican.id equals member.politicianId
                           join party in entities.Party on politican.partyId equals party.id
                           join contact in entities.ContactInfo on politican.id equals contact.politicianId
                           select new
                           {
                               firstname = politican.firstname,
                               lastname = politican.lastname,
                               party = party.name,
                               phone = contact.phone,
                               email = contact.email

                           }).ToList();*/
            var members = entities.ParliamentMember.Where(p => p.parliamentId == parliamentÏd).ToList();
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
