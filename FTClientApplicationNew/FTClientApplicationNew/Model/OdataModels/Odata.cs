using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTClientApplication.Model.OdataModels
{
    public class Odata
    {
        [JsonProperty("Odata.metadata")]
        public string MetaData { get; set; }
        [JsonProperty("Odata.nextlink")]
        public string NextLink { get; set; }
        public List<Value> Value { get; set; }

    }
}
