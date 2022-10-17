using Pathfinding.Models;

namespace Test.Pathfinding.Test.Models
{
    public class RouteTest
    {
        [Test]
        public void GivenTwoRoutesWithSameFromAndTwo_TheyShouldBeEqual()
        {
            var from = Node.Create('A');
            var to = Node.Create('C');

            var first = Route.Create(from, to, 10);
            var second = Route.Create(from, to, 5);

            Assert.That(second, Is.EqualTo(first));
        }

        [Test]
        public void GivenTwoRoutesWithDiffName_TheyShouldBeNotEqual()
        {
            var from = Node.Create('A');
            var to = Node.Create('C');

            var first = Route.Create(to, from, 10);
            var second = Route.Create(from, to, 5);

            Assert.That(second, Is.Not.EqualTo(first));
        }

        [Test]
        public void GivenRoute_ToStringShouldBeAsExpected()
        {
            var from = Node.Create('A');
            var to = Node.Create('C');
            var expectedRouteToString = "A->C";
            var route = Route.Create(from, to, 10);

            Assert.That(route.ToString(), Is.EqualTo(expectedRouteToString));
        }
    }
}