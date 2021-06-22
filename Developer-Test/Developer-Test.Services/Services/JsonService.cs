using Developer_Test.Services.Models;
using Developer_Test.Services.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Developer_Test.Services.Services
{
    public class JsonService : IJsonService
    {
        public async Task<List<Voucher>> LoadVouchersAsync()
        {
            /* 
               While I am deserializing a JSON file here for discount codes, normally I would use a
               database with a discount codes table and use Entity Framework to fetch the vouchers
               directly from there, however this is unavailable due to hosting a local db and that
               would render it unusable on another system.
            */
            try
            {
                var path = Directory.GetCurrentDirectory();
                var parentPath = Directory.GetParent(path).FullName;

                var jsonPath = parentPath + "/Developer-Test.Services/External/vouchers.json";
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    string json = await r.ReadToEndAsync();
                    List<Voucher> items = JsonConvert.DeserializeObject<List<Voucher>>(json);

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
