using FluentAssertions;
using Pathfinding.Models;
using Test.Pathfinding.TestDouble;

namespace Test.Pathfinding.Test.Models
{
    public class NodeTest
    {
        [Test]
        public void GivenTwoNodesWithSameName_TheyShouldBeEqual()
        {
            char name = 'A';
            var first = Node.Create(name);
            var second = Node.Create(name);

            Assert.That(second, Is.EqualTo(first));
        }

        [Test]
        public void GivenTwoNodesWithDiffName_TheyShouldBeNotEqual()
        {
            var first = Node.Create('A');
            var second = Node.Create('B');

            Assert.That(second, Is.Not.EqualTo(first));
        }

        [Test]
        public void AddingNewRouteTo_ShouldCallRegisterRouteFromRoutes()
        {
            var route = CreateRoute('A', 'B');
            var spy = new SpyRoutes();

            var node = new TestableNode('A').WithRoutesTo(spy);

            node.AddRouteTo(Node.Create('B'), 10);

            spy.RegisterRouteCalled.Should().BeTrue();
            spy.RouteAdded.Should().BeEquivalentTo(route);
        }

        private static Route CreateRoute(char from, char to, int distance = 10)
         => Route.Create(Node.Create(from), Node.Create(to), distance);
    }
}