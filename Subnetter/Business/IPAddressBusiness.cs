using System.Text;

namespace Subnetter.Business
{
    public class IPAddressBusiness
    {

        int[] bintoDec = { 128, 64, 32, 16, 8, 4, 2, 1 };



        public string GetNetworkIP(string inputIP, int networkBits)
        {

            var binIP = new StringBuilder(DecimalIPtoBinary(inputIP));

            for (int i = networkBits; i < inputIP.Length; i++)
            {
                if (binIP[i] == '.') continue;

                binIP[i] = '0';
            }

            return BinaryIptoDecimal(binIP.ToString());

        }


        public string DecimalIPtoBinary(string ip)
        {

            var octetsValDecimal = ip.Split('.');

            var binaryOctetsVal = new List<string>(octetsValDecimal.Count());

            for (int i = 0; i < octetsValDecimal.Count(); i++)
            {
                var octet = octetsValDecimal[i];
                binaryOctetsVal.Add(DecimalOctettoBinary(octet));
            }


            return string.Join('.', binaryOctetsVal);



        }


        public string BinaryIptoDecimal(string ip)
        {

            var octetsValBin = ip.Split('.');

            var decimalOctetsVal = new List<string>(octetsValBin.Count());

            for (int i = 0; i < octetsValBin.Count(); i++)
            {
                var octet = octetsValBin[i];
                decimalOctetsVal.Add(BinaryOctettoDecimal(octet));

            }

            return string.Join('.', decimalOctetsVal);





        }

        public string BinaryOctettoDecimal(string octet)
        {
            int decimalOctetVal = 0;


            for (int i = 0; i < octet.Length; i++)
            {
                if (octet[i] == '1')
                {
                    decimalOctetVal += bintoDec[i];
                }
            }

            return decimalOctetVal.ToString();
        }

        public string DecimalOctettoBinary(string octet)
        {


            int decimalOctetVal = int.Parse(octet.ToString());

            var binOctet = new StringBuilder("00000000");

            for (int i = 0; i < binOctet.Length; i++)
            {
                if (bintoDec[i] <= decimalOctetVal)
                {
                    decimalOctetVal -= bintoDec[i];
                    binOctet[i] = '1';
                }
            }



            return binOctet.ToString();
        }

        public string GenerateFirstIP(string networkIP)
        {

            StringBuilder sb = new StringBuilder(networkIP);

            sb[networkIP.Length - 1] = '1';

            return BinaryIptoDecimal(sb.ToString());
        }

        public string GenerateLastIP(string networkIP, int networkBits)
        {

            StringBuilder sb = new StringBuilder(JoinOctets(networkIP));

            int i = 0;
            for (i = networkBits; i < sb.Length - 1; i++)
            {
                //if (sb[i] == '.') continue;
                sb[i] = '1';
            }

            sb[sb.Length - 1] = '0';

            var ans = SeparateOctets(sb.ToString());

            return BinaryIptoDecimal(ans);
        }

        public string GenerateBroadCastAddress(string networkIP, int networkBits)
        {

            StringBuilder sb = new StringBuilder(JoinOctets(networkIP));

            int i = 0;
            for (i = networkBits; i < sb.Length; i++)
            {
                //if (sb[i] == '.') continue;

                sb[i] = '1';
            }

            var ans = SeparateOctets(sb.ToString());

            return BinaryIptoDecimal(ans);
        }

        public string GenSubnetMaskFromCIDR(int hostBits)
        {

            StringBuilder mask = new StringBuilder("00000000.00000000.00000000.00000000");


            int maskInd = 0;
            while (hostBits > 0)
            {
                if (mask[maskInd] == '.')
                {
                    maskInd++;
                    continue;
                }
                mask[maskInd] = '1';

                hostBits--;
                maskInd++;
            }


            string maskDecimal = BinaryIptoDecimal(mask.ToString());

            return maskDecimal;
        }




        public string JoinOctets(string binNetworkId)
        {
            var networkOctets = binNetworkId.Split('.');
            var joinedNetworkId = string.Join("", networkOctets);

            return joinedNetworkId;
        }

        public string SeparateOctets(string binNetworkId)
        {
            if (binNetworkId.Contains('.')) return binNetworkId;

            var sb = new StringBuilder(binNetworkId);

            sb.Insert(8, '.');
            sb.Insert(17, '.');
            sb.Insert(26, '.');


            return sb.ToString();
        }
    }
}
