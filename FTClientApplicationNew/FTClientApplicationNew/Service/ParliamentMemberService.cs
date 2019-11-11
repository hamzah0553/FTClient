using System.Linq;

using FTClientApplication.Model;

namespace FTClientApplication.Service
{
    class ParliamentMemberService
    {
        FTDatabaseEntities db = new FTDatabaseEntities();
        public ParliamentMember GetParliamentMember(int id, int parliamentId)
        {
            ParliamentMember parliamentMember;
            var result = from pmMember in db.ParliamentMember
                         where pmMember.politicianId == id && pmMember.parliamentId == parliamentId
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
            if (!CheckPMExist(pm.politicianId, pm.parliamentId))
            {
                db.ParliamentMember.Add(pm);
                db.SaveChanges();
            }

        }
    }
}
