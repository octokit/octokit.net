using System.Threading.Tasks;
using Moq;
using Nocto.Http;

namespace Nocto.Tests.TestHelpers
{
    public static class MoqExtensions
    {
        public static Mock<IApplication> ApplicationMock<T>(this Environment<T> environment)
        {
            var app = new Mock<IApplication>();
            app.Setup(x => x.Call(environment))
                .Returns(Task.FromResult(app.Object));

            return app;
        }

        public static Mock<IApplication> ApplicationMock()
        {
            var app = new Mock<IApplication>();
            app.Setup(x => x.Call(It.IsAny<Environment<string>>()))
                .Returns(Task.FromResult(app.Object));

            return app;
        }
    }
}
