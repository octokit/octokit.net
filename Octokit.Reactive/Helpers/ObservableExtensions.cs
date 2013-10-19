using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace Octokit.Reactive.Internal
{
    public static class ObservableExtensions
    {
        /*
            Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
            Microsoft Open Technologies would like to thank its contributors, a list
            of whom are at http://rx.codeplex.com/wikipage?title=Contributors.

            Licensed under the Apache License, Version 2.0 (the "License"); you
            may not use this file except in compliance with the License. You may
            obtain a copy of the License at

            http://www.apache.org/licenses/LICENSE-2.0

            Unless required by applicable law or agreed to in writing, software
            distributed under the License is distributed on an "AS IS" BASIS,
            WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
            implied. See the License for the specific language governing permissions
            and limitations under the License.
         */
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Tell this to the Rx team")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "Tell this to the Rx team")]
        internal static IObservable<TSource> Expand<TSource>(
            this IObservable<TSource> source,
            Func<TSource, IObservable<TSource>> selector,
            IScheduler scheduler)
        {
            return new AnonymousObservable<TSource>(observer =>
            {
                var outGate = new object();
                var q = new Queue<IObservable<TSource>>();
                var m = new SerialDisposable();
                var d = new CompositeDisposable {m};
                var activeCount = 0;
                var isAcquired = false;

                var ensureActive = default(Action);

                ensureActive = () =>
                {
                    var isOwner = false;

                    lock (q)
                    {
                        if (q.Count > 0)
                        {
                            isOwner = !isAcquired;
                            isAcquired = true;
                        }
                    }

                    if (isOwner)
                    {
                        m.Disposable = scheduler.Schedule(self =>
                        {
                            var work = default(IObservable<TSource>);

                            lock (q)
                            {
                                if (q.Count > 0)
                                    work = q.Dequeue();
                                else
                                {
                                    isAcquired = false;
                                    return;
                                }
                            }

                            var m1 = new SingleAssignmentDisposable();
                            d.Add(m1);
                            m1.Disposable = work.Subscribe(
                                x =>
                                {
                                    lock (outGate)
                                        observer.OnNext(x);

                                    var result = default(IObservable<TSource>);
                                    try
                                    {
                                        result = selector(x);
                                    }
                                    catch (Exception exception)
                                    {
                                        lock (outGate)
                                            observer.OnError(exception);
                                    }

                                    lock (q)
                                    {
                                        q.Enqueue(result);
                                        activeCount++;
                                    }

                                    ensureActive();
                                },
                                exception =>
                                {
                                    lock (outGate)
                                        observer.OnError(exception);
                                },
                                () =>
                                {
                                    d.Remove(m1);

                                    var done = false;
                                    lock (q)
                                    {
                                        activeCount--;
                                        if (activeCount == 0)
                                            done = true;
                                    }
                                    if (done)
                                        lock (outGate)
                                            observer.OnCompleted();
                                });
                            self();
                        });
                    }
                };

                lock (q)
                {
                    q.Enqueue(source);
                    activeCount++;
                }
                ensureActive();

                return d;
            });
        }

        internal static IObservable<TSource> Expand<TSource>(this IObservable<TSource> source,
            Func<TSource, IObservable<TSource>> selector)
        {
            return source.Expand(selector, DefaultScheduler.Instance);
        }
    }
}
