﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Check Suites API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
    /// </remarks>
    public interface IObservableCheckSuitesClient
    {
        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        IObservable<CheckSuite> Get(string owner, string name, long checkSuiteId);

        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        IObservable<CheckSuite> Get(long repositoryId, long checkSuiteId);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        IObservable<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        IObservable<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        IObservable<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        IObservable<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        /// <param name="options">Options to change the API response</param>
        IObservable<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        /// <param name="options">Options to change the API response</param>
        IObservable<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options);

        /// <summary>
        /// Updates Check Suites prefrences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        IObservable<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences);

        /// <summary>
        /// Updates Check Suites prefrences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        IObservable<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences);

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        IObservable<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite);

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        IObservable<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite);

        /// <summary>
        /// Triggers GitHub to create a new check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Details of the Check Suite request</param>
        IObservable<bool> Request(string owner, string name, CheckSuiteTriggerRequest request);

        /// <summary>
        /// Triggers GitHub to create a new check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Details of the Check Suite request</param>
        IObservable<bool> Request(long repositoryId, CheckSuiteTriggerRequest request);
    }
}