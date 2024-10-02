using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnvironmentVariablesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new EnvironmentVariablesClient(null));
            }
        }

        public class GetAllMethod
        {
			[Fact]
			public async Task RequestsTheCorrectUrlWithoutPageCount()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new EnvironmentVariablesClient(connection);

				await client.GetAll(1, "envName");

				connection.Received()
					.Get<EnvironmentVariablesCollection>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/variables"));
			}

			[Fact]
			public async Task RequestsTheCorrectUrlWithPageCount()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new EnvironmentVariablesClient(connection);

				await client.GetAll(1, "envName", 30);

				connection.Received()
					.Get<EnvironmentVariablesCollection>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/variables?per_page=30"));
			}
			
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentVariablesClient(Substitute.For<IApiConnection>());

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(0, "envName", 10));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null, 10));

				await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(0, "envName", 10));
				await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(1, "", 10));
			}
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentVariablesClient(connection);

                await client.Get(1, "envName", "variableName");

                connection.Received()
                    .Get<EnvironmentVariable>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/variables/variableName"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentVariablesClient(Substitute.For<IApiConnection>());

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(0, "envName", "variableName"));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null, "variableName"));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, "envName", null));

				await Assert.ThrowsAsync<ArgumentException>(() => client.Get(0, "envName", "variableName"));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, "", "variableName"));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, "envName", ""));				
            }
        }

        public class CreateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentVariablesClient(connection);
                var newVariable = new Variable("variableName", "variableValue");

                await client.Create(1, "envName", newVariable);

                connection.Received()
                    .Post<EnvironmentVariable>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/variables"), newVariable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentVariablesClient(Substitute.For<IApiConnection>());

                var variable = new Variable("variableName", "variableValue");

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(0, "envName", variable));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null, variable));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, "envName", null));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, "envName", new Variable(null, "variableValue")));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, "envName", new Variable("variableName", null)));

				await Assert.ThrowsAsync<ArgumentException>(() => client.Create(0, "envName", variable));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "", variable));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "envName", new Variable("", "variableValue")));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "envName", new Variable("variableName", "")));
            }
        }

        public class UpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentVariablesClient(connection);
                var variable = new Variable("variableName", "variableValue");

                await client.Update(1, "envName", variable);

                connection.Received()
                    .Patch<EnvironmentVariable>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/variables/variableName"), variable);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentVariablesClient(Substitute.For<IApiConnection>());

                var updatedVariable = new Variable("variableName", "variableValue");

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(0, "envName", updatedVariable));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null, updatedVariable));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, "envName", null));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, "envName", new Variable(null, "variableValue")));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, "envName", new Variable("variableName", null)));

				await Assert.ThrowsAsync<ArgumentException>(() => client.Update(0, "envName", updatedVariable));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Update(1, "", updatedVariable));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Update(1, "envName", new Variable("", "variableValue")));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Update(1, "envName", new Variable("variableName", "")));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentVariablesClient(connection);

                await client.Delete(1, "envName", "variableName");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/variables/variableName"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentVariablesClient(Substitute.For<IApiConnection>());

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(0, "envName", "variableName"));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, null, "variableName"));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, "envName", null));

				await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(0, "envName", "variableName"));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, "", "variableName"));
				await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, "envName", ""));
            }
        }
    }
}
