using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfDiego
{

    public class Program
    {
        public const string URL = "http://daybellphotography.com";
        static async Task Main(string[] args)
        {
            var name = new Name();
            name.name = "123452";
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
                dynamic json;

                var timer = new Timer(async (e) =>
                {
                    response = await PostToken(updateReq);
                    json = JsonConvert.DeserializeObject(response);
                    await SolveBoard(json.seedBoard.ToString());
                    
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

        private static async Task SolveBoard(string board)
        {
            //Parse
            var cells = JsonConvert.DeserializeObject<List<Cell>>(board);
            cells.ForEach(c => c.IsAlive = true);
            var service = new SolverService();
            service.SolveBoard(cells);

        }

        private static int CheckNeighbors(List<Cell> startingBoard, Cell cell)
        {
            var aliveNeighbors = 0;
            List<Cell> comparisonCells = new List<Cell>{
                new Cell{ x = cell.x-1, y=cell.y-1 },
                new Cell{ x = cell.x, y=cell.y-1 },
                new Cell{ x = cell.x+1, y=cell.y-1 },
                new Cell{ x = cell.x-1, y=cell.y },
                new Cell{ x = cell.x+1, y=cell.y },
                new Cell{ x = cell.x-1, y=cell.y+1 },
                new Cell{ x = cell.x, y=cell.y+1 },
                new Cell{ x = cell.x+1, y=cell.y+1 },

            };

            for(int i=0; i<comparisonCells.Count; i++)
            {
                if (startingBoard.Contains(comparisonCells[i]))
                {
                    aliveNeighbors++;
                }
            }

            return aliveNeighbors;
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

    public class Cell 
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool IsAlive { get; set; }

        public override string ToString()
        {
            return $"({x},{y}, {(IsAlive ? "Alive" : "Dead")})";
        }

        public Cell BecomeAlive()
        {
            return new Cell { x = this.x, y = this.y, IsAlive = true };
        }
        public Cell Die()
        {
            return new Cell { x = this.x, y = this.y, IsAlive = false };
        }

    }

}
