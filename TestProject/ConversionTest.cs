using CandidateTesting.RafaelSouza.Domain.Factory;
using CandidateTesting.RafaelSouza.Infrastructure;
using System.Globalization;
using System.Text;

namespace TestProject
{
    public class ConversionTest
    {
        private string _minhaCDNlogContent;
        private string _agoralogContent;

        public ConversionTest()
        {
            var sb = new StringBuilder();
            sb.AppendLine("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2");
            sb.AppendLine("101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4");
            sb.AppendLine("199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9");
            sb.AppendLine("312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1");

            _minhaCDNlogContent = sb.ToString();

            sb = new StringBuilder();
            sb.AppendLine("#Version: 1");
            sb.AppendLine("#Date: {0}");
            sb.AppendLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");
            sb.AppendLine("\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT");
            sb.AppendLine("\"MINHA CDN\" POST 200 /myImages 319 101 MISS");
            sb.AppendLine("\"MINHA CDN\" GET 404 /not-found 142 199 MISS");
            sb.AppendLine("\"MINHA CDN\" GET 200 /robots.txt 245 312 INVALIDATE");

            _agoralogContent = sb.ToString();
        }

        [Fact]
        public void MinhaCDNFactoryCreate_ShouldReturnLogs()
        {
            var factory = new MinhaCDNFactory(_minhaCDNlogContent);
            var minhaCDN = factory.Create();
            
            Assert.Equal("312", minhaCDN.logs[0].ResponseSize);
            Assert.Equal("200", minhaCDN.logs[0].StatusCode);
            Assert.Equal("HIT", minhaCDN.logs[0].CacheStatus);
            Assert.Equal("\"GET /robots.txt HTTP/1.1\"", minhaCDN.logs[0].UriPath);
            Assert.Equal("100.2", minhaCDN.logs[0].TimeTaken);
            Assert.Equal(_minhaCDNlogContent, minhaCDN.PrintLogs());
        }

        [Fact]
        public void AgoraFactoryCreate_ShouldReturnLogs()
        {
            var minhaCDNFactory = new MinhaCDNFactory(_minhaCDNlogContent);
            var minhaCDN = minhaCDNFactory.Create();

            var agoraFactory = new AgoraFactory();
            var dateTime = DateTime.Now; 
            var agora = agoraFactory.Create(minhaCDN, dateTime);
            CultureInfo cult = new CultureInfo("pt-BR");

            Assert.Equal("312", agora.logs[0].ResponseSize);
            Assert.Equal("200", agora.logs[0].StatusCode);
            Assert.Equal("HIT", agora.logs[0].CacheStatus);
            Assert.Equal("\"GET /robots.txt HTTP/1.1\"", agora.logs[0].UriPath);
            Assert.Equal("100.2", minhaCDN.logs[0].TimeTaken);            
            Assert.Equal(String.Format(_agoralogContent, dateTime.ToString("dd/MM/yyyy HH:mm:ss", cult)), agora.PrintLogs());
        }

        [Fact]
        public void AgoraFactoryCreate_ShouldReturnANonEmpityAgoraObject()
        {
            var textFromFile = FileLog.GetFile("https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt");

            var minhaCDNFactory = new MinhaCDNFactory(textFromFile);
            var minhaCDN = minhaCDNFactory.Create();

            var agoraFactory = new AgoraFactory();            
            var agora = agoraFactory.Create(minhaCDN, DateTime.Now);            
            Assert.NotNull(agora.PrintLogs());
        }
    }
}