using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Program));
            host.Open();
            foreach (var ea in host.Description.Endpoints)
            {
                Console.WriteLine(ea.Address);
            }
            Console.ReadLine();
        }
    }
}
