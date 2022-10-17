namespace Pathfinding.Models
{
    internal class Trip
    {
        private LinkedList<Route> _routes;
        private LinkedList<Node> _stops;

        private Trip(Route firstRoute)
        {
            _routes = new();            
            _stops = new();
            _routes.AddFirst(firstRoute);
            _stops.AddFirst(firstRoute.From);
            _stops.AddLast(firstRoute.To);
        }

        public static Trip Create(Route firstRoute) => new Trip(firstRoute);

        public void AddRoute(Route route)
        {
            if (_routes.Last.Value.To == route.From)
            {
                _routes.AddLast(route);
                _stops.AddLast(route.To);
            }            
        }

        public Trip Clone()
        {
            var current = _routes.First;
            var trp = new Trip(_routes.First.Value);
            current = current.Next;
            while (current != null)
            {
                trp.AddRoute(current.Value);
                current = current.Next;
            }
            return trp;
        }

        public Node? From => _stops.First.Value;
        public Node? To => _stops.Last.Value;
        public int NumberOfStops => _routes.Count;
        public long Distance => _routes.Sum(r => r.Distance);
        public bool StopVisited(Node node) => _stops.Contains(node);
        public override string ToString() => string.Join("->",_stops);
    }
}