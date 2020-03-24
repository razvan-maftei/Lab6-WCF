using System;
using System.ServiceModel;
using ObjectWCF;

namespace HostWCF {
    internal class Program {
        private static void Main(string[] args)
        {
            Console.WriteLine("Lansare server WCF...");
            var host = new ServiceHost(typeof(PostComment), new Uri("http://localhost:8000/PC"));
            foreach (var se in host.Description.Endpoints)
                Console.WriteLine("A (address): {0} \nB (binding): {1} \nC (Contract): {2}\n", se.Address,
                    se.Binding.Name, se.Contract.Name);
            host.Open();
            Console.WriteLine("Server in executie. Se asteapta conexiuni...");
            Console.WriteLine("Apasati Enter pentru a opri serverul!");
            Console.ReadKey();
            host.Close();
        }
    }
}