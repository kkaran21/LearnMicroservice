namespace CommandService.EventProcessor
{
    public interface IEventProcessor
    {
        void processEvent(string message);
    }
}