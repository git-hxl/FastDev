using Newtonsoft.Json;
using System;
namespace Framework.Editor
{
    internal class TypeHelper
    {
        public static Type GetTypebyValue(string value)
        {
            int intValue;
            float floatValue;
            bool boolValue;
            DateTime dateTimeValue;
            if (int.TryParse(value, out intValue))
            {
                return typeof(int);
            }
            else if (float.TryParse(value, out floatValue))
            {
                return typeof(float);
            }
            else if (bool.TryParse(value, out boolValue))
            {
                return typeof(bool);
            }
            else if (DateTime.TryParse(value, out dateTimeValue))
            {
                return typeof(DateTime);
            }
            else if (value.Contains(","))
            {
                object o = JsonConvert.DeserializeObject(value);
                return o.GetType();
            }
            return typeof(string);
        }



        public static Type GetTypeByString(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "bool":
                    return Type.GetType("System.Boolean", true, true);
                case "decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "double":
                    return Type.GetType("System.Double", true, true);
                case "float":
                    return Type.GetType("System.Single", true, true);
                case "int":
                    return Type.GetType("System.Int32", true, true);
                case "string":
                    return Type.GetType("System.String", true, true);

                case "bool[]":
                    return Type.GetType("System.Boolean[]", true, true);
                case "decimal[]":
                    return Type.GetType("System.Decimal[]", true, true);
                case "double[]":
                    return Type.GetType("System.Double[]", true, true);
                case "float[]":
                    return Type.GetType("System.Single[]", true, true);
                case "int[]":
                    return Type.GetType("System.Int32[]", true, true);
                case "string[]":
                    return Type.GetType("System.String[]", true, true);

                case "date":
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                default:
                    return Type.GetType("System.String", true, true);
            }
        }
    }
}
