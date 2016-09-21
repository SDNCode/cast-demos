namespace ConsoleApplication.Infrastructure
{
    public interface IConsumerValidator
    {
        bool Verify(string appId, string secret);
    }
}
