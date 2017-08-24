using Cake.Core;
using Cake.Core.IO;

public static class CakeExtensions
{
    public static ProcessArgumentBuilder AppendIfTrue(this ProcessArgumentBuilder builder, bool condition, string format, params object[] args)
    {
        return condition ? builder.Append(format, args) : builder;
    }
}
