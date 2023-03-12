using CandidateTesting.RafaelSouza.Domain.Interface;
using System.Text;

namespace CandidateTesting.RafaelSouza.Domain.Entities
{
    public class MinhaCDN : ILog
    {
        public List<Log> logs { get; set; }

        public string PrintLogs()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in logs)
            {
                sb.AppendLine($"{item.ResponseSize}|{item.StatusCode}|{item.CacheStatus}|{item.UriPath}|{item.TimeTaken}");
            }
            return sb.ToString();
        }
    }
}
