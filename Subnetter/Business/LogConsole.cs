using Subnetter.Business.Interface;
using Subnetter.Models;

namespace Subnetter.Business
{
    public class LogConsole : ILogger
    {
        public void Log(List<Network> subnets)
        {


            Console.WriteLine("The required subnets are: ");

            foreach (var subnet in subnets)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Network IP: {subnet.NetworkIP}");
                Console.WriteLine($"First IP: {subnet.FirstIP}");
                Console.WriteLine($"Last IP: {subnet.LastIP}");
                Console.WriteLine($"Broadcast Address: {subnet.BroadcastIP}");
                Console.WriteLine($"Subnet Mask: {subnet.SubnetMask}");
                Console.WriteLine("--------------------------------");


            }
        }
    }
}
