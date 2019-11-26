using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;
using FTClientApplication.OdataConsumer;

namespace FTClientApplication.ViewModel.Dk
{
    class GovernmentVM
    {
        FTDatabaseEntities entities = new FTDatabaseEntities();

        //Returns list of Government
        public List<Government> GetGovernment()
        {
            return entities.Government.ToList();
        }
        
        //Returns ministers depending on government
        public List<CustomMinister> GetMinisters()
        {
            List<CustomMinister> ministers = new List<CustomMinister>();
            List<Minister> tempList = entities.Minister.Where(s => s.MinisterialPost.Government.id == 1).ToList();
            foreach (var item in tempList)
            {
                CustomMinister minister = new CustomMinister()
                {
                    GovernmentId = item.MinisterialPost.governmentId,
                    Firstname = item.Politician.firstname,
                    Lastname = item.Politician.lastname,
                    Party = item.Politician.Party.name,
                    Title = item.MinisterialPost.title
                };
                ministers.Add(minister);
            }
            return ministers;
        }
        //Add Minister to db
        public void AddMinisters()
        {
            MinisterScraper scraper = new MinisterScraper();
            List<ExtractedValues> values = scraper.GetMinisters();
            List<CustomMinister> ministers = new List<CustomMinister>();
            foreach (var item in values)
            {
                Politician politician = GetPolitician(item.Firstname, item.Lastname);
                if (politician == null)
                {
                    politician = new Politician();
                    politician.firstname = item.Firstname;
                    politician.lastname = item.Lastname;
                    politician.partyId = entities.Party.Where(p => p.name.Equals(item.Party)).SingleOrDefault().id;
                    AddPolitician(politician);

                    politician = GetPolitician(item.Firstname, item.Lastname);
                    ContactInfo contactInfo = item.Contact;
                    contactInfo.id = politician.id;
                    Debug.WriteLine(politician.id);
                    AddContactInfo(contactInfo);
                }
                MinisterialPost post = GetMinisterialPost(item.Title);
                if (post == null)
                {
                    using (var context = new FTDatabaseEntities())
                    {
                        context.MinisterialPost.Add(new MinisterialPost() { governmentId = 1, title = item.Title});
                        context.SaveChanges();
                    }
                }
                Minister minister = new Minister();
                minister.ministerialPostId = GetMinisterialPost(item.Title).id;
                minister.politicianId = politician.id;
                minister.startDate = DateTime.Now.Date;
                entities.Minister.Add(minister);  
            }
            entities.SaveChanges();
        }
        //Add politician to db
        public void AddPolitician(Politician politician)
        {
            using (var context = new FTDatabaseEntities())
            {
                context.Politician.Add(politician);
                context.SaveChanges();
            }
        }
        //Add contact info
        public void AddContactInfo(ContactInfo contact)
        {
            using (var context = new FTDatabaseEntities())
            {
                context.ContactInfo.Add(contact);
                context.SaveChanges();
            }
        }

        //Add ministers
        public void AddMinister(Minister minister)
        {
            using (var context = new FTDatabaseEntities())
            {
                context.Minister.Add(minister);
                context.SaveChanges();
            }
        }


        //Get politician id with name
        public Politician GetPolitician(string firstname, string lastname)
        {
            Politician politician;
            using (var context = new FTDatabaseEntities())
            {
                politician = context.Politician.Where(p => p.firstname.Equals(firstname) && p.lastname.Equals(lastname)).SingleOrDefault();
            }
            return politician;
        }
        
        public MinisterialPost GetMinisterialPost(string title)
        {
            MinisterialPost post;
            using (var context = new FTDatabaseEntities())
            {
                post = context.MinisterialPost.Where(p => p.title.Equals(title)).SingleOrDefault();
            }
            return post;
        }
    }

    public class CustomMinister
    {
        public int GovernmentId { get; set; }
        public int MinisterId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string Party { get; set; }
    }
}
