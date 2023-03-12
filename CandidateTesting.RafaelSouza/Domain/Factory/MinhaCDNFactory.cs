using CandidateTesting.RafaelSouza.Domain.Entities;

namespace CandidateTesting.RafaelSouza.Domain.Factory
{
    public class MinhaCDNFactory
    {
        private string _mySource;

        public MinhaCDNFactory(string mySource)
        {
            _mySource = mySource;
        }

        public MinhaCDN Create()
        {
            var minhaCdn = new MinhaCDN();
            minhaCdn.logs = new List<Log>();

            foreach (var item in GetLines())
            {
                if (string.IsNullOrWhiteSpace(item))
                    break;

                var fields = GetFields(item);
                
                var log = new Log();                
                log.ResponseSize = fields[0];
                log.StatusCode = fields[1];
                log.CacheStatus = fields[2];
                log.UriPath = fields[3];
                log.TimeTaken = fields[4];
                log.HttpMethod = log.UriPath.Split("/")[0].Replace("\"", "").Trim();

                minhaCdn.logs.Add(log);
            }

            return minhaCdn;
        }

        private List<string> GetLines()
        {
            List<string> lines = new List<string>();
            foreach (var item in _mySource.Split("\r\n"))
            {
                lines.Add(item);
            }
            return lines;
        }

        private List<string> GetFields(string line)
        {
            return line.Split("|").ToList();
        }
    }
}
