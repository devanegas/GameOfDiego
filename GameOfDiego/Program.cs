using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDiego
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var obj = new Name();
            obj.name = "Diego3";
            string json = JsonConvert.SerializeObject(obj);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    "http://daybellphotography.com/register",
                     new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(contents);
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }

        }



    }

    public class Name
    {
        public string name { get; set; }
    }
}
