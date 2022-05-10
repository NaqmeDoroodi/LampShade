using System;
using System.Collections.Generic;
using System.Linq;
using IPE.SmsIrClient;
using Microsoft.Extensions.Configuration;

namespace Framework.Application.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public  void Send(string number, string message)
        {
            SmsIr smsIr = new(_configuration.GetSection("SmsSecrets")["ApiKey"]);
            var line = smsIr.GetLinesAsync().Result.Data.Last();

            try
            {
                var bulkSendResult =  smsIr.BulkSend(line, message, new List<string> { number }.ToArray());

                if (bulkSendResult.Status != 0) return;

                line = smsIr.GetLinesAsync().Result.Data.First();
                bulkSendResult =  smsIr.BulkSend(line, message, new List<string> { number }.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}