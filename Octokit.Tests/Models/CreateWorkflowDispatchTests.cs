using System;
using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class CreateWorkflowDispatchTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CreateWorkflowDispatch(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                Assert.Throws<ArgumentException>(() => new CreateWorkflowDispatch(""));
            }
        }

        [Fact]
        public void CanBeSerialized()
        {
            var item = new CreateWorkflowDispatch("main");

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Serialize(item);

            Assert.Equal(@"{""ref"":""main""}", payload);
        }

        [Fact]
        public void CanBeSerializedWithInputs()
        {
            var item = new CreateWorkflowDispatch("main");

            item.Inputs = new Dictionary<string, object>()
            {
                ["foo"] = 1,
                ["bar"] = "qux",
            };

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Serialize(item);

            Assert.Equal(@"{""ref"":""main"",""inputs"":{""foo"":1,""bar"":""qux""}}", payload);
        }
    }
}
