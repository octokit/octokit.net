using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// organization teams
    /// </summary>
    public class Team
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