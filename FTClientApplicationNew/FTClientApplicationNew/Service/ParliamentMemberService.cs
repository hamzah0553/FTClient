using System.Linq;

using OdataFtClientConsumer.System;
using OdataFtClientConsumer.System.Model;

namespace OdataFtClientConsumer.Service
{
    class ParliamentMemberService
    {
        FTContext db = new FTContext();
        public ParliamentMember GetParliamentMember(int id, int parliamentId)
        {
            ParliamentMember parliamentMember;
            var result = from pmMember in db.ParliamentMembers
                         where pmMember.PoliticianId == id && pmMember.ParliamentId == parliamentId
                         select pmMember;
            if (result.Any())
            {
                parliamentMember = result.First();
                return parliamentMember;
            }
            else
            {
                return null;
            }
        }

        public bool CheckPMExist(int id, int parliamentId)
        {
            var newPol = GetParliamentMember(id, parliamentId);
            if (newPol == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreatePM(ParliamentMember pm)
        {
            if (!CheckPMExist(pm.PoliticianId, pm.ParliamentId))
            {
                db.ParliamentMembers.InsertOnSubmit(pm);
                db.SubmitChanges();
            }

        }
    }
}
