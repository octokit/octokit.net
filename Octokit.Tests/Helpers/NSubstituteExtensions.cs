using System;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;

public static class NSubstituteExtensions
{
    public static ConfiguredCall ReturnsAsync<TTask, TTaskType>(
        this TTask value, Func<CallInfo, TTaskType> returnThis,
        params Func<CallInfo, TTaskType>[] returnThese) where TTask : Task<TTaskType>
    {
        return value.Returns(callInfo => Task.Factory.StartNew(() => returnThis(callInfo)));
    }

    public static ConfiguredCall ThrowsAsync<T>(this Task<T> value, Exception exception)
    {
        return value.ReturnsAsync<Task<T>, T>(callInfo => { throw exception; });
    }
}
