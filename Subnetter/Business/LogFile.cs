using Newtonsoft.Json;
using Subnetter.Business.Interface;
using Subnetter.Models;

namespace Subnetter.Business
{
    public class LogFile : ILogger
    {

        public void Log(List<Network> subnets)
        {

            string networkIp = subnets[0].NetworkIP;

            string outputPath = $@"C:\Users\nasingh\OneDrive - WatchGuard Technologies Inc\Desktop\{networkIp}.json";

            var jsonResult = JsonConvert.SerializeObject(subnets, Formatting.Indented);
            //File.Create(outputPath);

            File.WriteAllText(outputPath, jsonResult);





        }
    }
}
