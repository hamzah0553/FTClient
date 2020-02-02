using FTClientApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTClientApplication.Service
{
    class DBService
    {
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
        public Municipality GetMunicipality(string name)
        {
            Municipality municipality;
            using (var context = new FTDatabaseEntities())
            {
                municipality = context.Municipality.Where(p => p.name.Equals(name)).SingleOrDefault();
            }
            return municipality;
        }

        public Party GetParty(string initial)
        {
            Party party;
            using (var context = new FTDatabaseEntities())
            {
                party = context.Party.Where(p => p.initial.Equals(initial, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
            }
            return party;
        }
    }
}

