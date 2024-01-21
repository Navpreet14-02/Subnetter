using Subnetter.Business;
using Subnetter.Common;
using Subnetter.Common.Enums;

namespace Subnetter.View
{
    public class App
    {


        public void Start()
        {

            var inputValidator = new InputValidation();


            Console.WriteLine("Welcome to Subnetter!");


            Console.WriteLine("Give the NetworkIP (x.x.x.x/n): ");

            string network;
            while (true)
            {
                var input = Console.ReadLine();
                if (!inputValidator.ValidateIP(input))
                {
                    Console.WriteLine("Enter Valid IP: ");
                    continue;
                }
                network = input;
                break;
            }

            Console.WriteLine(@"Choose the basis to subnet the network:\n1.By Subnets\n2.By Hosts");

            SubnettingType subnetBy;

            while (true)
            {
                var input = Console.ReadLine();
                if (!inputValidator.ValidateSubnetType(input))
                {
                    Console.WriteLine("Choose Valid Option: ");
                    continue;
                }
                subnetBy = (SubnettingType)Convert.ToInt32(input);
                break;
            }

            Console.Write("Give the no of subnets or hosts: ");
            int requirement;
            while (true)
            {
                var input = Console.ReadLine();
                if (!inputValidator.ValidateRequirement(input))
                {
                    Console.WriteLine("Choose Valid Option: ");
                    continue;
                }
                requirement = Convert.ToInt32(input);
                break;
            }

            Console.WriteLine(@"Where do you want to print all subnets:\n1.On Console\n2.In a file");
            LogOptions logOption;
            while (true)
            {
                var input = Console.ReadLine();
                if (!inputValidator.ValidateLogOptions(input))
                {
                    Console.WriteLine("Choose Valid Option: ");
                    continue;
                }
                logOption = (LogOptions)Convert.ToInt32(input);
                break;
            }


            var addBus = new IPAddressBusiness();
            var logger = new LogFile();


            NetworkBusiness networkBus;

            if (logOption == LogOptions.OnConsole)
            {
                networkBus = new NetworkBusiness(addBus, new LogConsole());

            }
            else
            {
                networkBus = new NetworkBusiness(addBus, new LogFile());

            }



            var networkIPAndMask = network.Split('/');

            networkBus.GetSubnets(networkIPAndMask[0], Convert.ToInt32(networkIPAndMask[1]), subnetBy, requirement);
        }



    }
}
