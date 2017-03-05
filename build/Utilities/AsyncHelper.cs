using System;
using System.Threading;
using System.Threading.Tasks;

internal static class AsyncHelper
{
    private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

    public static void RunSync(Func<Task> func)
    {
        _myTaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
    }

    public static TResult RunSync<TResult>(Func<Task<TResult>> func)
    {
        return _myTaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
    }
}
