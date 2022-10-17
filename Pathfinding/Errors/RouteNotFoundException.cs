namespace Pathfinding.Errors
{
    public class RouteNotFoundException : BaseInvalidInputException
    {
        private RouteNotFoundException(string? message) : base(message)
        {
        }

        public static void Throw() => throw new RouteNotFoundException("NO SUCH ROUTE");
    }
}