using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfDiego
{

    public class Program
    {
        public const string URL = "http://localhost";
        static async Task Main(string[] args)
        {
            var name = GetRandomName();

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
                var stop = false;
                updateReq.token = token.token;
                updateReq.clientStatus = "waiting";
                var response = "";
                dynamic json;

                var timer = new Timer(async (e) =>
                {
                    response = await PostToken(updateReq);
                    json = JsonConvert.DeserializeObject(response);
                    if (json.seedBoard.ToString() == "" || stop == true)
                    {
                    }
                    else
                    {
                        stop = true;
                        SolveBoard(json.seedBoard.ToString(), Int32.Parse(json.generationsToCompute.ToString()), updateReq.token);
                    }

                }, null, startTimeSpan, periodTimeSpan);

                Console.ReadLine();

            }

        }

        private static Name GetRandomName()
        {
            var name = new Name();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            name.name = "Diego#" + randomString;

            return name;
        }

        private static async Task<string> GetToken(Name name)
        {

            string json = JsonConvert.SerializeObject(name);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    URL + "/register",
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
                    URL + "/update",
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

        private static async Task<string> PostBoard(SubmissionJson submissionJson)
        {
            string json = JsonConvert.SerializeObject(submissionJson);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    URL + "/update",
                     new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    return contents;
                }
                else
                {
                    return "Error posting";
                }
            }
        }

        private static async Task SolveBoard(string board, int nGenerations, string userToken)
        {
            var cells = JsonConvert.DeserializeObject<List<Cell>>(board);
            cells.ForEach(c => c.IsAlive = true);
            var service = new SolverService();

            for (int i = 0; i < nGenerations; i++)
            {
                cells = service.SolveBoard(cells);
            }


            var resultBoard = new List<ResultBoard>();
            foreach(var cell in cells)
            { 
                resultBoard.Add(new ResultBoard { x = cell.x, y = cell.y });
            }

            var result = new SubmissionJson { token = userToken, GenerationsComputed = nGenerations, ResultBoard = resultBoard };
            PostBoard(result);

        }
    }

}
