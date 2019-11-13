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

namespace FTClientApplication.OdataConsumer
{
    public class SelectionScraper
    {
        HtmlWeb web = new HtmlWeb();
        List<string> urlList = new List<string>();
        List<HtmlDocument> documents = new List<HtmlDocument>();

        public SelectionScraper()
        {
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/beu/medlemsoversigt");
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/eru/medlemsoversigt");
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/fiu/medlemsoversigt");
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/fou/medlemsoversigt");
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/kef/medlemsoversigt");
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/sau/medlemsoversigt");
            urlList.Add("https://www.ft.dk/da/udvalg/udvalgene/ufu/medlemsoversigt");
            for (int i = 0; i < urlList.Count; i++)
            {
                documents.Add(web.Load(urlList[i]));
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
            Thread thread = new Thread(GetSelectionMembers);
            thread.Start(0);
        }

        private void GetSelectionMembers(Object data)
        {
            string pattern = "^\\d+$";
            var tdList = documents[(int)data].DocumentNode.SelectNodes("//td");
            foreach (var node in tdList)
            {
                var value = RemoveExcessWhiteSpace(node.InnerText);
                if (!value.Equals(""))
                {
                    if (!Regex.IsMatch(value, pattern))
                    {
                        Debug.WriteLine(value);
                    }
                }
            }
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
}
