namespace Subnetter.Models
{
    public class Network
    {
        public string NetworkIP { get; set; }
        public string SubnetMask { get; set; }
        public string FirstIP { get; set; }
        public string LastIP { get; set; }
        public string BroadcastIP { get; set; }
    }
}
