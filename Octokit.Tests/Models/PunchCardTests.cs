using System;
using System.Collections.Generic;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class PunchCardTests
{
    public class TheConstructor
    {
        [Fact]
        public void ThrowsExceptionWithNullPunchCardPoints()
        {
            Assert.Throws<ArgumentNullException>(() => new PunchCard(null));
        }

        [Fact]
        public void ThrowsExceptionWhenPunchCardPointsHaveIncorrectFormat()
        {
            IList<int> point1 = new[] { 1, 2, 3, 4, 5, 6 };
            IEnumerable<IList<int>> points = new List<IList<int>> { point1 };
            Assert.Throws<ArgumentException>(() => new PunchCard(points));
        }

        [Fact]
        public void DoesNotThrowExceptionWhenPunchPointsHaveCorrectFormat()
        {
            IList<int> point1 = new[] { 1, 2, 3 };
            IEnumerable<IList<int>> points = new List<IList<int>> { point1 };
            Assert.DoesNotThrow(() => new PunchCard(points));
        }

        [Fact]
        public void CanQueryCommitsForDayAndHour()
        {
            IList<int> point1 = new[] { 1, 0, 3 };
            IList<int> point2 = new[] { 1, 1, 4 };
            IList<int> point3 = new[] { 1, 2, 0 };
            IEnumerable<IList<int>> points = new List<IList<int>> { point1, point2, point3 };

            var punchCard = new PunchCard(points);

            var commitsAtMondayAt12Am = punchCard.GetCommitCountFor(DayOfWeek.Monday, 0);
            var commitsAtMondayAt1Am = punchCard.GetCommitCountFor(DayOfWeek.Monday, 1);
            var commitsAtMondayAt2Am = punchCard.GetCommitCountFor(DayOfWeek.Monday, 2);
            var commitsAtTuesdayAt2Am = punchCard.GetCommitCountFor(DayOfWeek.Tuesday, 2);

            Assert.Equal(3, commitsAtMondayAt12Am);
            Assert.Equal(4, commitsAtMondayAt1Am);
            Assert.Equal(0, commitsAtMondayAt2Am);
            Assert.Equal(0, commitsAtTuesdayAt2Am);
        }

        [Fact]
        public void SetsPunchPointsAsReadOnlyDictionary()
        {
            IList<int> point1 = new[] { 1, 0, 3 };
            IList<int> point2 = new[] { 1, 1, 4 };
            IList<int> point3 = new[] { 1, 2, 0 };
            IEnumerable<IList<int>> points = new List<IList<int>> { point1, point2, point3 };

            var punchCard = new PunchCard(points);

            AssertEx.IsReadOnlyCollection<PunchCardPoint>(punchCard.PunchPoints);
        }
    }
}