using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTClientApplication.Model.OdataModels
{
    class ExtractedValues
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Party { get; set; }
        public string PartyShortname { get; set; }
        public string Title { get; set; }
        public ContactInfo Contact { get; set; }

    }
}
