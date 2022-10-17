namespace Pathfinding.Errors
{
    public class InvalidInputException : BaseInvalidInputException
    {
        public InvalidInputException(string? message) : base(message)
        {
        }

        public static void Throw(string message) => throw new InvalidInputException(message);
    }
}