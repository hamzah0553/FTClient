using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using FTClientApplication.Model;
using FTClientApplication.Model.OdataModels;

namespace FTClientApplication.OdataConsumer
{
    class BiographyReader
    {
        private XmlReader reader;
        ExtractedValues politicianValues;
        public BiographyReader(string biography)
        {
            reader = XmlReader.Create(new StringReader(biography));

        }
        
        public void ReadBiography()
        {
            string firstname = "";
            string lastname = "";
            string party = "";
            string formattedTitle = "";
            string partyShortname = "";
            string phone = "";
            string email = "";
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("firstname"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    firstname = element.Value;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("lastname"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    lastname = element.Value;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("party"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    party = element.Value;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("formattedTitle"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    formattedTitle = element.Value;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("partyShortname"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    partyShortname = element.Value;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("phoneFolketinget"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    phone = element.Value;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("email"))
                {
                    XElement element = XNode.ReadFrom(reader) as XElement;
                    email = element.Value;
                }
            }
            ContactInfo contact = new ContactInfo();
            contact.email = email;
            contact.phone = phone;
            CombineToPolitician(firstname, lastname, party, formattedTitle, partyShortname, contact);
        }

        private ExtractedValues CombineToPolitician(string firstname, string lastname, string party, string formattedTitle, string partyShortname, ContactInfo contact)
        {
            politicianValues = new ExtractedValues {
                Firstname = firstname,
                Lastname = lastname, Party = party,
                Title = formattedTitle,
                PartyShortname = partyShortname,
                Contact = contact
            };
            return politicianValues;
        }

        public ExtractedValues GetPolitician()
        {
            return politicianValues;
        }
    }
}
