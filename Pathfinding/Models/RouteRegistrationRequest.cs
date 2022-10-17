using Pathfinding.Errors;

namespace Pathfinding.Models
{
    public class RouteRegistrationRequest
    {
        private RouteRegistrationRequest(char from, char to, int distance)
        {
            From = from;
            To = to;
            Distance = distance;
            Validate();
        }

        public static RouteRegistrationRequest Create(char from, char to, int distance)
            => new(from, to, distance);

        public char From { get; }
        public char To { get; }
        public int Distance { get; }

        private void Validate()
        {
            EnsureSourceAndTargetAreNotSame();
            EnsureDistanceMoreThanZero();

            void EnsureSourceAndTargetAreNotSame()
            {
                if (To == From) InvalidInputException.Throw("invalid route from node to same node");
            }
            void EnsureDistanceMoreThanZero()
            {
                if (Distance <= 0) InvalidInputException.Throw("invalid distance value, distance should be > 0");
            }
        }
    }
}