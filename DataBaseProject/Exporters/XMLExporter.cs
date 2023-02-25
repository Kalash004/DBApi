using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.Exporters
{
    internal class XMLExporter : AbstractExporter, IDataExporter
    {
        public void Export(string filename)
        {
           // get object, and create xml from it
           var users = GetData();
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<User>));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, users);
                    Console.WriteLine(sww.ToString());
                }
            }
        }
    }
}
