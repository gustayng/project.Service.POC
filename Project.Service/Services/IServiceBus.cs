using System.Threading.Tasks;

namespace Cart.Common.Services
{
    public interface IServiceBus
    {
        Task SendMessageAsync(string MSIClientID, string jsonMessage, string connectionString);
        Task SendMessageToTopicAsync(string MSIClientID, string connectionString, string topicName, string messageBody);
    }
}