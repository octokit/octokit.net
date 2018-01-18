using System;

namespace Octokit.Reactive
{
    public interface IObservableApplicationsClient
    {
        IObservable<Application> Create();
    }
}
