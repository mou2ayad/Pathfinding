namespace Pathfinding.Models
{
    internal class Route : IEquatable<Route>
    {
        public int Distance { get; }
        public Node From { get; }
        public Node To { get; }

        private Route(Node from, int distance, Node to)
        {
            Distance = distance;
            From = from;
            To = to;
        }

        public static Route Create(Node from, Node to, int distance) => new Route(from, distance, to);

        public bool Equals(Route? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override int GetHashCode() => From.GetHashCode() ^ To.GetHashCode();

        public override bool Equals(object? obj) => Equals(obj as Route);

        public override string ToString() => $"{From}->{To}";
    }
}