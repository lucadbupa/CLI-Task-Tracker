using Backend_Task_Tracker;
using Serilog;
namespace Backend_Task_Tracker
{
    internal class Program
    {
        internal static readonly string Path = "Task.json";
        static void Main(String[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();
            LogicHandler.TaskMain(args);
        }
    }
}

