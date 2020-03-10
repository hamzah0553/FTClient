using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;
using FTClientApplication.OdataConsumer;
using FTClientApplication.Service;

namespace FTClientApplication.ViewModel.Dk
{
    class GovernmentVM : IExcelAdapter
    {
        FTDatabaseEntities topLevelContext = new FTDatabaseEntities();
        DBService service = new DBService();

        //Returns list of Government
        public List<Government> GetGovernment()
        {
            return topLevelContext.Government.ToList();
        }

        //Returns ministers depending on government
        public List<CustomMinister> GetMinisters()
        {
            using (var entities = new FTDatabaseEntities())
            {

                List<CustomMinister> ministers = new List<CustomMinister>();
                List<Minister> tempList = entities.Minister.Where(s => s.MinisterialPost.Government.id == 1).ToList();
                foreach (var item in tempList)
                {
                    ContactInfo contact = item.Politician.ContactInfo.FirstOrDefault();
                    CustomMinister minister = new CustomMinister()
                    {
                        GovernmentId = item.MinisterialPost.governmentId,
                        Firstname = item.Politician.firstname,
                        Lastname = item.Politician.lastname,
                        Party = item.Politician.Party.name,
                        Title = item.MinisterialPost.title
                    };
                    if (contact != null)
                    {
                        if (contact.email != null)
                        {
                            minister.Email = contact.email;
                        }
                        if (contact.phone != null)
                        {
                            minister.Phone = contact.phone;
                        }
                    }
                    ministers.Add(minister);
                }
                return ministers;
            }
        }
        //Add Minister to db
        public void AddMinisters()
        {
            using (var entities = new FTDatabaseEntities())
            {
                MinisterScraper scraper = new MinisterScraper();
                List<ExtractedValues> values = scraper.GetMinisters();
                List<CustomMinister> ministers = new List<CustomMinister>();
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
                        ContactInfo contactInfo = item.Contact;
                        contactInfo.politicianId = politician.id;
                        Debug.WriteLine(politician.id);
                        service.AddContactInfo(contactInfo);
                    }
                    MinisterialPost post = service.GetMinisterialPost(item.Title);
                    if (post == null)
                    {
                        using (var context = new FTDatabaseEntities())
                        {
                            context.MinisterialPost.Add(new MinisterialPost() { governmentId = 1, title = item.Title });
                            context.SaveChanges();
                        }
                    }
                    Minister minister = new Minister();
                    minister.ministerialPostId = service.GetMinisterialPost(item.Title).id;
                    minister.politicianId = politician.id;
                    minister.startDate = DateTime.Now.Date;
                    entities.Minister.Add(minister);
                }
                entities.SaveChanges();
            }

        }

        public void UpdateGovernment()
        {
            topLevelContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Minister]");
            AddMinisters();
        }

        //Excel adapter implementation
        public List<List<string>> ConvertData()
        {
            List<CustomMinister> ministers = GetMinisters();
            List<List<string>> lists = new List<List<string>>();
            for (int i = 0; i < ministers.Count; i++)
            {
                lists.Add(new List<string>());
                lists[i].Add(ministers[i].Title);
                lists[i].Add(ministers[i].Firstname);
                lists[i].Add(ministers[i].Party);
                lists[i].Add(ministers[i].Phone);
                lists[i].Add(ministers[i].Email);
            }
            return lists;
        }

        public List<string> GetColumnNames()
        {
            List<string> list = new List<string>();
            list.Add("Minister post");
            list.Add("Fornavn");
            list.Add("Efternavn");
            list.Add("Telefon");
            list.Add("E-mail");
            return list;
        }
    }

    /* public void RemoveMinisters()
     {
         entities.min
     }*/
    //Add politician to db


    public class CustomMinister
    {
        public int GovernmentId { get; set; }
        public int MinisterId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string Party { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
