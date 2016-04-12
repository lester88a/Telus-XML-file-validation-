using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailErrorService
{
    class EmailService
    { 
        //default setting of the email environment
        private string _recipient;
        private string _sender;
        private string _smtpServer;
        private int _smtpPort;
        private string _senderUserName;
        private string _senderUserPass;

        public EmailService()
        {
            //call the EmailEnvironment, use the values from the emailEnvironment.txt
            EmailEnvironment ee = new EmailEnvironment();
            this._recipient = ee.recipient;
            this._sender = ee.sender;
            this._smtpServer = ee.smtpServer;
            this._smtpPort = ee.smtpPort;
            this._senderUserName = ee.senderUserName;
            this._senderUserPass = ee.senderUserPass;
            //display the values of all variables
            Console.WriteLine("\n\nThis info from EmailService class:");
            Console.WriteLine("The recipient value: {0}", this._recipient);
            Console.WriteLine("The sender value: {0}", this._sender);
            Console.WriteLine("The smtpServer value: {0}", this._smtpServer);
            Console.WriteLine("The smtpPort value: {0}", this._smtpPort);
            Console.WriteLine("The senderUserName value: {0}", this._senderUserName);
            Console.WriteLine("The senderUserPass value: {0}", this._senderUserPass);
        }
        // "rtsissue@futuretel-service.com"
        //"lester.xu@futuretel-service.com"
        MailMessage message = new MailMessage();

        public void SendEmailMethod(string errorSubject, string errorBody)
        {
            message.To.Add(_recipient);
            message.Subject = errorSubject;
            message.From = new MailAddress(_sender);
            message.Body = errorBody;
            SmtpClient smtp = new SmtpClient(_smtpServer, _smtpPort);

            //configure the client 
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(_senderUserName, _senderUserPass);


            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }
    }
}
