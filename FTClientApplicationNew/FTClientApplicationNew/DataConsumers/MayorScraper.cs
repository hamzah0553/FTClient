using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FTClientApplication.ViewModel.Dk;

namespace FTClientApplication.OdataConsumer
{
    class MayorScraper
    {
        MayorVM mayorVM = new MayorVM();

        HtmlWeb web = new HtmlWeb();
        List<string> urlList = new List<string>();
        List<HtmlDocument> documents = new List<HtmlDocument>();
        List<string> tempMayors = new List<string>();
        List<string> municipalities = new List<string>();
        List<string> parties = new List<string>();
        List<CustomMayor> mayors = new List<CustomMayor>();


        public MayorScraper()
        {
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?po=126739&st=1&cu=1#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?po=126739&st=1&cu=1&epslanguage=da&=&page=1#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=2#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=3#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=4#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=5#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=6#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=7#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=8#results");
            urlList.Add("http://www.danskekommuner.dk/Borgmesterfakta/Sogning/?epslanguage=da&po=126739&st=1&cu=1&=&page=9#results");
            for (int i = 0; i < urlList.Count; i++)
            {
                documents.Add(web.Load(urlList[i]));
            }
            SaveAllMayors();
        }

        public List<CustomMayor> GetCustomMayors()
        {
            return mayors;
        }

        private void SaveAllMayors()
        {
            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < documents.Count; i++)
            {
                SaveMayors(i);
            }
            for (int i = 0; i < tempMayors.Count; i++)
            {
                string[] splittedItem = SplitResult(tempMayors[i]);
                splittedItem[2] = RemoveBrackets(splittedItem[2]);
                CustomMayor mayor = new CustomMayor();
                mayor.Firstname = splittedItem[0];
                mayor.Lastname = splittedItem[1];
                mayor.Municipality = municipalities[i];
                mayor.Party = mayorVM.GetPartyFromInitial(splittedItem[2]);
                mayors.Add(mayor);
            }
            stopWatch.Stop();
            Debug.WriteLine(stopWatch.ElapsedMilliseconds);
        }

        private void SaveMayors(object data)
        {
            int index = (int)data;
            bool isMayor = false;

            var spanList = documents[index].DocumentNode.SelectNodes("//span[@class='" + "link" + "']");
            foreach (var item in spanList)
            {
                string value = item.InnerText;
                value = RemoveExcessWhiteSpace(value);
                if (isMayor == false)
                {
                    municipalities.Add(value);
                    isMayor = true;
                }
                else
                {
                    tempMayors.Add(value);
                    isMayor = false;
                }
            }
        }

        private string[] SplitResult(string result)
        {
            string[] splittedResult = result.Split(' ');
            if (splittedResult.Length == 7)
            {
                splittedResult[0] = splittedResult[0] + " " + splittedResult[1];
                for (int i = 1; i < splittedResult.Length; i++)
                {
                    if (i+1 == splittedResult.Length)
                    {
                        break;
                    }
                    splittedResult[i] = splittedResult[i + 1];
                }
            }
            return splittedResult;
        }

        private string RemoveBrackets(string value)
        {
            return Regex.Replace(value, "[^0-9a-zA-Z]+", "", RegexOptions.Compiled);
        }
        private string RemoveExcessWhiteSpace(string value)
        {
            string newValue = value.Trim();
            newValue = Regex.Replace(value, @"\s+", " ", RegexOptions.Compiled);
            newValue = newValue.Trim();
            newValue = HttpUtility.HtmlDecode(newValue);
            return newValue;
        }
    }
}

