using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test.Pathfinding")]

namespace Pathfinding.Models
{
    internal class Node : IEquatable<Node>
    {
        public char Name { get; }
        public Routes RoutesTo { get; protected set; }

        protected Node(char name)
        {
            Name = name;
            RoutesTo = new Routes();
        }

        public Route? GetRouteToNode(char nodeName)
        {
            return RoutesTo.GetRouteToNode(Node.Create(nodeName));
        }

        internal List<Trip> FindTripsTo(char targetNode, int maxStops, int minStops, bool allowMultiVisit = true, long? distanceThreshold = null)
        {
            List<Trip> trips = new();
            foreach (var route in RoutesTo)
            {
                Trip tr = Trip.Create(route);

                trips.AddRange(route.To.FindTripsTo(tr, targetNode, maxStops, minStops, allowMultiVisit, distanceThreshold));
            }
            return trips;
        }

        internal List<Trip> FindTripsTo(Trip trip, char targetNode, int maxStops, int minStops, bool allowMultiVisit = true, long? distanceThreshold = null)
        {
            if (distanceThreshold.HasValue && trip.Distance >= distanceThreshold)
                return new List<Trip>();

            List<Trip> trips = new List<Trip>();

            if (trip.NumberOfStops >= minStops)
            {
                if (trip.To.Name == targetNode)
                {
                    trips.Add(trip);
                    if (!distanceThreshold.HasValue)
                        return trips;
                }
            }
            if (maxStops == trip.NumberOfStops)
                return new List<Trip>();

            foreach (var route in RoutesTo)
            {
                if (!allowMultiVisit)
                {
                    if (trip.StopVisited(route.To) && route.To.Name != targetNode)
                        continue;
                }

                var tr = trip.Clone();
                tr.AddRoute(route);
                trips.AddRange(route.To.FindTripsTo(tr, targetNode, maxStops, minStops, allowMultiVisit, distanceThreshold));
            }
            return trips;
        }

        public void AddRouteTo(Node toNode, int distance) =>
            RoutesTo.RegisterRoute(Route.Create(this, toNode, distance));

        public static Node Create(char name) => new(name);

        public override string ToString() => Name.ToString();

        public bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (object.ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals(object? obj) => Equals(obj as Node);
    }
}