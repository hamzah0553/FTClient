using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTClientApplication.Model.OdataModels
{
    public class Value
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Gruppenavnkort { get; set; }
        public string Navn { get; set; }
        public string Biografi { get; set; }
        public string PeriodeId { get; set; }
        public string Opdateringsdato { get; set; }
        public string Startdato { get; set; }
        public string Slutdato { get; set; }
    }
}
