using CandidateTesting.RafaelSouza.Application;

namespace CandidateTesting.RafaelSouza
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new AppHandler().Process(args);           
        }
    }
}