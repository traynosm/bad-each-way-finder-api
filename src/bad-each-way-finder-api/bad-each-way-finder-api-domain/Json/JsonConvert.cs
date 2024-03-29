﻿using Newtonsoft.Json;

namespace bad_each_way_finder_api_exchange.Json
{
    public class JsonConvert
    {
        public static JsonResponse<T> Import<T>(TextReader reader)
        {
            var jsonResponse = reader.ReadToEnd();
            return Deserialize<JsonResponse<T>>(jsonResponse);
        }

        public static T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        //Used for json rpc calls to create a body
        public static void Export(JsonRequest request, TextWriter writer)
        {
            var json = Serialize<JsonRequest>(request);
            writer.Write(json);
        }

        public static string Serialize<T>(T value)
        {

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            return Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
        }
    }
}
