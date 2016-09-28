using Cake.Core;
using Cake.Frosting;

public class Program
{
    public static int Main(string[] args)
    {
        // Create the host.
        var host = new CakeHostBuilder()
            .WithArguments(args)
            .ConfigureServices(services =>
            {
                // Use a custom settings class.
                services.UseContext<BuildContext>();

                // Use a custom lifetime to initialize the context.
                services.UseLifetime<BuildLifetime>();

                // Use the parent directory as the working directory.
                services.UseWorkingDirectory("..");
            })
            .Build();

        // Run the host.
        return host.Run();
    }
}