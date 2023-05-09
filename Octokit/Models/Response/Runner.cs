using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
  [DebuggerDisplay("{DebuggerDisplay,nq}")]
  public class Runner
  {
    public Runner() { }

    public Runner(long id)
    {
      Id = id;
    }

    public Runner(long id, string name, string os, string status, bool busy, List<RunnerLabel> labels)
    {
      Id = id;
      Name = name;
      Os = os;
      Status = status;
      Busy = busy;
      Labels = labels;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Os { get; private set; }
    public string Status { get; private set; }
    public bool Busy { get; private set; }
    public IReadOnlyList<RunnerLabel> Labels { get; private set; }

    internal string DebuggerDisplay
    {
      get
      {
        return string.Format(CultureInfo.InvariantCulture,
          "Runner Id: {0}; Name: {1}; OS: {2}; Status: {3}; Busy: {4}; Labels: {5};",
          Id, Name, Os, Status, Busy, string.Join(", ", Labels.Select(l => l.Name)));
      }
    }
  }
}
