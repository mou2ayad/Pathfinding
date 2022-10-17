using FluentAssertions;
using Pathfinding.Errors;
using Pathfinding.Models;

namespace Test.Pathfinding.TestSenarios
{
    internal class TestScenarios
    {
        private Map _map;

        [SetUp]
        public void SetUp()
        {
            _map = new Map();
            _map.RegisterRoute(RouteRegistrationRequest.Create('A', 'B', 5));
            _map.RegisterRoute(RouteRegistrationRequest.Create('B', 'C', 4));
            _map.RegisterRoute(RouteRegistrationRequest.Create('C', 'D', 8));
            _map.RegisterRoute(RouteRegistrationRequest.Create('D', 'C', 8));
            _map.RegisterRoute(RouteRegistrationRequest.Create('D', 'E', 6));
            _map.RegisterRoute(RouteRegistrationRequest.Create('A', 'D', 5));
            _map.RegisterRoute(RouteRegistrationRequest.Create('C', 'E', 2));
            _map.RegisterRoute(RouteRegistrationRequest.Create('E', 'B', 3));
            _map.RegisterRoute(RouteRegistrationRequest.Create('A', 'E', 7));
        }

        [TestCase("A-B-C", 9)]
        [TestCase("A-E-B-C-D", 22)]
        public void TestingGettingDistanceOfRoute(string tripString, int expectedDistance)
        {
            var distance = _map.CalculateTripDistance(tripString);
            distance.Should().Be(expectedDistance);
        }

        [TestCase("A-E-D")]
        public void GivingWrongRoute_ExpectingGettingDistanceOfRoute_ToThrowNoSuchRouteError(string tripString)
        {
            var ex = Assert.Throws<RouteNotFoundException>(() => _map.CalculateTripDistance(tripString));
            ex.Message.Should().Be("NO SUCH ROUTE");
        }

        [TestCase('C', 'C', 3, 2)]
        public void TestingGettingNumberOfTripsWithMaxNumOfStops(char from, char to, int maxNumOfStops, int expectedTrips)
        {
            int numOfTrips = _map.FindNumOfTripsWithMaximumStops(from, to, maxNumOfStops);
            numOfTrips.Should().Be(expectedTrips);
        }

        [TestCase('A', 'C', 4, 3)]
        public void TestingGettingNumberOfTripsWithExactNumOfStops(char from, char to, int NumOfStops, int expectedTrips)
        {
            int numOfTrips = _map.FindNumOfTripsToWithExactNumOfStops(from, to, NumOfStops);
            numOfTrips.Should().Be(expectedTrips);
        }

        [TestCase('A', 'C', 9)]
        [TestCase('B', 'B', 9)]
        public void TestingFindingShortestRoute(char from, char to, long expectedLength)
        {
            var length = _map.FindShortestRouteLength(from, to);
            length.Should().Be(expectedLength);
        }

        [TestCase('C', 'C', 30, 7)]
        public void TestingFindNumberOfPossibleRoutesWithMaxDistance(char from, char to, long maxDistance, int expectedRoutesNumber)
        {
            var length = _map.FindNumberOfPossibleRoutesWithLessThanDistance(from, to, maxDistance);
            length.Should().Be(expectedRoutesNumber);
        }
    }
}