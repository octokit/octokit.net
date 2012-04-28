using System.Threading.Tasks;
using Burr.Http;
using Moq;

namespace Burr.Tests.TestHelpers
{
    public static class MoqExtensions
    {
        public static Mock<IApplication> ApplicationMock()
        {
            var app = new Mock<IApplication>();
            app.Setup(x => x.Call(It.IsAny<Env<string>>()))
                .Returns(Task.FromResult(app.Object));

            return app;
        }
    }
}
