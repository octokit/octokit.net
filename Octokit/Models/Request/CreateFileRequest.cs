﻿using System.Diagnostics;
using System.Globalization;
using Octokit.Helpers;

namespace Octokit
{
    /// <summary>
    /// Base class with common properties for all the Repository Content Request APIs.
    /// </summary>
    public abstract class ContentRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected ContentRequest(string message)
        {
            Ensure.ArgumentNotNullOrEmptyString(message, nameof(message));

            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="branch">The branch the request is for.</param>
        protected ContentRequest(string message, string branch) : this(message)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            Branch = branch;
        }

        /// <summary>
        /// The commit message. This is required.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The branch name. If null, this defaults to the default branch which is usually "master".
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Specifies the committer to use for the commit. This is optional.
        /// </summary>
        public Committer Committer { get; set; }

        /// <summary>
        /// Specifies the author to use for the commit. This is optional.
        /// </summary>
        public Committer Author { get; set; }
    }

    /// <summary>
    /// Represents the request to delete a file in a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeleteFileRequest : ContentRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFileRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sha">The sha.</param>
        public DeleteFileRequest(string message, string sha) : base(message)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            Sha = sha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFileRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sha">The sha.</param>
        /// <param name="branch">The branch the request is for.</param>
        public DeleteFileRequest(string message, string sha, string branch) : base(message, branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            Sha = sha;
        }

        public string Sha { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SHA: {0} Message: {1}", Sha, Message);
            }
        }
    }

    /// <summary>
    /// Represents the parameters to create a file in a repository.
    /// </summary>
    /// <remarks>https://developer.github.com/v3/repos/contents/#create-a-file</remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CreateFileRequest : ContentRequest
    {
        /// <summary>
        /// Creates an instance of a <see cref="CreateFileRequest" />.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        public CreateFileRequest(string message, string content) : this(message, content, true)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFileRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="branch">The branch the request is for.</param>
        public CreateFileRequest(string message, string content, string branch) : this(message, content, branch, true)
        { }

        /// <summary>
        /// Creates an instance of a <see cref="CreateFileRequest" />.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="convertContentToBase64">True to convert content to base64.</param>
        public CreateFileRequest(string message, string content, bool convertContentToBase64) : base(message)
        {
            Ensure.ArgumentNotNull(content, nameof(content));

            if (convertContentToBase64)
            {
                content = content.ToBase64String();
            }
            Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFileRequest"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="branch">The branch the request is for.</param>
        /// <param name="convertContentToBase64">True to convert content to base64.</param>
        public CreateFileRequest(string message, string content, string branch, bool convertContentToBase64) : base(message, branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(content, nameof(content));

            if (convertContentToBase64)
            {
                content = content.ToBase64String();
            }
            Content = content;
        }

        /// <summary>
        /// The contents of the file to create, Base64 encoded. This is required.
        /// </summary>
        public string Content { get; private set; }

        internal virtual string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0} Content: {1}", Message, Content);
            }
        }
    }

    /// <summary>
    /// Represents the parameters to update a file in a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateFileRequest : CreateFileRequest
    {
        /// <summary>
        /// Creates an instance of a <see cref="UpdateFileRequest" />.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="sha">The sha.</param>
        public UpdateFileRequest(string message, string content, string sha)
            : this(message, content, sha, true)
        { }

        /// <summary>
        /// Creates an instance of a <see cref="UpdateFileRequest" />.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="sha">The sha.</param>
        /// <param name="branch">The branch the request is for.</param>
        public UpdateFileRequest(string message, string content, string sha, string branch)
           : this(message, content, sha, branch, true)
        { }

        /// <summary>
        /// Creates an instance of a <see cref="UpdateFileRequest" />.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="sha">The sha.</param>
        /// <param name="convertContentToBase64">True to convert content to base64.</param>
        public UpdateFileRequest(string message, string content, string sha, bool convertContentToBase64)
            : base(message, content, convertContentToBase64)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            Sha = sha;
        }

        /// <summary>
        /// Creates an instance of a <see cref="UpdateFileRequest" />.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="content">The content.</param>
        /// <param name="sha">The sha.</param>
        /// <param name="branch">The branch the request is for.</param>
        /// <param name="convertContentToBase64">True to convert content to base64.</param>
        public UpdateFileRequest(string message, string content, string sha, string branch, bool convertContentToBase64)
           : base(message, content, branch, convertContentToBase64)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            Sha = sha;
        }

        /// <summary>
        /// The blob SHA of the file being replaced.
        /// </summary>
        public string Sha { get; private set; }

        internal override string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SHA: {0} Message: {1}", Sha, Message);
            }
        }
    }
}
