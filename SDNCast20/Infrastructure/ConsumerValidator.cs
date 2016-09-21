using System.Collections.Generic;

namespace ConsoleApplication.Infrastructure
{
    public class ConsumerValidator : IConsumerValidator
    {
        private IDictionary<string, string> applicationRegistrations = new Dictionary<string, string>()
        {
            { "app123", "appsecret123" }
        };
        public bool Verify(string appId, string secret)
        {
            return (applicationRegistrations.ContainsKey(appId) && applicationRegistrations[appId] == secret);
        }
    }
}
