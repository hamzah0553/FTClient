using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using FTClientApplication.Model;

namespace FTClientApplication.OdataConsumer
{
    public class SelectionScraper
    {
        HtmlWeb web = new HtmlWeb();
        List<UrlItem> urlList = new List<UrlItem>();
        List<HtmlDocument> documents = new List<HtmlDocument>();

        public SelectionScraper()
        {
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/beu/medlemsoversigt", 1));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/eru/medlemsoversigt", 2));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/fiu/medlemsoversigt", 4));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/fou/medlemsoversigt", 5));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/kef/medlemsoversigt", 3));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/sau/medlemsoversigt", 7));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/ufu/medlemsoversigt", 6));
            for (int i = 0; i < urlList.Count; i++)
            {
                documents.Add(web.Load(urlList[i].Url));
            }
        }

        public void GetAllMembers()
        {
            //uses same amount of threads as documents to increase performance
            List<Thread> listOfThreads = new List<Thread>();
            for (int i = 0; i < documents.Count; i++)
            {
                //Thread thread = new Thread(GetSelectionMembers);
                //thread.Start(i);
                //listOfThreads.Add(thread);
            }
            Thread thread = new Thread(CreateSelectionMembers);

            thread.Start(0);
        }

        private void CreateSelectionMembers(Object data)
        {
            string pattern = "^\\d+$";
            bool shouldInclude = true;
            var tdList = documents[(int)data].DocumentNode.SelectNodes("//td");
            foreach (var node in tdList)
            {
                var value = RemoveExcessWhiteSpace(node.InnerText);
                if (!value.Equals(""))
                {
                    if (!Regex.IsMatch(value, pattern))
                    {
                        if (shouldInclude)
                        {
                            string[] fullname = SplitName(value);
                            //Debug.WriteLine(fullname[0] + " " + fullname[1]);
                            shouldInclude = false;
                        }
                        else
                        {
                            shouldInclude = true;
                        }
                    }
                }
            }
        }
        private string[] SplitName(string value)
        {
            string[] splittedName = value.Split(' ');
            string firstname = "";
            string lastname = "";
            for (int i = 0; i < splittedName.Length; i++)
            {
                if (splittedName.Length > 2)
                {
                    if (splittedName.Length > i)
                    {
                        firstname = splittedName[i];
                    }
                    lastname = splittedName[splittedName.Length-1];
                }
                if (splittedName.Length < 3)
                {
                    firstname = splittedName[0];
                    lastname = splittedName[1];
                }
            }
            Debug.WriteLine(firstname + " " + lastname);
            return new string[] { firstname, lastname };
        }
        private string RemoveExcessWhiteSpace(string value)
        {
            string newValue = value.Trim();
            newValue = Regex.Replace(value, @"\s+", " ");
            newValue = newValue.Trim();
            newValue = HttpUtility.HtmlDecode(newValue);
            return newValue;
        }
    }

    public class UrlItem
    {
        public UrlItem(string url, int selectionIndex)
        {
            Url = url;
            SelectionIndex = selectionIndex;
        }
        public string Url { get; set; }
        public int SelectionIndex { get; set; }
       
    }

}
