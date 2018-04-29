using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace DailyUpdate_v3._1
{
    // https://www.youtube.com/watch?v=zv7ZkQwDEqM



    public class ClassSerializer
    {
        public static string filename = "sfdu.xml";
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Type3 { get; set; }
        public string Type4 { get; set; }
        public string Type5 { get; set; }

        public static ClassSerializer GetSettings()
        {

            
            ClassSerializer settings = null;


            if (File.Exists(filename))
            {

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(ClassSerializer));
                    settings = (ClassSerializer)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else settings = new ClassSerializer();

            return settings;
            
        }
        public void Save()
        {
            
            if (File.Exists(filename)) File.Delete(filename);
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xser = new XmlSerializer(typeof(ClassSerializer));
                xser.Serialize(fs, this);
                fs.Close();
               

            }

        }
        
    }
}
