using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EmailErrorService
{
    class CollectRepairID
    {
        //constructor
        public CollectRepairID()
        {
            
        }

        //Collect all the repair id from the XML file
        public void CollectAllId(string fileName)
        {
            //verify the mandatory fields
            XmlReader reader = XmlReader.Create(fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            //define all the necessary tags
            string repairID2;
            string errorM = "Repair ID list in XML file:\n";

            //check the repairId tag
            #region
            foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
            {
                try
                {
                    repairID2 = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                    Console.WriteLine(repairID2);
                    errorM += repairID2 + ",";
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                    errorM += "Missing Repair ID" + ",";
                }
            }
            #endregion

            Console.WriteLine(errorM.Remove(errorM.Length-1));
        }
    }
}
