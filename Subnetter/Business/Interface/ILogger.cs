using Subnetter.Models;

namespace Subnetter.Business.Interface
{
    public interface ILogger
    {
        void Log(List<Network> subnets);
    }
}
