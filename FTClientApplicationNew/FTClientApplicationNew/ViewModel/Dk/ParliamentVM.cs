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

        //Create new parliament
        public void AddParliament()
        {
            Parliament parliament = new Parliament();
            DateTime date = DateTime.Now;
            parliament.startYear = date.Year;
            entities.Parliament.Add(parliament);
            entities.SaveChanges();
            
        }

        //returns all parliaments
        public List<Parliament> GetParliaments()
        {
            return entities.Parliament.ToList();
        }

        public List<Party> GetParties()
        {
            return entities.Party.ToList();
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
        //Edit function for parliament members
        public string EditMember(CustomPolitcian politcian)
        {
            string msg = "";
            Politician selectedPolitician = entities.Politician.Where(p => p.id == politcian.PoliticianId).SingleOrDefault();
            selectedPolitician.firstname = politcian.Firstname;
            selectedPolitician.lastname = politcian.Lastname;
            selectedPolitician.partyId = entities.Party.Where(p => p.name.Equals(politcian.Party)).SingleOrDefault().id;
            selectedPolitician.ContactInfo.SingleOrDefault().phone = politcian.Phone;
            selectedPolitician.ContactInfo.SingleOrDefault().email = politcian.Email;
            entities.SaveChanges();

            return msg;
        }
        //add parliament member to db onl
        public bool AddMember(CustomPolitcian politcian)
        {
            if (politcian == null)
            {
                return false;
            }
            else
            {
                if (politcian.Firstname == null || politcian.Lastname == null || politcian.Party == null)
                {
                    return false;
                }
                else
                {
                    entities.Politician.Add(new Politician()
                    {
                        firstname = politcian.Firstname,
                        lastname = politcian.Lastname,
                        partyId = entities.Party.Where(p => p.name.Equals(politcian.Party)).SingleOrDefault().id
                    });
                    entities.ContactInfo.Add(new ContactInfo()
                    {
                        email = politcian.Email,
                        phone = politcian.Phone,
                        politicianId = entities.Politician.Last().id
                    });
                    return true;
                }
            }
        }
    }
    //the displayed data in datagrid view
    public class CustomPolitcian
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
