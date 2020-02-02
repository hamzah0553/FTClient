using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;

namespace FTClientApplication.ViewModel.Dk
{
    class SelectionVM: IExcelAdapter
    {
        FTDatabaseEntities entities = new FTDatabaseEntities();

        //Returns list of selections
        public List<Selection> GetSelections()
        {
            return entities.Selection.ToList();
        }
        //Returns list of parliaments
        public List<Parliament> GetParliaments()
        {
            return entities.Parliament.ToList();
        }
        //Returns selection members depending on parliament and selections
        public List<CustomSelectionMember> GetSelectionMembers(string selectionName)
        {
            List<CustomSelectionMember> members = new List<CustomSelectionMember>();
            List<Selection_member> tempList = entities.Selection_member.Where(s => s.Selection.name.Equals(selectionName) &&
                s.ParliamentMember.Parliament.startYear == 2019).ToList();
            foreach (var item in tempList)
            {
                CustomSelectionMember customSelectionMember = new CustomSelectionMember()
                {
                    ParliamentMemberId = item.parliamentMemberId,
                    Firstname = item.ParliamentMember.Politician.firstname,
                    Lastname = item.ParliamentMember.Politician.lastname,
                    Party = item.ParliamentMember.Politician.Party.name,
                    Selection = selectionName
                };
                members.Add(customSelectionMember);
            }
            return members;
        }

        public List<List<string>> ConvertData()
        {
            List<Selection> selections = GetSelections();
            List<CustomSelectionMember> selectionMembers = new List<CustomSelectionMember>();
            List<List<string>> lists = new List<List<string>>();
            int count = 0;
            for (int i = 0; i < selections.Count; i++)
            {
                Debug.WriteLine(selections[i]);
                selectionMembers = GetSelectionMembers(selections[i].name);
                for (int j = 0; j < selectionMembers.Count; j++)
                {
                    Debug.WriteLine(selectionMembers[j].Firstname + " " + selectionMembers[j].Lastname);
                    lists.Add(new List<string>());
                    lists[count].Add(selectionMembers[j].Firstname);
                    lists[count].Add(selectionMembers[j].Lastname);
                    lists[count].Add(selectionMembers[j].Party);
                    lists[count].Add(selectionMembers[j].Selection);
                    count++;
                }
            }
            return lists;
        }

        public List<string> GetColumnNames()
        {
            List<string> list = new List<string>();
            list.Add("Fornavn");
            list.Add("Efternavn");
            list.Add("Parti");
            list.Add("Udvalg");
            return list;
        }
    }

    public class CustomSelectionMember
    {
        public int ParliamentMemberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Party { get; set; }
        public string Selection { get; set; }
    }
}
