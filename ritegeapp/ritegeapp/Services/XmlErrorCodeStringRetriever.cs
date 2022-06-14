using RitegeDomain.DTO;
using RitegeDomain.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace ritegeapp.Services
{
    public class XmlEventCodeStringRetriever
    {
        Assembly assembly = typeof(App).GetTypeInfo().Assembly;

        string xmlEmbeddedResourcePath = "ritegeapp.Resources.LangFrensh.xml";
        public XmlEventCodeStringRetriever()
        {

        }
        //public bool EventCodeIsDangerous()
        public EventDTO GetErrorCodeString(EventDTO parkingEvent)
        {
            using (Stream stream = assembly.GetManifestResourceStream(xmlEmbeddedResourcePath))
            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Entry" && reader.GetAttribute("key") == "CodeEvent"+parkingEvent.CodeEvent.ToString())
                        {
                            parkingEvent.DescriptionEvent=reader.ReadInnerXml();
                            return parkingEvent;
                        }
                    }
                }

            }
            parkingEvent.DescriptionEvent="Pas de description";
            return parkingEvent;
        }
     
    }
}
