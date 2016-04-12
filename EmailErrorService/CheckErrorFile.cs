using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EmailErrorService
{
    class CheckErrorFile
    {
        //call the EmailService
        EmailService es = new EmailService();
        //private instance variables for emailService
        private string msgSubject = "";
        private string msgBody = "";

        //define the errorMsg variable
        public string errorMsg = "";
        //variables
        string errorFileLocation = System.AppDomain.CurrentDomain.BaseDirectory + @"outbound";

        //constructor
        public CheckErrorFile()
        { 
            
        }

        //CheckErrorFloder
        public void CheckErrorFloder()
        { 
            //ensure the errorXMLFiles folder exists
            if (!Directory.Exists(errorFileLocation))
            {
                Console.WriteLine("Cannot find the <outbound> folder.\n");
            }
            else
            {
                //if the folder exists, then verify the files that inside the folder
                Console.WriteLine("Found the <outbound> folder.\n");
                
                verfyErrorFiles();
                
            }
        }
        //verify method
        public void verfyErrorFiles()
        {

            //find all xml files
            string[] fileNames = Directory.GetFiles(errorFileLocation, "*.xml");

            //verify which files is the resolution file
            for (int i = 0; i < fileNames.Length; i++)
            {

                if (fileNames[i].Contains("RESOLUTION"))
                {
                    //declare a numberOfRecords variable
                    int numberOfRecords = 0;
                    string repairID = null;
                    //declare a repairIDCount variable
                    int repairIDCount = 0;

                    //show the messages that found the specific files
                    Console.WriteLine("\n\n\nFound the XML file: {0}.", fileNames[i].Remove(0, errorFileLocation.Length + 1));
                    Console.WriteLine();

                    //verify the mandatory fields
                    XmlReader reader = XmlReader.Create(fileNames[i]);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileNames[i]);
                    //reader.Close();

                    //define all the necessary tags
                    string ascWarrantyCd;
                    string clientDamageCd;
                    string repairTransactionType;
                    string shippingDestinationType;
                    string repairResolutionList;
                    string repairResolutionCode;
                    string repairIDss;

                    //define local variables for collect all the repair ids
                    string repairID2;
                    string errorM = "\n\nRepair ID list in this XML file:\n";

                    //verify the ascWarrantyCd tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    {
                        try
                        {
                            ascWarrantyCd = e.SelectSingleNode("*[local-name() = 'ascWarrantyCd']").InnerText;
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;

                            //verify the content
                            if (ascWarrantyCd == "")
                            {
                                errorMsg += "Empty content at the <ascWarrantyCd> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                            }

                            //collect all the repair ids from the XML file
                            repairID2 = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            errorM += repairID2 + ",";
                        }
                        catch (Exception)
                        {
                            //Console.WriteLine("Missing the ascWarrantyCd tag. Failed!!!");
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            errorMsg += "Missing the <ascWarrantyCd> tag. Failed!!! repairID: " + repairIDss + ".\n\n";

                            //Console.WriteLine("Errors at the file: {0}!!! \nSee Details at below: \n{1}.", fileNames[i].Remove(0, errorFileLocation.Length + 1), errorMsg);

                        }
                    }
                    #endregion

                    //verify the clientDamageCd tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    {
                        try
                        {
                            clientDamageCd = e.SelectSingleNode("*[local-name() = 'clientDamageCd']").InnerText;
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;

                            if (clientDamageCd == "")
                            {
                                errorMsg += "Empty content at the <clientDamageCd> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                            }

                        }
                        catch (Exception)
                        {
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            errorMsg += "Missing the <clientDamageCd> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                        }
                    }
                    #endregion

                    //verify the repairTransactionType tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    {
                        try
                        {
                            repairTransactionType = e.SelectSingleNode("*[local-name() = 'repairTransactionType']").InnerText;
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;

                            if (repairTransactionType == "")
                            {
                                errorMsg += "Empty content at the <repairTransactionType> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                            }

                        }
                        catch (Exception)
                        {
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            errorMsg += "Missing the <repairTransactionType> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                        }
                    }
                    #endregion

                    //verify the shippingDestinationType tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    {
                        try
                        {
                            shippingDestinationType = e.SelectSingleNode("*[local-name() = 'shippingDestinationType']").InnerText;
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;

                            //verify the content
                            if (shippingDestinationType == "")
                            {
                                errorMsg += "Empty content at the <shippingDestinationType> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                            }

                        }
                        catch (Exception)
                        {
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            errorMsg += "Missing the <shippingDestinationType> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                        }
                    }
                    #endregion

                    //verify the number of records
                    #region


                    while (reader.Read())
                    {
                        //find the specific tag (ervmt:numberOfRecords) of the resolution XML file
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "ervmt:numberOfRecords")
                        {
                            //assign the specific element value and convert it to an integer
                            string messageType = reader.ReadString();
                            numberOfRecords = Convert.ToInt32(messageType);

                            ////display the number of records
                            //Console.WriteLine("The number of records: " + numberOfRecords);

                        }

                        //find the specific tag (ervmt:repairID) of the resolution XML file

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "ervmt:repairID")
                        {
                            //assign the specific element value
                            repairID = reader.ReadString();

                            ////display the repairID
                            //Console.WriteLine("The repairID: " + repairID);

                            //count the total of repair IDs
                            repairIDCount++;

                        }
                    }
                    reader.Close();

                    //display the number of records for Email
                    string showNOR = "\n\n\n";
                    showNOR += "The total of repairIDs: " + repairIDCount;

                    //compare between the numberOfRecords and  total of repairIDs
                    if (numberOfRecords != repairIDCount) //if match
                    {
                        errorMsg += "\nThe number of records: " + numberOfRecords + ", The total of repairIDs: " + repairIDCount +
                    ".\nThe number of records did not match the total of repairID. Failed!!!\n\n";

                    }

                    #endregion

                    //verify the repairResolutionList tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    {
                        try
                        {
                            repairResolutionList = e.SelectSingleNode("*[local-name() = 'repairResolutionList']").InnerText;
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            //verify the content
                            if (repairResolutionList == "")
                            {
                                errorMsg += "Empty content at the <repairResolutionList> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                            }

                        }
                        catch (Exception)
                        {
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            errorMsg += "Missing the <repairResolutionList> tag. Failed!!! repairID: " + repairIDss + ".\n\n";
                        }
                    }
                    #endregion

                    //verify the repairResolutionCode tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairResolutionList']"))
                    {
                        try
                        {
                            repairResolutionCode = e.SelectSingleNode("*[local-name() = 'repairResolutionCode']").InnerText;
                            //verify the content
                            if (repairResolutionCode == "")
                            {
                                errorMsg += "Empty content at the <repairResolutionCode> tag. Failed!!!\n\n";
                            }

                        }
                        catch (Exception)
                        {
                            errorMsg += "Missing the <repairResolutionCode> tag. Failed!!!\n\n";
                        }
                    }
                    #endregion

                    //verify the VerifySerialNumber
                    #region
                    string serialNumber;
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairEquipmentInfo']"))
                    {
                        try
                        {
                            serialNumber = e.SelectSingleNode("*[local-name() = 'serialNumber']").InnerText;

                            //verify the content
                            if (serialNumber.Length > 15)
                            {
                                errorMsg += "The length of the serialNumber is greater than 15. The serialNumber is: " + serialNumber + " (under the repairEquipmentInfo tag). Failed!!!\n\n";
                            }
                            if (serialNumber.Length < 15 && serialNumber.Length > 0)
                            {
                                errorMsg += "The length of the serialNumber is less than 15. The serialNumber is: " + serialNumber + " (under the repairEquipmentInfo tag). Failed!!!\n\n";
                            }
                        }
                        catch (Exception)
                        {
                            errorMsg += "Missing the <serialNumber> tag. Failed!!!\n\n";
                        }
                    }
                    #endregion

                    //verify the VerifySerialNumber2
                    #region
                    string serialNumber2;
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'replacementRepairEquipmentInfo']"))
                    {
                        try
                        {
                            serialNumber2 = e.SelectSingleNode("*[local-name() = 'serialNumber']").InnerText;
                            //Console.WriteLine(serialNumber2);
                            //verify the content
                            if (serialNumber2.Length > 15)
                            {

                                errorMsg += "The length of the serialNumber is greater than 15. The serialNumber is: " + serialNumber2 + " (under the replacementRepairEquipmentInfo tag). Failed!!!\n\n";
                            }
                            if (serialNumber2.Length < 15 && serialNumber2.Length > 0)
                            {
                                errorMsg += "The length of the serialNumber is less than 15. The serialNumber is: " + serialNumber2 + " (under the replacementRepairEquipmentInfo tag). Failed!!!\n\n";
                            }
                        }
                        catch (Exception)
                        {
                            errorMsg += "Missing the <serialNumber> tag. Failed!!!\n\n";
                        }
                    }
                    #endregion

                    //send the error message to email
                    try
                    {
                        if (errorMsg != "" || errorMsg == null)
                        {
                            //if the errorMsg is not empty, then send email
                            Console.WriteLine("Errors at the file: {0}!!! \nSee Details at below: \n{1}", fileNames[i].Remove(0, errorFileLocation.Length + 1), errorMsg);
                            es.SendEmailMethod("Errors at the file: " + fileNames[i].Remove(0, errorFileLocation.Length + 1), "Errors at the file: " + fileNames[i] + "\n\nSee Details at below: \n" + errorMsg + errorM.Remove(errorM.Length - 1) + showNOR);

                            //after send the email, move the error file
                            //set destination variable
                            string destinationFile = System.AppDomain.CurrentDomain.BaseDirectory + @"errorXMLFiles\" + fileNames[i].Remove(0, errorFileLocation.Length + 1);
                            //if the destination file exists, then rename it
                            if (File.Exists(destinationFile))
                            {
                                Console.WriteLine("The file: {0} already exists in destination folder.\n", fileNames[i].Remove(0, errorFileLocation.Length + 1));
                                File.Move(fileNames[i], destinationFile.Replace(".xml", "_1.xml")); //moved
                                Console.WriteLine("Renamed the error XML file as a new file.\n"); //display the renamed info
                            }
                            //else just move the error file
                            else
                            {
                                File.Move(fileNames[i], destinationFile);
                            }
                            
                            //display the successful information of moved error file
                            Console.WriteLine("The error XML file has moved successful!\n");
                        }


                    }
                    catch (Exception ee)
                    {
                        //catch the error handling information
                        Console.WriteLine("Error:\n" + ee);
                    }
                }

                //clear the errorMsg before scan the next xml file
                errorMsg = "";
            }

        }
    }
}
