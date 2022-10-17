using FluentAssertions;
using Pathfinding.Errors;
using Pathfinding.Models;
using Test.Pathfinding.TestDouble;

namespace Test.Pathfinding.Test.Models
{
    public class RoutesTest
    {
        [Test]
        public void RegisteringSameRouteTwice_ShouldThrowInvalidInputException()
        {
            var existedRoute = CreateRoute('A', 'B');
            var newRoute = CreateRoute('A', 'B');
            var sut = new TestableRoutes().including(existedRoute);

            var ex = Assert.Throws<InvalidInputException>(() => sut.RegisterRoute(newRoute));
            Assert.That(ex.Message, Is.EqualTo("A route with same source and destination A->B is already added"));
        }

        [Test]
        public void RegisteringNewRoute_ShouldAddNewRouteToRoutesList()
        {
            var existedRoute = CreateRoute('A', 'B');
            var newRoute = CreateRoute('B', 'C');
            var sut = new TestableRoutes().including(existedRoute);

            sut.RegisterRoute(newRoute);

            sut.GetRoutes().Should().HaveCount(2);
            sut.GetRoutes().Should().Contain(newRoute);
        }

        private static Route CreateRoute(char from, char to, int distance = 10)
            => Route.Create(Node.Create(from), Node.Create(to), distance);
    }
}