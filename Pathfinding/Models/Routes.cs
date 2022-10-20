using Pathfinding.Errors;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Pathfinding.Models
{
    internal class Routes
    {
        protected HashSet<Route> routes;

        public Routes() => routes = new HashSet<Route>();

        public Route? GetRouteToNode(Node Nodeto)
        {
            return routes.FirstOrDefault(r => r.To.Equals(Nodeto));
        }

        public virtual void RegisterRoute(Route route)
        {
            Validate(route);
            routes.Add(route);
        }

        private void Validate(Route route)
        {
            if (routes.Contains(route))
                InvalidInputException.Throw($"A route with same source and destination {route} is already added");
        }

        public IEnumerator<Route> GetEnumerator()
        {
            foreach (var route in routes)
                yield return route;
        }
    }
}