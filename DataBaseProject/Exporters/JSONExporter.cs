using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;
using Newtonsoft.Json;

namespace DataBaseProject.Exporters
{
    internal class JSONExporter : AbstractExporter, IDataExporter
    {
        public void Export(string filename)
        {
            var users = GetData();
            Console.WriteLine(JsonConvert.SerializeObject(users));
        }
    }
}
