using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octokit
{
    public class ReleaseAssetUpdate
    {
        public ReleaseAssetUpdate(string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            Name = name;
        }

        /// <summary>
        /// The file name of the asset.
        /// This field is required.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// An alternate description of the asset.
        /// Used in place of the filename.
        /// </summary>
        public string Label { get; set; }
    }
}
