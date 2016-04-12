using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailErrorService
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckErrorFile cf = new CheckErrorFile();
            cf.CheckErrorFloder();

            //DebugRepairResolutionList db = new DebugRepairResolutionList(); 
            ////EmailEnvironment ee = new EmailEnvironment();
            //try
            //{ 
            //    EmailService em = new EmailService();
            //    em.SendEmailMethod("test", "test body");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error:\n"+e);
            //}


        }
    }
}
