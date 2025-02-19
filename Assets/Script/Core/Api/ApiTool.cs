using System;
using Unity.Plastic.Newtonsoft.Json;
namespace GameApi
{
    public class ApiTool
    {

        // 
        public static T JsonToObject<T>(string json)
        {
            try
            {
                T value = JsonConvert.DeserializeObject<T>(json);
                return value;
            }
            catch (JsonException ex)
            {
                UnityEngine.Debug.LogError("JSON Deserialization Error: " + ex.Message);
            }
            catch (Exception) { }

            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T[] JsonToArray<T>(string json)
        {
            ListJson<T> list = new ListJson<T>();
            list.list = new T[0];
            try
            {
                string wrap_json = "{ \"list\": " + json + "}";
                list = JsonConvert.DeserializeObject<ListJson<T>>(wrap_json);
                return list.list;
            }
            catch (JsonException ex)
            {
                UnityEngine.Debug.LogError("JSON Deserialization Error: " + ex.Message);
            }
            catch (Exception) { }
            return new T[0];
        }

        public static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static int ParseInt(string int_str, int default_val = 0)
        {
            bool success = int.TryParse(int_str, out int val);
            return success ? val : default_val;
        }
    }
}