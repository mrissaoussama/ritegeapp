using RitegeDomain.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ritegeapp.Services
{
    public class XmlErrorCodeStringRetriever
    {
        string xmlEmbeddedResourcePath = "ritegeapp.Resources.LangFrensh.xml";
        public XmlErrorCodeStringRetriever()
        {

        }
        //public bool EventCodeIsDangerous()
        public ParkingEvent GetErrorCodeStringAndType(ParkingEvent parkingEvent)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(xmlEmbeddedResourcePath))
            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Entry" && reader.GetAttribute("key") == parkingEvent.TypeEvent)
                        {
                                         if (Enum.IsDefined(typeof(AlertCodes), parkingEvent.TypeEvent))
                            {
                                parkingEvent.TypeEvent = "Alert";
                            }
                            else parkingEvent.TypeEvent = "Evennement";

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
