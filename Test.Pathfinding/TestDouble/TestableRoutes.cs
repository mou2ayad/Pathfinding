using Pathfinding.Models;

namespace Test.Pathfinding.TestDouble
{
    internal class TestableRoutes : Routes
    {
        public TestableRoutes() : base()
        {
        }

        public TestableRoutes including(Route route)
        {
            routes.Add(route);
            return this;
        }

        public HashSet<Route> GetRoutes() => routes;
    }
}