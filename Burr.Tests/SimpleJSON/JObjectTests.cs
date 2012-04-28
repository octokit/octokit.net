using System.Collections.Generic;
using Xunit;
using Burr.SimpleJSON;

namespace Tests.SimpleJSON
{
    class JObjectTests
    {
        [Fact]
        public void StringEquality()
        {
            Assert.Equal(JObject.CreateString("test"),
                            JObject.CreateString("test"));
            Assert.NotEqual(JObject.CreateString("test"),
                               JObject.CreateString("test2"));
        }

        [Fact]
        public void BoolEquality()
        {
            Assert.Equal(JObject.CreateBoolean(true),
                            JObject.CreateBoolean(true));
            Assert.NotEqual(JObject.CreateBoolean(true),
                               JObject.CreateBoolean(false));
        }

        [Fact]
        public void NullEquality()
        {
            Assert.Equal(JObject.CreateNull(), JObject.CreateNull());
        }

        [Fact]
        public void ArrayEquality()
        {
            Assert.Equal(JObject.CreateArray(new List<JObject> { JObject.CreateNull() }),
                            JObject.CreateArray(new List<JObject> { JObject.CreateNull() }));

            Assert.NotEqual(JObject.CreateArray(new List<JObject> { JObject.CreateNull() }),
                               JObject.CreateArray(new List<JObject> {
                                                           JObject.CreateNull(),
                                                           JObject.CreateNull()
                                                       }));
        }

        [Fact]
        public void ObjectEquality()
        {
            Assert.Equal(JObject.CreateObject(new Dictionary<string, JObject> {
                                                         { "test", JObject.CreateNull() }
                                                     }),
                            JObject.CreateObject(new Dictionary<string, JObject> {
                                                         { "test", JObject.CreateNull() }
                                                     }));

            Assert.NotEqual(JObject.CreateObject(new Dictionary<string, JObject> {
                                                            { "test", JObject.CreateNull() }
                                                        }),
                               JObject.CreateObject(new Dictionary<string, JObject> {
                                                            { "test2", JObject.CreateNull() }
                                                        }));
        }

        [Fact]
        public void NumberEquality()
        {
            Assert.Equal(JObject.CreateNumber("100", "", ""),
                            JObject.CreateNumber("100", "", ""));
            Assert.Equal(JObject.CreateNumber("-100", "", ""),
                            JObject.CreateNumber("-100", "", ""));
            Assert.Equal(JObject.CreateNumber("100", "5", ""),
                            JObject.CreateNumber("100", "5", ""));
            Assert.Equal(JObject.CreateNumber("100", "5", "2"),
                            JObject.CreateNumber("100", "5", "2"));
            Assert.Equal(JObject.CreateNumber("-100", "5", "2"),
                            JObject.CreateNumber("-100", "5", "2"));

            Assert.NotEqual(JObject.CreateNumber("-100", "5", "2"),
                               JObject.CreateNumber("100", "5", "2"));
        }
    }
}
