﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTClientApplication.Model;

namespace FTClientApplication.ViewModel.Dk
{
    class SelectionVM
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
        public List<CustomSelectionMember> GetSelectionMembers(string selectionName, int startYear)
        {
            List<CustomSelectionMember> members = new List<CustomSelectionMember>();
            List<Selection_member> tempList = entities.Selection_member.Where(s => s.Selection.name.Equals(selectionName) &&
                s.ParliamentMember.Parliament.startYear == startYear).ToList();
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
