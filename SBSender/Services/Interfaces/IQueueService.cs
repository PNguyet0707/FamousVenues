namespace SBSender.Services.Interfaces
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage);
        Task SendBatchMessageAsync<T>(IList<T> serviceBusMessages);
        Task SendMessageToTopicAsync<T>(T serviceBusMessage);
    }
}
