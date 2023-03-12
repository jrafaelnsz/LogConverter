using CandidateTesting.RafaelSouza.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.RafaelSouza.Domain.Entities
{
    public class Agora : ILog
    {
        public double Version { get; set; }
        public DateTime Date { get; set; }
        public string? Fields { get; set; }
        public string? Provider { get; set; }
        public List<Log> logs { get; set; }

        public string PrintLogs()
        {
            var sb = new StringBuilder();
            CultureInfo cult = new CultureInfo("pt-BR");
            sb.AppendLine("#Version: " + Version);
            sb.AppendLine("#Date: " + Date.ToString("dd/MM/yyyy HH:mm:ss", cult));
            sb.AppendLine($"#Fields: {Fields}");
            foreach (var item in logs)
            {
                var customUriPath = item.UriPath.Split(" ")[1];
                var customTimeTaken = item.TimeTaken.Split(".")[0];
                sb.AppendLine($"\"MINHA CDN\" {item.HttpMethod} {item.StatusCode} {customUriPath} {customTimeTaken} {item.ResponseSize} {item.CacheStatus}");
            }
            return sb.ToString();
        }
    }    
}
