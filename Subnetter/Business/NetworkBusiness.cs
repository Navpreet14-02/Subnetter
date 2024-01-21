using Subnetter.Business.Interface;
using Subnetter.Common.Enums;
using Subnetter.Models;
using System.Text;

namespace Subnetter.Business
{
    public class NetworkBusiness
    {


        private IPAddressBusiness _ipBusiness;
        private ILogger _logger;

        public NetworkBusiness(IPAddressBusiness ipBusiness, ILogger logger)
        {
            _ipBusiness = ipBusiness;
            _logger = logger;
        }



        public Network CreateNetwork(string networkId, int networkBits)
        {
            var network = new Network();

            network.NetworkIP = _ipBusiness.BinaryIptoDecimal(networkId);
            network.SubnetMask = _ipBusiness.GenSubnetMaskFromCIDR(networkBits);
            network.FirstIP = _ipBusiness.GenerateFirstIP(networkId);
            network.LastIP = _ipBusiness.GenerateLastIP(networkId, networkBits);
            network.BroadcastIP = _ipBusiness.GenerateBroadCastAddress(networkId, networkBits);


            return network;
        }


        public void GetSubnets(string networkId, int networkBits, SubnettingType subnetBy, int requirement)
        {

            int addNetworkBits = GetAdditionalNetworkBits(subnetBy, requirement, networkBits);


            Console.WriteLine($"Additional Bits Added For Network:{addNetworkBits}");


            var networkIP = _ipBusiness.GetNetworkIP(networkId, networkBits);
            string binNetworkIP = _ipBusiness.DecimalIPtoBinary(networkIP);


            List<string> subnetIPStrings = new List<string>();

            var joinedNetworkId = new StringBuilder(_ipBusiness.JoinOctets(binNetworkIP));



            Subnet(joinedNetworkId, networkBits, networkBits + addNetworkBits - 1, subnetIPStrings);

            int actualNetworkBits = networkBits + addNetworkBits;


            subnetIPStrings.Sort();

            var subnets = new List<Network>();

            foreach (var subnetStr in subnetIPStrings)
            {

                var sepSubnets = _ipBusiness.SeparateOctets(subnetStr);
                subnets.Add(CreateNetwork(sepSubnets, actualNetworkBits));
            }


            _logger.Log(subnets);

        }

        public int GetAdditionalNetworkBits(SubnettingType subnetBy, int requirement, int networkBits)
        {
            int addNetworkBits = 0;

            if (subnetBy == SubnettingType.BySubnets)
            {

                int i = 0;
                while (true)
                {
                    double val = Math.Pow(2, i);

                    if (val >= requirement)
                    {
                        addNetworkBits = i;
                        break;
                    }
                    i++;

                }
            }
            else
            {

                int i = 1;
                while (true)
                {
                    double val = Math.Pow(2, i);

                    if (val >= requirement + 2)
                    {
                        addNetworkBits = 32 - networkBits - i;
                        break;
                    }
                    i++;

                }


            }

            return addNetworkBits;
        }


        public void Subnet(StringBuilder binNetworkId, int hostBitStart, int hostBitEnd, List<string> networks)
        {

            if (hostBitEnd < hostBitStart)
            {

                networks.Add(binNetworkId.ToString());
                return;
            }



            binNetworkId[hostBitEnd] = '0';
            Subnet(binNetworkId, hostBitStart, hostBitEnd - 1, networks);

            binNetworkId[hostBitEnd] = '1';
            Subnet(binNetworkId, hostBitStart, hostBitEnd - 1, networks);
            // No change to bit


            // Change bit to 1


        }




    }
}
