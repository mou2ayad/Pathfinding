namespace Pathfinding.Errors
{
    public class BaseInvalidInputException : Exception
    {
        protected BaseInvalidInputException(string? message) : base(message)
        {
        }

        public override string ToString()
        {
            return Message;
        }
    }
}