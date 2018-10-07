using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProjectCardMove
    {
        public ProjectCardMove(ProjectCardPosition position, int columnId, int? cardId)
        {
            ColumnId = columnId;
            switch (position)
            {
                case ProjectCardPosition.Top:
                    Position = "top";
                    break;
                case ProjectCardPosition.Bottom:
                    Position = "bottom";
                    break;
                case ProjectCardPosition.After:
                    Ensure.ArgumentNotNull(cardId, nameof(cardId));
                    Position = string.Format(CultureInfo.InvariantCulture, "after:{0}", cardId);
                    break;
            }
        }

        public string Position { get; private set; }

        public int ColumnId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Position: {0}", Position);
            }
        }
    }

    public enum ProjectCardPosition
    {
        Top,
        Bottom,
        After
    }
}
