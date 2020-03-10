using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;
using FTClientApplication.OdataConsumer;
using FTClientApplication.Service;

namespace FTClientApplication.ViewModel.Dk
{
    class ParliamentVM: IExcelAdapter
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

        //gets members from specific parliament
        public List<CustomPolitcian> GetParliamentWithMembers()
        {
            Parliament selectedParliament = entities.Parliament.Where(p => p.startYear == 2019).SingleOrDefault();
            var members = entities.ParliamentMember.Where(p => p.parliamentId == selectedParliament.id).ToList();
            List<CustomPolitcian> politcians = new List<CustomPolitcian>();
            foreach (var member in members)
            {
                var contact = member.Politician.ContactInfo.FirstOrDefault();
                if(contact == null)
                {
                    contact = new ContactInfo();
                    contact.id = 0;
                    contact.phone = null;
                    contact.email = null;
                }
                politcians.Add(new CustomPolitcian() 
                {
                    PoliticianId = member.Politician.id,
                    ContactId = contact.id,
                    Firstname = member.Politician.firstname,
                    Lastname = member.Politician.lastname,
                    Email = contact.email,
                    Phone = contact.phone,
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
        //add parliament member to db only
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
        //Implemented methods from excel adapter
        public List<List<string>> ConvertData()
        {
            List<CustomPolitcian> politcians = GetParliamentWithMembers();
            List<List<string>> lists = new List<List<string>>();
            for (int i = 0; i < politcians.Count; i++)
            {
                lists.Add(new List<string>());
                lists[i].Add(politcians[i].Firstname);
                lists[i].Add(politcians[i].Lastname);
                lists[i].Add(politcians[i].Phone);
                lists[i].Add(politcians[i].Email);
                lists[i].Add(politcians[i].Party);
            }
            return lists;
        }

        public List<string> GetColumnNames()
        {
            List<string> list = new List<string>();
            list.Add("Fornavn");
            list.Add("Efternavn");
            list.Add("Telefon");
            list.Add("E-mail");
            list.Add("Parti");
            return list;
        }

        public void AddMembers()
        {
            using (var entities = new FTDatabaseEntities())
            {
                DBService service = new DBService();
                ParliamentMemberFilter filter = new ParliamentMemberFilter();
                List<ExtractedValues> values = filter.GetParliamentMembers();
                List<CustomPolitcian> politcians = new List<CustomPolitcian>();
                foreach (var item in values)
                {
                    Politician politician = service.GetPolitician(item.Firstname, item.Lastname);
                    if (politician == null)
                    {
                        politician = new Politician();
                        politician.firstname = item.Firstname;
                        politician.lastname = item.Lastname;
                        politician.partyId = entities.Party.Where(p => p.name.Equals(item.Party)).SingleOrDefault().id;
                        service.AddPolitician(politician);

                        politician = service.GetPolitician(item.Firstname, item.Lastname);
                        UpdateContact(politician.id, item.Contact);
                    }
                    else
                    {
                        var partyId = entities.Party.Where(p => p.name.Equals(item.Party)).SingleOrDefault().id;
                        politician.partyId = partyId;
                        entities.Politician.Attach(politician);
                        entities.Entry(politician).Property(pol => pol.partyId).IsModified = true;
                        entities.SaveChanges();
                    }
                    UpdateContact(politician.id, item.Contact);
                    ParliamentMember member = new ParliamentMember();
                    member.politicianId = politician.id;
                    member.parliamentId = 1;
                    entities.ParliamentMember.Add(member);
                }
                entities.SaveChanges();
            }
        }

        private void UpdateContact(int politicianId, ContactInfo newContact)
        {
            DBService service = new DBService();
            ContactInfo contactInfo = newContact;
            var checkContact = entities.ContactInfo.Where(c => c.email.Equals(contactInfo.email) && c.phone.Equals(contactInfo.phone));
            if (!checkContact.Any())
            {
                contactInfo.politicianId = politicianId;
                service.AddContactInfo(contactInfo);
            }
            else
            {
                checkContact.First().email = newContact.email;
                checkContact.First().phone = newContact.phone;
                using (var context = new FTDatabaseEntities())
                {
                    ContactInfo testContact = checkContact.First();
                    testContact.phone = newContact.phone;
                    testContact.email = newContact.email;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateMembers()
        {
            entities.Database.ExecuteSqlCommand("ALTER TABLE [Selection_member] DROP CONSTRAINT FK_Selection_member_ParliamentMember");
            entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Selection_member]");
            entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [ParliamentMember]");
            entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [ContactInfo]");
            entities.Database.ExecuteSqlCommand("ALTER TABLE [Selection_member] " +
                "ADD CONSTRAINT FK_Selection_member_ParliamentMember FOREIGN KEY (parliamentMemberId) " +
                "REFERENCES [ParliamentMember] (id) ON DELETE CASCADE ON UPDATE CASCADE");
            AddMembers();
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
