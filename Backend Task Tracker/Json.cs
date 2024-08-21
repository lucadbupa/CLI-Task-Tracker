using System;
using System.Reflection;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Backend_Task_Tracker
{
    internal class Json
    {
        internal static void AddKeyToFile(string input)
        {
            try
            {
                var json = File.ReadAllText(Program.Path);
                
                var list = JsonConvert.DeserializeObject<Dictionary<string, ListItem>>(json);
                int newNumber;

                if (list.Count == 0)
                    newNumber = 1;
                else
                    newNumber = list.Count + 1;

                var time = TimeProvider.System.GetLocalNow().DateTime.ToString();
                ListItem newListItem = new()
                {
                    Description = input,
                    Status = "todo",
                    CreatedAt = time,
                    UpdatedAt = time
                };
                
                list.Add(newNumber.ToString(), newListItem);
                var text = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(Program.Path, text);
                Log.Information($"New list created with ID: {newNumber}");
            } 
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
        internal static void UpdateFileKey(string ID, string property, string input)
        {
            try
            {
                var json = File.ReadAllText(Program.Path);
                var list = JsonConvert.DeserializeObject<Dictionary<string, ListItem>>(json);
                var time = TimeProvider.System.GetLocalNow().DateTime.ToString();
                if (list.ContainsKey(ID))
                {
                    PropertyInfo propertyInfo = list[ID].GetType().GetProperty(property);
                    propertyInfo.SetValue(list[ID], Convert.ChangeType(input, propertyInfo.PropertyType), null);
                    list[ID].UpdatedAt = time;
                    var text = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText(Program.Path, text);
                    Log.Information($"Updated ID: {ID}, with new value: {input}");
                }
                else
                    Log.Error("Cannot Find ID");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

        }

        internal static void DeleteKeyInFile(string ID)
        {
            try
            {
                var json = File.ReadAllText(Program.Path);
                var list = JsonConvert.DeserializeObject<Dictionary<string, ListItem>>(json);
                if (list.ContainsKey(ID))
                {
                    list.Remove(ID);
                    var newTaskID = 1;
                    var keys = new List<string>(list.Keys);
                    foreach (var key in keys)
                    {
                        ListItem item = list[key];
                        list.Remove(key);
                        list[newTaskID.ToString()] = item;
                        newTaskID++;
                    }
                    var text = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText(Program.Path, text);
                    Log.Information($"Removed ID: {ID}");
                }
                else
                    Log.Error("Cannot Find ID");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        internal static void ListKeysInFile(bool doSpecificTask, string specificTask)
        {
           
            
            var json = File.ReadAllText(Program.Path);
            var list = JsonConvert.DeserializeObject<Dictionary<string, ListItem>>(json);
            if (!doSpecificTask)
            {
                foreach (var Key in list)
                {
                    Log.Information($"ID: {Key.Key}, Task: {Key.Value.Description}, Status: {Key.Value.Status}, Created: {Key.Value.CreatedAt}, Updated Last: {Key.Value.UpdatedAt}");
                }
            }
            else
            {
                foreach (var Key in list)
                {
                    if (Key.Value.Status == specificTask)
                    {
                        Log.Information($"ID: {Key.Key}, Task: {Key.Value.Description}, Status: {Key.Value.Status}, Created: {Key.Value.CreatedAt}, Updated Last: {Key.Value.UpdatedAt}");
                    }
                }
            }
        }       

       

        public class ListItem
        {
            public string Description {get; set; }
            public string Status { get; set; }
            public string CreatedAt { get; set; }
            public string UpdatedAt { get; set; }

        }
    }
}
