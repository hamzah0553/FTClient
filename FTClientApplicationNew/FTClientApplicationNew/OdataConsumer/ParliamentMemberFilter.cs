using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;

namespace FTClientApplication.OdataConsumer
{
    class ParliamentMemberFilter
    {
        string url = "https://oda.ft.dk/api/Akt%C3%B8r?$inlinecount=allpages&$filter=substringof(%27Medlem%20af%20folketinget%27,biografi)%20eq%20true";

        public List<ExtractedValues> GetParliamentMembers()
        {
            List<ExtractedValues> politicians = FilterParliamentMembers();
            return politicians;
        }

        private List<ExtractedValues> FetchAllPoliticians()
        {
            List<ExtractedValues> politicians = new List<ExtractedValues>();
            bool hasNextLink = true;
            while (hasNextLink)
            {
                OdataClient client = new OdataClient(url);
                Odata page = client.fetchData();

                foreach (var politician in page.Value)
                {
                    BiographyReader reader = new BiographyReader(politician.Biografi);
                    reader.ReadBiography();
                    ExtractedValues tempPol = reader.GetPolitician();
                    politicians.Add(tempPol);
                }
                if (page.NextLink == null)
                {
                    hasNextLink = false;
                }
                else { url = page.NextLink; }
            }
            return politicians;
        }

        private List<ExtractedValues> FilterParliamentMembers()
        {
            List<ExtractedValues> politicians = FetchAllPoliticians();

            for (int i = 0; i < politicians.Count; i++)
            {
                politicians[i].Title = politicians[i].Title.Trim();
                if (!politicians[i].Title.Equals("Medlem af Folketinget"))
                {
                    politicians.RemoveAt(i);
                    i = i - 1;
                }

                if (politicians[i].Firstname.Equals("Stine") && politicians[i].Lastname.Equals("Brix"))
                {
                    politicians.RemoveAt(i);
                    i = i - 1;
                }

                if (politicians[i].Firstname.Equals("Maja") && politicians[i].Lastname.Equals("Panduro"))
                {
                    politicians.RemoveAt(i);
                    i = i - 1;
                }
            }
            return politicians;
        }

    }
}
