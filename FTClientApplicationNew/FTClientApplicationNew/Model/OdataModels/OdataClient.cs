using System;
using System.Net;
using System.IO;

using FTClientApplication.Model;

using Newtonsoft.Json;

namespace FTClientApplication.Model.OdataModels
{
    class OdataClient
    {
        public string text;
        public string url;
        HttpWebRequest request;
        HttpWebResponse response;
        Stream responseStream;
        public OdataClient(string url)
        {
            this.url = url;
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            text = reader.ReadToEnd();
        }
        public Odata fetchData()
        {
            Odata page1 = JsonConvert.DeserializeObject<Odata>(text);
            return page1;
        }
        public Stream GetResponseStream()
        {
            return responseStream;
        }
    }
}
