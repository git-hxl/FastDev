
namespace FastDev
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using UnityEditor;
    using UnityEngine;
    // Solutions to prevent serialization errors. Seen in https://forum.unity.com/threads/jsonserializationexception-self-referencing-loop-detected.1264253/
    // Newtonsoft struggles serializing structs like Vector3 because it has a property .normalized
    // that references Vector3, and thus entering a self-reference loop throwing circular reference error.
    // Add the class to BootstrapJsonParser

    public static class JsonConvertUtil
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void ApplyCustomConverters()
        {
            ConfigureJsonInternal();
        }
#else

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
    }


    public class ColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            JObject obj = new JObject() { ["r"] = value.r, ["g"] = value.g, ["b"] = value.b, ["a"] = value.a };
            obj.WriteTo(writer);
        }
        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Color((float)obj.GetValue("r"), (float)obj.GetValue("g"), (float)obj.GetValue("b"), (float)obj.GetValue("a"));
        }
    }
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            JObject obj = new JObject() { ["x"] = value.x, ["y"] = value.y };
            obj.WriteTo(writer);
        }
        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector2((float)obj.GetValue("x"), (float)obj.GetValue("y"));
        }
    }
    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            JObject obj = new JObject() { ["x"] = value.x, ["y"] = value.y, ["z"] = value.z };
            obj.WriteTo(writer);
        }
        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector3((float)obj.GetValue("x"), (float)obj.GetValue("y"), (float)obj.GetValue("z"));
        }
    }
    public class Vector4Converter : JsonConverter<Vector4>
    {
        public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
        {
            JObject obj = new JObject() { ["x"] = value.x, ["y"] = value.y, ["z"] = value.z, ["w"] = value.w };
            obj.WriteTo(writer);
        }
        public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector4((float)obj.GetValue("x"), (float)obj.GetValue("y"), (float)obj.GetValue("z"), (float)obj.GetValue("w"));
        }
    }
}
