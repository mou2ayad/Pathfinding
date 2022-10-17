using Pathfinding.Models;

namespace Test.Pathfinding.TestDouble
{
    internal class SpyRoutes : Routes
    {
        public bool RegisterRouteCalled { get; private set; }
        public Route? RouteAdded { get; private set; }

        public override void RegisterRoute(Route route)
        {
            base.RegisterRoute(route);
            RegisterRouteCalled = true;
            RouteAdded = route;
        }
    }
}