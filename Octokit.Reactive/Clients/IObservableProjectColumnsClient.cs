﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Project Columns API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/columns/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public interface IObservableProjectColumnsClient
    {
        /// <summary>
        /// Gets all columns.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#list-project-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>        
        IObservable<ProjectColumn> GetAll(int projectId);

        /// <summary>
        /// Gets a single column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#get-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        IObservable<ProjectColumn> Get(int id);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#create-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        /// <param name="newProjectColumn">The column to create</param>
        IObservable<ProjectColumn> Create(int projectId, NewProjectColumn newProjectColumn);

        /// <summary>
        /// Updates a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#update-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        /// <param name="projectColumnUpdate">New values to update the column with</param>
        IObservable<ProjectColumn> Update(int id, ProjectColumnUpdate projectColumnUpdate);

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#delete-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        IObservable<bool> Delete(int id);

        /// <summary>
        /// Moves a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#move-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        /// <param name="position">The position to move the column</param>
        IObservable<bool> Move(int id, ProjectColumnMove position);
    }
}
