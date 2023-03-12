using CandidateTesting.RafaelSouza.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.RafaelSouza.Domain.Interface
{
    public interface ILog
    {
        List<Log> logs { get; set; }

        string PrintLogs();
    }
}
