using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Client for accessing contents of files within a repository as base64 encoded content.
    /// </summary>
    public interface IRepositoryContentsClient
    {
        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path);

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name);

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference);

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference);

        /// <summary>
        /// Gets the preferred README for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        Task<Readme> GetReadme(string owner, string name);

        /// <summary>
        /// Gets the perferred README's HTML for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        Task<string> GetReadmeHtml(string owner, string name);

        /// <summary>
        /// This method will return a 302 to a URL to download a tarball or zipball archive for a repository.
        /// Please make sure your HTTP framework is configured to follow redirects or you will need to use the 
        /// Location header to make a second GET request.
        /// Note: For private repositories, these links are temporary and expire quickly.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        [Obsolete("Use GetArchive to download the archive instead")]
        Task<string> GetArchiveLink(string owner, string name);

        /// <summary>
        /// Get an archive of a given repository's contents
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The binary contents of the archive</returns>
        Task<byte[]> GetArchive(string owner, string name);

        /// <summary>
        /// This method will return a 302 to a URL to download a tarball or zipball archive for a repository.
        /// Please make sure your HTTP framework is configured to follow redirects or you will need to use the 
        /// Location header to make a second GET request.
        /// Note: For private repositories, these links are temporary and expire quickly.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <returns></returns>
        [Obsolete("Use GetArchive to download the archive instead")]
        Task<string> GetArchiveLink(string owner, string name, ArchiveFormat archiveFormat);

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <returns>The binary contents of the archive</returns>
        Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat);

        /// <summary>
        /// This method will return a 302 to a URL to download a tarball or zipball archive for a repository.
        /// Please make sure your HTTP framework is configured to follow redirects or you will need to use the 
        /// Location header to make a second GET request.
        /// Note: For private repositories, these links are temporary and expire quickly.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <returns></returns>
        [Obsolete("Use GetArchive to download the archive instead")]
        Task<string> GetArchiveLink(string owner, string name, ArchiveFormat archiveFormat, string reference);

        /// <summary>
        /// Get an archive of a given repository's contents, using a specific format and reference
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <returns>The binary contents of the archive</returns>
        Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference);

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <param name="timeout"> Time span until timeout </param>
        /// <returns>The binary contents of the archive</returns>
        Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference, TimeSpan timeout);

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        /// <returns></returns>
        Task<RepositoryContentChangeSet> CreateFile(string owner, string name, string path, CreateFileRequest request);

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        /// <returns>The updated content</returns>
        Task<RepositoryContentChangeSet> UpdateFile(string owner, string name, string path, UpdateFileRequest request);

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        Task DeleteFile(string owner, string name, string path, DeleteFileRequest request);
    }

    /// <summary>
    /// The archive format to return from the server
    /// </summary>
    public enum ArchiveFormat
    {
        /// <summary>
        /// The TAR archive format
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tarball")]
        [Parameter(Value = "tarball")]
        Tarball,

        /// <summary>
        /// The ZIP archive format
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Zipball")]
        [Parameter(Value = "zipball")]
        Zipball
    }
}