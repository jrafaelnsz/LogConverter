using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.RafaelSouza.Domain.Entities
{
    public class Log
    {
        public string?  HttpMethod { get; set; }
        public string? StatusCode { get; set; }
        public string? UriPath { get; set; }
        public string? TimeTaken { get; set; }
        public string? ResponseSize { get; set; }
        public string? CacheStatus { get; set; }
    }
}
