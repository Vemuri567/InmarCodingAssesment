using Microsoft.Extensions.Configuration;

namespace SpecFlowFramework.Utility
{
    public class AppSettingsHelper
    {
        public static string GetAppSettingsValue(string key)
        {
            var adoValue = System.Environment.GetEnvironmentVariable(key);
            Console.WriteLine("Looking for " + key + " in Common");

            if (adoValue != null)
            {
                Console.WriteLine("Found ADO Secret " + key + " " + adoValue);
                return adoValue;
            }
            else
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true);
                var config = builder.Build();
                var value = config[$"{key}"];
                return value;
            }
        }
    }
}
