using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace EmailErrorService
{
    class DebugRepairResolutionList
    {

        //call the EmailService
        EmailService es = new EmailService();
        //private instance variables for emailService
        private string msgSubject = "";
        private string msgBody = "";

        //define the errorMsg variable
        public string errorMsg = "";
        //variables
        string errorFileLocation = System.AppDomain.CurrentDomain.BaseDirectory + @"errorXMLFiles";


        //constructor
        public DebugRepairResolutionList()
        {
            //ensure the errorXMLFiles folder exists
            if (!Directory.Exists(errorFileLocation))
            {
                Console.WriteLine("Cannot find the <errorXMLFiles> folder.\n");
            }
            else
            {
                //if the folder exists, then verify the files that inside the folder
                Console.WriteLine("Found the <errorXMLFiles> folder.\n");

                Check();

            }
        }

        public void Check()
        {
            //find all xml files
            string[] fileNames = Directory.GetFiles(errorFileLocation, "*.xml");

            for (int i = 0; i < fileNames.Length; i++)
            {

                if (fileNames[i].Contains("RESOLUTION"))
                {
                    //declare a numberOfRecords variable
                    int numberOfRecords = 0;
                    string repairID = null;
                    //declare a repairIDCount variable
                    int repairIDCount = 0;

                    //verify the mandatory fields
                    XmlReader reader = XmlReader.Create(fileNames[i]);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileNames[i]);

                    string repairResolutionList;
                    string repairResolutionCode;
                    string repairIDss="";
                    string repairIDss2 = "";
                    string repairIDss3="";

                    //verify the repairResolutionList tag
                    #region
                    foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    {
                        try
                        {
                            repairResolutionList = e.SelectSingleNode("*[local-name() = 'repairResolutionList']").InnerText;
                            
                            //verify the content
                            if (repairResolutionList == "")
                            {
                                repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                                errorMsg += "Empty content at the <repairResolutionList> tag. Failed!!! repairID: " + repairIDss + "\n\n";
                            }

                           
                        }
                        catch (Exception)
                        {
                            repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                            //Console.WriteLine(repairIDss);
                            errorMsg += "Missing the <repairResolutionList> tag. Failed!!! repairID: " + repairIDss + "\n\n";
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
                                errorMsg += "Empty content at the <repairResolutionCode> tag. Failed!!! repairID: " + repairIDss2 + "\n\n";
                            }
                        }
                        catch (Exception)
                        { 
                            errorMsg += "Missing the <repairResolutionCode> tag. Failed!!! repairID: " + repairIDss3 + "\n\n";
                        }
                    }
                    #endregion 
                    //verify the repairResolutionCode taggggggggggggggggggggggggggggg
                    //#region
                    //foreach (XmlElement e in doc.SelectNodes("//*[local-name() = 'repairOrderResolution']"))
                    //{
                    //    try
                    //    {
                    //        repairResolutionList = e.SelectSingleNode("*[local-name() = 'repairResolutionList']").InnerText;
                    //        repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                    //        //verify the content
                    //        if (repairResolutionList == "")
                    //        {
                    //            errorMsg += "Empty content at the <repairResolutionList> tag. Failed!!! repairID: " + repairIDss + "\n\n";
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {
                    //        repairIDss = e.SelectSingleNode("*[local-name() = 'repairID']").InnerText;
                    //        errorMsg += "Missing the <repairResolutionList> tag. Failed!!! repairID: " + repairIDss + "\n\n";
                    //    }
                    //}
                    //#endregion 

                    //verify the number of records
                    //#region


                    //while (reader.Read())
                    //{ 
                    //    //find the specific tag (ervmt:repairID) of the resolution XML file

                    //    if (reader.NodeType == XmlNodeType.Element && reader.Name == "ervmt:repairID")
                    //    {
                    //        //assign the specific element value
                    //        repairID = reader.ReadString();

                    //        ////display the repairID
                    //        Console.WriteLine("The repairID: {0}.", repairID); 

                    //    }
                    //}
                    //reader.Close(); 

                    //#endregion


                    //send the error message to email
                    try
                    {
                        if (errorMsg != "" || errorMsg == null)
                        {
                            Console.WriteLine("Errors at the file: {0}!!! \nSee Details at below: \n{1}", fileNames[i].Remove(0, errorFileLocation.Length + 1), errorMsg);
                            //es.SendEmailMethod("Errors at the file: " + fileNames[i].Remove(0, errorFileLocation.Length + 1), "Errors at the file: " + fileNames[i] + "\n\nSee Details at below: \n" + errorMsg);
                        }


                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            
        }
    }
}
