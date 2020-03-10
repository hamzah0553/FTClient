using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

using HtmlAgilityPack;
using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;
using FTClientApplication.ViewModel;

namespace FTClientApplication.OdataConsumer
{
    class MinisterScraper
    {
        HtmlWeb web = new HtmlWeb();
        string url = "https://www.ft.dk/da/Medlemmer/Regeringen";
        HtmlDocument document;
    
        public MinisterScraper()
        {
            document = web.Load(this.url);
        }
        public List<ExtractedValues> GetMinisters()
        {
            List<string> politicianInfos = new List<string>();
            List<string> politicianContactInfos = new List<string>();
            List<ExtractedValues> ministers = new List<ExtractedValues>();

            bool isInfoContact = false;

            var tdList = document.DocumentNode.SelectNodes("//td");
            foreach (var node in tdList)
            {
                var value = RemoveExcessWhiteSpace(node.InnerText);
                if (!value.Equals(""))
                {
                    if (!isInfoContact)
                    {
                        politicianInfos.Add(value);
                        isInfoContact = true;
                    }
                    else if (isInfoContact)
                    {
                        politicianContactInfos.Add(value);
                        isInfoContact = false;
                    }
                }
            }
            for (int i = 0; i < politicianInfos.Count; i++)
            {
                ministers.Add(ConvertToPolitician(politicianInfos[i], politicianContactInfos[i]));
            }
            return ministers;
        }

        private string RemoveExcessWhiteSpace(string value)
        {
            string newValue = value.Trim();
            newValue = Regex.Replace(value, @"\s+", " ");
            newValue = newValue.Trim();
            newValue = HttpUtility.HtmlDecode(newValue);
            return newValue;
        }

        private ExtractedValues ConvertToPolitician(string line, string contactLine)
        {
            string[] splittedLine;
            string[] splittedContactLine;
            splittedLine = line.Split(' ');
            splittedContactLine = contactLine.Split(' ');

            ExtractedValues politician = new ExtractedValues();

            var name = GetNameFromSL(splittedLine);
            politician.Title = GetTitleFromSL(splittedLine);
            politician.Firstname = name.Item1;
            politician.Lastname = name.Item2;
            politician.Party = GetPartyFromSL(splittedLine);
            politician.Contact = GetContactFromCL(splittedContactLine);
            return politician;
        }

        private ContactInfo GetContactFromCL(string[] splittedContactLine)
        {
            ContactInfo contact;
            string phone = "";
            string email = "";

            for (int i = 0; i < splittedContactLine.Length; i++)
            {
                if (char.IsDigit(splittedContactLine[i][0]) || splittedContactLine[i].Contains("+"))
                {
                    phone = phone + splittedContactLine[i];
                    if (!char.IsDigit(splittedContactLine[i+1][1]))
                    {
                        email = splittedContactLine[i + 2];
                    }
                }
            }
            contact = new ContactInfo {phone = phone, email = email };
            return contact;
        }
        private string GetTitleFromSL(string[] splittedLine)
        {
            bool loop = true;
            int count = 0;
            string ministerTitle = "";
            string temp = "";
            while (loop)
            {
                if (count > 0)
                {
                    temp = temp + " " + splittedLine[count];
                }
                else
                {
                    temp = temp + splittedLine[count];
                }
                if (char.IsUpper(splittedLine[count + 1][0]))
                {
                    ministerTitle = temp;
                    loop = false;
                }
                count++;
            }
            return ministerTitle;
        }
        private Tuple<string, string> GetNameFromSL(string[] splittedLine)
        {
            string firstname = "";
            string lastname = "";
            for (int i = 0; i < splittedLine.Length; i++)
            {
                if (splittedLine[i].Contains("("))
                {
                    break;
                }
                if (char.IsUpper(splittedLine[i + 1][0]))
                {
                    if (splittedLine[i + 2].Contains("("))
                    {
                        lastname = splittedLine[i + 1];
                        break;
                    }
                    firstname = firstname + splittedLine[i + 1];
                }
            }
            return Tuple.Create(firstname, lastname);
        }
        private string GetPartyFromSL(string[] splittedLine)
        {
            string party = "";
            for (int i = 0; i < splittedLine.Length; i++)
            {
                if (splittedLine[i].Contains("("))
                {
                    party = splittedLine[i + 1];
                    break;
                }
            }
            return party;
        }
    }
}
