using Serilog;
using Serilog.Formatting.Json;
using System;

namespace Assignment.Common.Logger
{
    public class CustomLogger: ICustomLogger
    {
        public ILogger Logger { get; set; }
        public CustomLogger()
        {
            Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(new JsonFormatter()).WriteTo.Debug(outputTemplate: $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}").WriteTo.File("Logs/Custom/log.txt", rollingInterval: RollingInterval.Month)
                .CreateLogger();
        }

        public void Log(string message)
        {
            Logger.Information(message);
        }

        public void Log(string message, object data)
        {
            Logger.Information(message + "{@data}", data);
        }
    }
}
