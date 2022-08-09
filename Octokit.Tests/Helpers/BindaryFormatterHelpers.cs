using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Octokit.Tests.Helpers
{
    public class BinaryFormatterExtensions
    {
        public static T SerializeAndDeserializeObject<T>(T input)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, input);
                stream.Position = 0;
                formatter.Binder = new SerializationBinderHelper();
                var deserializedObject = formatter.Deserialize(stream);
                var deserialized = (T)deserializedObject;
                return deserialized;
            }
        }

        internal class SerializationBinderHelper : SerializationBinder
        {
            public string Name { get; set; }

            public override Type BindToType(string i_AssemblyName, string i_TypeName)
            {
                Type typeToDeserialize = Type.GetType(String.Format(" {0}, {1}", i_TypeName, i_AssemblyName)); return typeToDeserialize;
            }
        }
    }
}
