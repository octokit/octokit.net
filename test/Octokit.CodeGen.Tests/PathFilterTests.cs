using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class PathFilterTests
    {

        [Fact]
        public void Allow_WhenUnset_BlocksEverything()
        {
            var pathFilter = new PathFilter();

            var items = new List<PathResult> {{
                new PathResult()
                {
                    Path = "/foo/bar/baz"
                }
            }};

            Assert.Empty(pathFilter.Filter(items));
        }

        [Fact]
        public void Allow_WithPrefixMatchingPath_PassesThrough()
        {
            var pathFilter = new PathFilter();

            pathFilter.Allow("/foo");

            var items = new List<PathResult> {{
                new PathResult()
                {
                    Path = "/foo/bar/baz"
                }
            }};

            Assert.Single(pathFilter.Filter(items));
        }

        // TODO: we should filter deprecated paths as we don't care about
        //       backwards compatibility currently
    }
}
