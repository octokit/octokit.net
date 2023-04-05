using System.Collections.Generic;
using System.Diagnostics;

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

    public Runner(long id, string name, string os, string status, bool busy, List<Label> labels)
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
    public List<Label> Labels { get; private set; }
  }
}
