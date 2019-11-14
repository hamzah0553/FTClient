using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;

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
        public List<CustomMinister> GetMinisters(DateTime startDate)
        {
            List<CustomMinister> ministers = new List<CustomMinister>();
            List<Minister> tempList = entities.Minister.Where(s => s.MinisterialPost.Government.startDate.Date == startDate.Date).ToList();
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
