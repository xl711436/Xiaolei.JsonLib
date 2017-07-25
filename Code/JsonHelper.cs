using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Xiaolei.JsonLib
{
    public class JsonHelper
    {

        public static string Object2JsonString(object I_Obj)
        {
            string R_Result = string.Empty;

            JsonSerializer serializer = new JsonSerializer();

            if (I_Obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, I_Obj);
                return textWriter.ToString();
            }

            return R_Result;
        }


        public static T JsonString2Object<T>(string I_Json) where T : class
        {
            T R_Result;

            R_Result = JsonConvert.DeserializeObject<T>(I_Json);

            return R_Result;
        }



        public static byte[] Object2ByteArray(object I_Obj)
        {
            byte[] R_Result = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (BsonWriter writer = new BsonWriter(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, I_Obj);
                }
                R_Result = ms.ToArray();
            }

            return R_Result;
        }


        public static T ByteArray2Object<T>(byte[] I_ByteArray) where T : class
        {
            T R_Result;

            using (MemoryStream ms = new MemoryStream(I_ByteArray))
            {
                using (BsonReader reader = new BsonReader(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    R_Result = serializer.Deserialize<T>(reader);
                }
            }

            return R_Result;
        }


        public static string XmlString2JsonString(string I_XmlString, string I_RootName = "Root")
        {
            string R_Result = string.Empty;



            XmlDocument doc = new XmlDocument();
            doc.LoadXml(I_XmlString);

            R_Result = JsonConvert.SerializeXmlNode(doc);

            JObject cuObe = JObject.Parse(R_Result);

            R_Result = cuObe[I_RootName].ToString();



            return R_Result;
        }



        public static string JsonString2XmlString(string I_JsonString, string I_RootName = "Root")
        {
            string R_Result = string.Empty;

            XNode node = JsonConvert.DeserializeXNode(I_JsonString, I_RootName);

            R_Result = node.ToString();

            return R_Result;
        }





    }

}
