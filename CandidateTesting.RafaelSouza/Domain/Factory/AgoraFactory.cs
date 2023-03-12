using CandidateTesting.RafaelSouza.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.RafaelSouza.Domain.Factory
{
    public class AgoraFactory
    {
        public Agora Create(MinhaCDN minhaCDN, DateTime dateTime) 
        { 
            var agora = new Agora();
            agora.Version = 1.0;
            agora.Date = dateTime;
            agora.Fields = "provider http-method status-code uri-path time-taken response-size cache-status";
            agora.logs = minhaCDN.logs;

            return agora;
        }
    }
}
