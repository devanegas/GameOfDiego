using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfDiego
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var name = new Name();
            name.name = "Davanegas8";
            var contents = await GetToken(name);

            if (contents == "Error")
            {
                Console.WriteLine("Error");
            }
            else
            {
                var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromSeconds(1);
                var token = JsonConvert.DeserializeObject<Token>(contents);
                var updateReq = new UpdateRequest();
                updateReq.token = token.token;
                updateReq.clientStatus = "waiting";
                var response = "";

                var timer = new System.Threading.Timer(async (e) =>
                {
                    response = await PostToken(updateReq);
                    Console.WriteLine(response);
                    Console.WriteLine("Solving the board....");
                    Thread.Sleep(2000);
                }, null, startTimeSpan, periodTimeSpan);

                Console.ReadLine();
                
            }

        }

        private static async Task<string> GetToken(Name name)
        {
            
            string json = JsonConvert.SerializeObject(name);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    "http://daybellphotography.com/register",
                     new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    return contents;
                }
                else
                {
                    return "Error";
                }
            }
        }

        private static async Task<string> PostToken(UpdateRequest updateReq)
        {
            string json = JsonConvert.SerializeObject(updateReq);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    "http://daybellphotography.com/update",
                     new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    return contents;
                }
                else
                {
                    return "Error updating";
                }
            }
        }
    }

    public class Name
    {
        public string name { get; set; }
    }

    public class Token
    {
        public string name { get; set; }
        public string token { get; set; }
    }

    public class UpdateRequest
    {
        public string token { get; set; }
        public string clientStatus { get; set; }
    }
}
