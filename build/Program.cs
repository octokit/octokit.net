using Cake.Frosting;

public class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<Context>()
            .UseLifetime<Lifetime>()
            .Run(args);
;
    }
}