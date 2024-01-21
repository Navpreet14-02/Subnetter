using System.Text.RegularExpressions;

namespace Subnetter.Common
{
    public class InputValidation
    {
        public bool ValidateIP(string networkIP)
        {
            if (!networkIP.Contains('/')) return false;

            var iPRegex = new Regex("(([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");


            return iPRegex.IsMatch(networkIP);
        }


        public bool ValidateSubnetType(string type)
        {
            int typeInt = 0;
            var isInt = int.TryParse(type, out typeInt);

            if (!isInt) { return false; }

            return typeInt == 1 || typeInt == 2;
        }

        public bool ValidateRequirement(string requirement)
        {
            int reqInt = 0;
            var isInt = int.TryParse(requirement, out reqInt);

            return isInt;
        }
        public bool ValidateLogOptions(string logOption)
        {
            int optionInt = 0;
            var isInt = int.TryParse(logOption, out optionInt);

            if (!isInt) { return false; }

            return optionInt == 1 || optionInt == 2;
        }



    }
}
