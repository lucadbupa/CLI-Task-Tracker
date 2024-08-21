using Serilog;

namespace Backend_Task_Tracker
{
    internal class LogicHandler
    {   
        internal static void TaskMain(String[] commandsArgs)
        {
            if (!CheckAndCreateFile(Program.Path))
                Environment.Exit(1);
            var size = commandsArgs.Length;
            switch(commandsArgs[0])
            {
                case "add":
                {
                    if (size > 1) 
                    {
                        Json.AddKeyToFile(commandsArgs[1]);
                        break;
                    }
                    Log.Error("Missing argument");
                    break;
                }
                case "update":
                {
                    if (size > 2)
                    {
                        Json.UpdateFileKey(commandsArgs[1], "Description", commandsArgs[2]);
                        break;
                    }
                    Log.Error("Missing argument");
                    break;
                }
                case "delete":
                {
                    if (size > 1)
                    {
                        Json.DeleteKeyInFile(commandsArgs[1]);
                        break;
                    }
                    Log.Error("Missing argument");
                    break;
                }
                case "mark-todo":
                {
                    if (size > 1)
                    {
                        Json.UpdateFileKey(commandsArgs[1], "Status", "todo");
                        break;
                    }
                    Log.Error("Missing argument");
                    break;
                }
                case "mark-in-progress":
                {
                    if (size > 1)
                    {
                        Json.UpdateFileKey(commandsArgs[1], "Status", "in-progress");
                        break;
                    }
                    Log.Error("Missing argument");
                    break;
                }
                case "mark-done":
                {
                    if (size > 1)
                    {
                        Json.UpdateFileKey(commandsArgs[1], "Status", "done");
                        break;
                    }
                    Log.Error("Missing argument");
                    break;
                }
                case "list":
                {
                    if (size <= 1)
                    {
                        Json.ListKeysInFile(false, "");
                        break;
                    }
                    Json.ListKeysInFile(true, commandsArgs[1]);
                    break;
                }
                default:
                {
                   Log.Error(commandsArgs[0] + " Is not a valid command.");
                   break;
                }
            }
        }

        private static bool CheckAndCreateFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (var stream = File.CreateText(path))
                    {
                        stream.WriteLine("{}");
                    }
                    Thread.Sleep(1000);
                }
                return true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Path: {path}");
                return false;
            }
        }
    }   
}
