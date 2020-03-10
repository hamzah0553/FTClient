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
    //Note for this class
    //Every selection needs to be manually added to the database 
    //If the selection doesnt exist the program will crash
    public class SelectionScraper
    {
        int parliamentId;
        HtmlWeb web = new HtmlWeb();
        List<UrlItem> urlList = new List<UrlItem>();
        List<HtmlDocument> documents = new List<HtmlDocument>();

        public SelectionScraper()
        {
            this.parliamentId = 1;
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/beu/medlemsoversigt", 1));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/eru/medlemsoversigt", 2));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/fiu/medlemsoversigt", 4));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/fou/medlemsoversigt", 5));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/kef/medlemsoversigt", 3));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/sau/medlemsoversigt", 7));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/ufu/medlemsoversigt", 6));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/euu/medlemsoversigt", 8));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/mof/medlemsoversigt", 9));
            urlList.Add(new UrlItem("https://www.ft.dk/da/udvalg/udvalgene/reu/medlemsoversigt", 10));

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
                Thread thread = new Thread(CreateSelectionMembers);
                thread.Start(new ThreadData(urlList[i].SelectionIndex, i));
                listOfThreads.Add(thread);
            }
        }

        private void CreateSelectionMembers(Object data)
        {
            ThreadData indexes = (ThreadData)data;
            string pattern = "^\\d+$";
            bool shouldInclude = true;
            var tdList = documents[indexes.Index].DocumentNode.SelectNodes("//td");
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
                            string firstname = fullname[0];
                            string lastname = fullname[1];
                            Selection_member selection_Member = new Selection_member();
                            //saving the retrieved data to db
                            using (var context = new FTDatabaseEntities())
                            {
                                if (context.Politician.Where(p => p.firstname.Equals(firstname) && p.lastname.Equals(lastname)).Any())
                                {
                                    Debug.WriteLine(firstname + " " + lastname + " " + indexes.SelectionIndex);
                                    selection_Member.parliamentMemberId =
                                        context.ParliamentMember.Where(p => p.Politician.firstname.Equals(firstname) &&
                                        p.Politician.lastname.Equals(lastname) && p.Parliament.id == parliamentId).Single().id;
                                    selection_Member.selectionId = indexes.SelectionIndex;
                                    if (!SelectionMemberExist(selection_Member, context))
                                    {
                                        context.Selection_member.Add(selection_Member);

                                    }
                                    context.SaveChanges();
                                }
                            }
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
        private bool SelectionMemberExist(Selection_member member, FTDatabaseEntities context)
        {
            if (context.Selection_member.Where(s => s.selectionId == member.selectionId && s.parliamentMemberId == member.parliamentMemberId).Any())
            {
                return true;
            }
            else { return false; }
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
                    if (splittedName.Length-1 > i)
                    {
                        if (firstname.Equals(""))
                        {
                            firstname = firstname + splittedName[i];
                        }
                        else
                        {
                            firstname = firstname + " " + splittedName[i];
                        }
                    }
                    lastname = splittedName[splittedName.Length-1];
                }
                if (splittedName.Length < 3)
                {
                    firstname = splittedName[0];
                    lastname = splittedName[1];
                }
            }
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

    public class ThreadData
    {
        public ThreadData(int selectionIndex, int index)
        {
            Index = index;
            SelectionIndex = selectionIndex;
        }
        public int SelectionIndex { get; set; }
        public int Index { get; set; }

    }

}
