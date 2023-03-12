using CandidateTesting.RafaelSouza.Domain.Factory;
using CandidateTesting.RafaelSouza.Domain.Interface;
using CandidateTesting.RafaelSouza.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.RafaelSouza.Application
{
    public class AppHandler
    {
        public bool ValidateInputs(string url, string localPath)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri resultUrl))
            {
                Console.WriteLine("Invalid URL input.");
                return false;
            }

            if (string.IsNullOrEmpty(localPath))
            {
                Console.WriteLine("Local path cannot be null or empty.");
                return false;
            }

            return true;
        }

        public void Process(string[] args)
        {
            if(ValidateInputs(args[0], args[1]))
            {
                var textFromFile = FileLog.GetFile(args[0]);

                var minhaCDNFactory = new MinhaCDNFactory(textFromFile);
                var minhaCDN = minhaCDNFactory.Create();

                var agoraFactory = new AgoraFactory();
                var agora = agoraFactory.Create(minhaCDN, DateTime.Now);

                //Cria arquivo no local indicdo no args[1]
                FileLog.CreateLocalFile(args[1], agora.PrintLogs());

                //Exibe os logs
                List<ILog> minhaLista = new List<ILog>();
                minhaLista.Add(minhaCDN);
                minhaLista.Add(agora);

                foreach (var item in minhaLista)
                {
                    Console.WriteLine(item.PrintLogs());
                }

                Console.WriteLine("Successfully processed!");
            }            
        }
    }
}
