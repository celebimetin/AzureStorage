using AzureStorageLibrary;
using AzureStorageLibrary.Services;
using System;
using System.Threading.Tasks;

namespace QueueConsoleApp
{
    internal class Program
    {
        private async static Task Main(string[] args)
        {
            ConnectionStrings.AzureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=udemyrealstorageaccount;AccountKey=kCxkM3J1A4DQIhoWDZVXr0JbXIaiv43oBDsjXqzEHYGJ3piMPRRXJpov1zFzjaniERUKEUxBZhTG+AStDsIJHA==;EndpointSuffix=core.windows.net";

            AzureQueue azureQueue = new AzureQueue("testkuyruk");

            //string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("mcelebi"));
            //azureQueue.SendMessageAsync(base64).Wait();

            //var message = azureQueue.RetrieveNextMessageAsync().Result;
            //string text = Encoding.UTF8.GetString(Convert.FromBase64String(message.MessageText));

            //Console.WriteLine("Mesaj " + text);

            //await azureQueue.DeleteMessageAsync(message.MessageId, message.PopReceipt);

            //Console.WriteLine("Mesaj silindi");
        }
    }
}