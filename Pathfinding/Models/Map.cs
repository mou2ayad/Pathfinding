using Pathfinding.Errors;
using System.Text.RegularExpressions;

namespace Pathfinding.Models
{
    public class Map
    {
        private Dictionary<char, Node> _nodes;

        public Map() => _nodes = new();

        public void RegisterRoute(RouteRegistrationRequest request)
            => RegisterNode(request.From).AddRouteTo(RegisterNode(request.To), request.Distance);

        private Node RegisterNode(char name)
        {
            if (!_nodes.TryGetValue(name, out var node))
            {
                node = Node.Create(name);
                _nodes.Add(name, node);
            }
            return node;
        }

        public long CalculateTripDistance(string? tripString)
        {
            var trip = parseTripRequest(tripString);

            EnsureStopsIsPartOfTheMap(trip.ToArray());

            var currentTripStop = trip.First;
            var currentMapNode = _nodes[currentTripStop.Value];
            currentTripStop = currentTripStop.Next;
            long distance = 0;
            while (currentTripStop != null)
            {
                var subRoute = currentMapNode.GetRouteToNode(currentTripStop.Value);
                if (subRoute == null)
                    RouteNotFoundException.Throw();
                distance += subRoute.Distance;
                currentMapNode = subRoute.To;
                currentTripStop = currentTripStop.Next;
            }
            return distance;
        }

        public int FindNumOfTripsWithMaximumStops(char from, char to, int maxStops)
          => FindTrips(from, to, maxStops, 0).Count;

        public int FindNumOfTripsToWithExactNumOfStops(char from, char to, int NumOfStops)
            => FindTrips(from, to, NumOfStops, NumOfStops).Count;

        public long FindShortestRouteLength(char from, char to)
        {
            var trips = FindTrips(from, to, int.MaxValue, 0, false);

            if (trips.Count == 0)
                RouteNotFoundException.Throw();

            return trips.Min(e => e.Distance);
        }

        public int FindNumberOfPossibleRoutesWithLessThanDistance(char from, char to, long distanceThreshold)
        {
            var trips = FindTrips(from, to, int.MaxValue, 0, true, distanceThreshold);

            return trips.Count;
        }

        private List<Trip> FindTrips(char from, char to, int maxStops, int minStops = 0, bool allowMultiVisit = true, long? distanceThreshold = null)
        {
            EnsureStopsIsPartOfTheMap(from, to);
            return _nodes[from].FindTripsTo(to, maxStops, minStops, allowMultiVisit, distanceThreshold);
        }

        private void EnsureStopsIsPartOfTheMap(params char[] stops)
        {
            foreach (var stop in stops.Distinct())
                if (!_nodes.TryGetValue(stop, out _))
                    InvalidInputException.Throw($"Stop [{stop}] is not a part of the Map");
        }

        private static LinkedList<char> parseTripRequest(string? tripString)
        {
            if (string.IsNullOrEmpty(tripString))
                InvalidInputException.Throw($"trip string can't be null or empty");

            var stops = tripString.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList(); ;
            LinkedList<char> output = new();
            foreach (var stop in stops)
            {
                if (!Regex.IsMatch(stop, "^[A-Za-z]{1}$"))
                    InvalidInputException.Throw($"invalid trip request the request");
                output.AddLast(stop[0]);
            }
            return output;
        }
    }
}