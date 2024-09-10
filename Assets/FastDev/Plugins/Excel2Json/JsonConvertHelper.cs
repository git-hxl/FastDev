namespace Excel2Json
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
#if UNITY_5_3_OR_NEWER
    using UnityEngine; 
#endif

    // Solutions to prevent serialization errors. Seen in https://forum.unity.com/threads/jsonserializationexception-self-referencing-loop-detected.1264253/
    // Newtonsoft struggles serializing structs like Vector3 because it has a property .normalized
    // that references Vector3, and thus entering a self-reference loop throwing circular reference error.
    // Add the class to BootstrapJsonParser

    public static class JsonConvertHelper
    {

#if UNITY_5_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void ApplyCustomConverters()
        {
            ConfigureJsonInternal();
        }
#endif
        public static void ConfigureJsonInternal()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new ColorConverter());
                settings.Converters.Add(new Vector2Converter());
                settings.Converters.Add(new Vector3Converter());
                settings.Converters.Add(new Vector4Converter());
                return settings;
            };
        }

        /// <summary>
        /// 根据名称返回类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetTypeByString(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "uint":
                    return typeof(uint);
                case "int":
                    return typeof(int);
                case "long":
                    return typeof(long);
                case "float":
                    return typeof(float);
                case "double":
                    return typeof(double);
                case "bool":
                    return typeof(bool);
                case "string":
                    return typeof(string);

                case "vector2":
                    return typeof(Vector2);
                case "vector3":
                    return typeof(Vector3);
                case "vector4":
                    return typeof(Vector4);
                case "color":
                    return typeof(Color);

                case "int[]":
                    return typeof(int[]);
                case "long[]":
                    return typeof(long[]);
                case "float[]":
                    return typeof(float[]);
                case "double[]":
                    return typeof(double[]);
                case "bool[]":
                    return typeof(bool[]);
                case "string[]":
                    return typeof(string[]);

                case "vector2[]":
                    return typeof(Vector2[]);
                case "vector3[]":
                    return typeof(Vector3[]);
                case "vector4[]":
                    return typeof(Vector4[]);
                case "color[]":
                    return typeof(Color[]);

                case "date":
                case "datetime":
                    return typeof(DateTime);
                default:
                    return typeof(string);
            }
        }
    }


    public class ColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            //JObject obj = new JObject() { ["r"] = value.r, ["g"] = value.g, ["b"] = value.b, ["a"] = value.a };
            JArray array = new JArray() { value.r, value.g, value.b, value.a };
            array.WriteTo(writer);
        }
        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            //JObject obj = JObject.Load(reader);
            JArray array = JArray.Load(reader);
            return new Color((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
        }
    }
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            //JObject obj = new JObject() { ["x"] = value.x, ["y"] = value.y };
            JArray array = new JArray() { value.x, value.y };
            array.WriteTo(writer);
        }
        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            //JObject obj = JObject.Load(reader);
            JArray array = JArray.Load(reader);
            return new Vector2((float)array[0], (float)array[1]);
        }
    }
    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            JArray array = new JArray() { value.x, value.y, value.z };
            array.WriteTo(writer);
        }
        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            return new Vector3((float)array[0], (float)array[1], (float)array[2]);
        }
    }
    public class Vector4Converter : JsonConverter<Vector4>
    {
        public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
        {
            JArray array = new JArray() { value.x, value.y, value.z, value.w };
            array.WriteTo(writer);
        }
        public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            return new Vector4((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
        }
    }

}
