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
            app.Setup(x => x.Invoke(environment))
                .Returns(Task.FromResult(app.Object));

            return app;
        }

        public static Mock<IApplication> ApplicationMock()
        {
            var app = new Mock<IApplication>();
            app.Setup(x => x.Invoke(It.IsAny<Environment<string>>()))
                .Returns(Task.FromResult(app.Object));

            return app;
        }
    }
}
