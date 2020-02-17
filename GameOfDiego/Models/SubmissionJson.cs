using System.Collections.Generic;

namespace GameOfDiego
{
    public class SubmissionJson
    {
        public string token { get; set; }
        public int GenerationsComputed { get; set; }
        public List<ResultBoard> ResultBoard { get; set; }
    }

}
