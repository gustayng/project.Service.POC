using Azure.Identity;
using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace Cart.Common.Services
{

    public class ServiceBus : IServiceBus
    {

        public async Task SendMessageAsync(string MSIClientID, string jsonMessage, string connectionString)
        {
            // create a Service Bus client 
            //await using (ServiceBusClient client = new ServiceBusClient("ikeasservicebus.servicebus.windows.net", new ManagedIdentityCredential(MSIClientID)))
            await using (ServiceBusClient client = GetServiceBusClient(MSIClientID, connectionString))
            {

                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender("cartservice");

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(jsonMessage);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: cartservice");
            }
        }

        public async Task SendMessageToTopicAsync(string MSIClientID, string connectionString, string topicName, string messageBody)
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = GetServiceBusClient(MSIClientID, connectionString))
            {
                // create a sender for the topic
                ServiceBusSender sender = client.CreateSender(topicName);
                await sender.SendMessageAsync(new ServiceBusMessage(messageBody));
                Console.WriteLine($"Sent a single message to the topic: {topicName}");
            }
        }



        private ServiceBusClient GetServiceBusClient(string MSIClientID, string connectionString)
        {
            ServiceBusClient client = null;
            if (!string.IsNullOrEmpty(MSIClientID))
            {
                client = new ServiceBusClient("ikeasservicebus.servicebus.windows.net", new ManagedIdentityCredential(MSIClientID));
            }
            else
            {
                client = new ServiceBusClient(connectionString);
            }
            return client;
        }
    }
}
