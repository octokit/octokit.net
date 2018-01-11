using System;

namespace Octokit.Reactive
{
    public interface IObservableApplicationClient
    {
        IObservable<Application> Create();
    }
}
