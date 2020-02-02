using FTClientApplication.Model;
using FTClientApplication.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTClientApplication.ViewModel.Dk
{
    class MayorVM: IExcelAdapter
    {

        FTDatabaseEntities entities = new FTDatabaseEntities();
        DBService service = new DBService();


        public void AddMayors(List<CustomMayor> mayors)
        {

            List<CustomMayor> ministers = new List<CustomMayor>();

            foreach (var item in mayors)
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
                    ContactInfo contactInfo = new ContactInfo();
                    contactInfo.email = item.Email;
                    contactInfo.phone = item.Phone;
                    contactInfo.politicianId = politician.id;
                    service.AddContactInfo(contactInfo);
                }
                Municipality municipality = service.GetMunicipality(item.Municipality);
                if (municipality == null)
                {
                    using (var context = new FTDatabaseEntities())
                    {
                        context.Municipality.Add(new Municipality() { regionId = 1, name = item.Municipality });
                        context.SaveChanges();
                    }
                }
                Mayor mayor = new Mayor();
                mayor.politicianId = politician.id;
                mayor.municipalityId = service.GetMunicipality(item.Municipality).id;
                entities.Mayor.Add(mayor);
            }
            entities.SaveChanges();
        }

        public List<CustomMayor> GetMayors()
        {
            List<CustomMayor> mayors = new List<CustomMayor>();
            List<Mayor> tempList = entities.Mayor.ToList();
            foreach (var item in tempList)
            {
                ContactInfo contact = item.Politician.ContactInfo.FirstOrDefault();
                CustomMayor mayor = new CustomMayor()
                {
                    Firstname = item.Politician.firstname,
                    Lastname = item.Politician.lastname,
                    Party = item.Politician.Party.name,
                    Municipality = item.Municipality.name,
                    Region = item.Municipality.Region.name
                };
                if (contact != null)
                {
                    if (contact.email != null)
                    {
                        mayor.Email = contact.email;
                    }
                    if (contact.phone != null)
                    {
                        mayor.Phone = contact.phone;
                    }
                }
                mayors.Add(mayor);
            }
            return mayors;
        }

        public string GetPartyFromInitial(string initial)
        {
            if (initial.Equals("K", StringComparison.InvariantCultureIgnoreCase))
            {
                initial = "KF";
            }
            if (initial.Equals("R", StringComparison.InvariantCultureIgnoreCase))
            {
                initial = "RV";
            }
            Debug.WriteLine(initial);
            return service.GetParty(initial).name;
        }

        public List<string> GetColumnNames()
        {
            List<string> list = new List<string>();
            list.Add("Fornavn");
            list.Add("Efternavn");
            list.Add("E-mail");
            list.Add("Telefon");
            list.Add("Parti");
            list.Add("Kommune");
            list.Add("Region");
            return list;
        }

        public List<List<string>> ConvertData()
        {
            List<CustomMayor> mayors = GetMayors();
            List<List<string>> lists = new List<List<string>>();
            for (int i = 0; i < mayors.Count; i++)
            {
                lists.Add(new List<string>());
                lists[i].Add(mayors[i].Firstname);
                lists[i].Add(mayors[i].Lastname);
                lists[i].Add(mayors[i].Email);
                lists[i].Add(mayors[i].Phone);
                lists[i].Add(mayors[i].Party);
                lists[i].Add(mayors[i].Municipality);
                lists[i].Add(mayors[i].Region);
            }
            return lists;
        }
    }

        

    public class CustomMayor 
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Party { get; set; }
        public string Municipality { get; set; }
        public string Region { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
