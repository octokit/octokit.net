using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCodespace
    {
        public string MachineType { get; set; }
        public string Reference { get; set; }
        public CodespaceLocation? Location { get; set; }
        public string DisplayName { get; set; }

        public NewCodespace(Machine machineType, string reference = "main", CodespaceLocation? location = null, string displayName = null)
        {
            MachineType = machineType.Name;
            Reference = reference;
            Location = location;
            DisplayName = displayName;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "NewCodespace Repo: {0}", DisplayName);
            }
        }

    }
}
