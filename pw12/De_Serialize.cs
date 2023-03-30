using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace pw12
{
    internal class De_Serialize
    {
        public static void Serialize(List<Records> se_records)
        {
            string json = JsonConvert.SerializeObject(se_records);
            File.WriteAllText(@"D:\Records.json", json);
        }
        public static List<Records> Deserialize()
        {
            if (File.Exists(@"D:\Records.json"))
            {
                string de_ison = File.ReadAllText(@"D:\Records.json");
                List<Records> de_records = JsonConvert.DeserializeObject<List<Records>>(de_ison);
                return de_records;
            }
            else
            {
                List<Records> el_de_records = new List<Records>();
                string json = JsonConvert.SerializeObject(el_de_records);
                File.WriteAllText(@"D:\Records.json", json);
                return el_de_records;
            }
        }
    }
}

