using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;
using Moq;
using Burr.Http;
using Burr.Tests.TestHelpers;

namespace Burr.Tests
{
    public class AggregateQueryTests
    {
        public class FakeObject
        {
            public string Name { get; set; }
        }

        public class TheGetEnumeratorMethod
        {
            //[Fact(Skip="Just playing")]
            [Fact]
            public void CanEnumerate()
            {
                var q = new AggregateQuery<FakeObject>();

                q.Take(5).ToList();
            }
        }
    }
}
