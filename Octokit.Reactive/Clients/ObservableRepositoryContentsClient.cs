using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// Client for accessing contents of files within a repository as base64 encoded content.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryContentsClient : IObservableRepositoryContentsClient
    {
        readonly IGitHubClient _client;

        /// <summary>
        /// Creates an instance of <see cref="ObservableRepositoryContentsClient"/>.
        /// </summary>
        /// <param name="client"></param>
        public ObservableRepositoryContentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        /// <summary>
        /// Returns the HTML rendered README.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Content.GetReadme(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns the HTML rendered README.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Readme> GetReadme(int repositoryId)
        {
            return _client.Repository.Content.GetReadme(repositoryId).ToObservable();
        }

        /// <summary>
        /// Returns just the HTML portion of the README without the surrounding HTML document. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Content.GetReadmeHtml(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns just the HTML portion of the README without the surrounding HTML document. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<string> GetReadmeHtml(int repositoryId)
        {
            return _client.Repository.Content.GetReadmeHtml(repositoryId).ToObservable();
        }

        /// <summary>
        /// Get an archive of a given repository's contents
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<byte[]> GetArchive(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetArchive(owner, name, ArchiveFormat.Tarball);
        }

        /// <summary>
        /// Get an archive of a given repository's contents
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<byte[]> GetArchive(int repositoryId)
        {
            return GetArchive(repositoryId, ArchiveFormat.Tarball);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        public IObservable<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetArchive(owner, name, archiveFormat, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        public IObservable<byte[]> GetArchive(int repositoryId, ArchiveFormat archiveFormat)
        {
            return GetArchive(repositoryId, archiveFormat, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, using a specific format and reference
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        public IObservable<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reference, "reference");

            return GetArchive(owner, name, archiveFormat, reference, TimeSpan.FromMinutes(60));
        }

        /// <summary>
        /// Get an archive of a given repository's contents, using a specific format and reference
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        public IObservable<byte[]> GetArchive(int repositoryId, ArchiveFormat archiveFormat, string reference)
        {
            Ensure.ArgumentNotNull(reference, "reference");

            return GetArchive(repositoryId, archiveFormat, reference, TimeSpan.FromMinutes(60));
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <param name="timeout"> Time span until timeout </param>
        public IObservable<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference, TimeSpan timeout)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reference, "reference");
            Ensure.GreaterThanZero(timeout, "timeout");

            return _client.Repository.Content.GetArchive(owner, name, archiveFormat, reference, timeout).ToObservable();
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <param name="timeout"> Time span until timeout </param>
        public IObservable<byte[]> GetArchive(int repositoryId, ArchiveFormat archiveFormat, string reference, TimeSpan timeout)
        {
            Ensure.GreaterThanZero(timeout, "timeout");
            Ensure.ArgumentNotNull(reference, "reference");

            return _client.Repository.Content.GetArchive(repositoryId, archiveFormat, reference, timeout).ToObservable();
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        public IObservable<RepositoryContent> GetAllContents(string owner, string name, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            return _client
                .Connection
                .GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(owner, name, path));
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The content path</param>
        public IObservable<RepositoryContent> GetAllContents(int repositoryId, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            return _client
                .Connection
                .GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(repositoryId, path));
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<RepositoryContent> GetAllContents(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client
                .Connection
                .GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(owner, name, string.Empty));
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<RepositoryContent> GetAllContents(int repositoryId)
        {
            return _client
                .Connection
                .GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(repositoryId, string.Empty));
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        /// <param name="path">The content path</param>
        public IObservable<RepositoryContent> GetAllContentsByRef(string owner, string name, string reference, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            return _client.Connection.GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(owner, name, path, reference));
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        /// <param name="path">The content path</param>
        public IObservable<RepositoryContent> GetAllContentsByRef(int repositoryId, string reference, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            return _client.Connection.GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(repositoryId, path, reference));
        }

        /// <summary>
        /// Returns the contents of the home directory in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        public IObservable<RepositoryContent> GetAllContentsByRef(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Connection.GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(owner, name, string.Empty, reference));
        }

        /// <summary>
        /// Returns the contents of the home directory in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        public IObservable<RepositoryContent> GetAllContentsByRef(int repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Connection.GetAndFlattenAllPages<RepositoryContent>(ApiUrls.RepositoryContent(repositoryId, string.Empty, reference));
        }

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        public IObservable<RepositoryContentChangeSet> CreateFile(string owner, string name, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            return _client.Repository.Content.CreateFile(owner, name, path, request).ToObservable();
        }

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        public IObservable<RepositoryContentChangeSet> CreateFile(int repositoryId, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            return _client.Repository.Content.CreateFile(repositoryId, path, request).ToObservable();
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        public IObservable<RepositoryContentChangeSet> UpdateFile(string owner, string name, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            return _client.Repository.Content.UpdateFile(owner, name, path, request).ToObservable();
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        public IObservable<RepositoryContentChangeSet> UpdateFile(int repositoryId, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            return _client.Repository.Content.UpdateFile(repositoryId, path, request).ToObservable();
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        public IObservable<Unit> DeleteFile(string owner, string name, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            return _client.Repository.Content.DeleteFile(owner, name, path, request).ToObservable();
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        public IObservable<Unit> DeleteFile(int repositoryId, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            return _client.Repository.Content.DeleteFile(repositoryId, path, request).ToObservable();
        }
    }
}
