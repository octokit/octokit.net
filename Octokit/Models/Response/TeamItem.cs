using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// organization teams - used for the list
    /// </summary>
    public class TeamItem
    {
        /// <summary>
        /// team id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; set; }
    }
}