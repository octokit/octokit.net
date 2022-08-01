using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryCodeOwnersErrors
    {
        public RepositoryCodeOwnersErrors()
        {
        }

        public RepositoryCodeOwnersErrors(List<RepositoryCodeOwnersError> errors)
        {
            Errors = errors;
        }

        public IReadOnlyList<RepositoryCodeOwnersError> Errors { get; private set; }

        [DebuggerDisplay("{DebuggerDisplay,nq}")]
        public class RepositoryCodeOwnersError
        {
            public RepositoryCodeOwnersError()
            {
            }

            public RepositoryCodeOwnersError(int line, int column, string kind, string source, string suggestion, string message, string path)
            {
                Line = line;
                Column = column;
                Kind = kind;
                Source = source;
                Suggestion = suggestion;
                Message = message;
                Path = path;
            }

            public int Line { get; private set; }

            public int Column { get; private set; }

            public string Kind { get; private set; }

            public string Source { get; private set; }
            
            public string Suggestion { get; private set; }

            public string Message { get; private set; }
            
            public string Path { get; private set; }

            internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
        }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }
}
