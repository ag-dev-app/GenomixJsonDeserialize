using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomixJsonDeserialize
{
    public class JsonFieldsCollector
    {
        private readonly Dictionary<string, JValue> fields;
        private List<string> fieldsKeys;


        public JsonFieldsCollector(JToken token)
        {
            fields = new Dictionary<string, JValue>();
            fieldsKeys = new List<string>();
            CollectFields(token);
        }

        private void CollectFields(JToken jToken)
        {
            switch (jToken.Type)
            {
                case JTokenType.Object:
                    foreach (var child in jToken.Children<JProperty>())
                        CollectFields(child);
                    break;
                case JTokenType.Array:
                    foreach (var child in jToken.Children())
                        CollectFields(child);
                    break;
                case JTokenType.Property:
                    CollectFields(((JProperty)jToken).Value);
                    break;
                default:
                    fields.Add(jToken.Path, (JValue)jToken);
                    break;
            }
        }

        public IEnumerable<KeyValuePair<string, JValue>> GetAllFields() => fields;

        //public static string AllJsonFieldsDisplay(string JsonString)
        //{
        //    var json = JToken.Parse(JsonString);
        //    var fieldsCollector = new JsonFieldsCollector(json);
        //    var fields = fieldsCollector.GetAllFields();
        //    string txt = "";
        //    foreach (var field in fields)
        //        txt += ($"{field.Key}: '{field.Value}'\r\n");
        //    return txt;
        //}

        public void SaveFieldsKeys(string JsonString)
        {
            var json = JToken.Parse(JsonString);
            var fieldsCollector = new JsonFieldsCollector(json);
            var fields = fieldsCollector.GetAllFields();
            fieldsKeys = new List<string>();
            foreach (var field in fields)
                fieldsKeys.Add($"{field.Key}");
        }

        public List<string> GetFieldsKeys()
        {
            return fieldsKeys;
        }

        public string DisplayValue(string key)
        {
            return fields[key].ToString();
        }

    }
}

