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
                Trip trip = Trip.Create(route);

                trips.AddRange(
                    route.To.FindTripsTo(trip, targetNode, maxStops, minStops, allowMultiVisit, distanceThreshold)
                    );
            }
            return trips;
        }

        internal List<Trip> FindTripsTo(
            Trip trip,
            char targetNode,
            int maxStops,
            int minStops,
            bool allowMultiVisit = true,
            long? distanceThreshold = null)
        {
            if (IsDistanceThresholdAchieved())
                return EmptyTripsList();

            List<Trip> trips = new();

            if (isMinNumberOfStopsAchieved())
            {
                if (IsTargetStopAchieved())
                {
                    trips.Add(trip);
                    if (!isDistanceThresholdEnabled())
                        return trips;
                }
            }
            if (isMaxNumberOfStopsAchieved())
                return EmptyTripsList();

            foreach (var route in RoutesTo)
            {
                if (!allowMultiVisit)
                {
                    if (trip.StopVisited(route.To) && route.To.Name != targetNode)
                        continue;
                }

                var copyOfTrip = trip.Clone();
                copyOfTrip.AddRoute(route);
                trips.AddRange(route.To.FindTripsTo(copyOfTrip, targetNode, maxStops, minStops, allowMultiVisit, distanceThreshold));
            }
            return trips;

            bool IsDistanceThresholdAchieved()
                => isDistanceThresholdEnabled() && trip.Distance >= distanceThreshold;

            bool IsTargetStopAchieved() => trip.To.Name == targetNode;

            bool isDistanceThresholdEnabled() => distanceThreshold.HasValue;

            bool isMaxNumberOfStopsAchieved() => maxStops == trip.NumberOfStops;

            bool isMinNumberOfStopsAchieved() => trip.NumberOfStops >= minStops;

            List<Trip> EmptyTripsList() => new();
        }

        public void AddRouteTo(Node toNode, int distance) =>
            RoutesTo.RegisterRoute(Route.Create(this, toNode, distance));

        public static Node Create(char name) => new(name);

        public override string ToString() => Name.ToString();

        public bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals(object? obj) => Equals(obj as Node);
    }
}