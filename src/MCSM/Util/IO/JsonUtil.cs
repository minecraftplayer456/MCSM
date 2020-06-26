using System.Text.Json;

namespace MCSM.Util.IO
{
    /// <summary>
    ///     Utility class for serializing and deserializing json / json files
    /// </summary>
    //TODO Add json file support
    public class JsonUtil
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        /// <summary>
        ///     Serializes an object into the json as string
        /// </summary>
        /// <param name="value">object to be serialized</param>
        /// <typeparam name="TValue">object type</typeparam>
        /// <returns>serialized json string</returns>
        public static string Serialize<TValue>(TValue value)
        {
            return JsonSerializer.Serialize(value, Options);
        }

        /// <summary>
        ///     Deserializes an object from string
        /// </summary>
        /// <param name="json">json object string to deserialize</param>
        /// <typeparam name="TValue">type of object</typeparam>
        /// <returns>deserialized object</returns>
        public static TValue Deserialize<TValue>(string json)
        {
            return JsonSerializer.Deserialize<TValue>(json, Options);
        }
    }
}