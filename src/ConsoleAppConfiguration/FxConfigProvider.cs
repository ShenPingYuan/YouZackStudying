using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleAppConfiguration
{
    internal class FxConfigProvider : FileConfigurationProvider
    {
        public FxConfigProvider(FxConfigSource src) : base(src) { }
        public override void Load(Stream stream)
        {
            var data=new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);
            var xmlNodes = xmlDoc.SelectNodes("/configuration/connectionStrings/add");
            foreach (XmlNode? xmlNode in xmlNodes.Cast<XmlNode>())
            {
                if (xmlNode != null)
                {
                    var name= xmlNode.Attributes["name"]?.Value;
                    string connectionString = xmlNode.Attributes["connectionString"]?.Value;
                }
            }

            //...
            data["connnectionString"] = "fsadlkga;sldfkj";
            Data = data;
        }
    }
}
