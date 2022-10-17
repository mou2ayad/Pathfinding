using Pathfinding.Models;

namespace Test.Pathfinding.TestDouble
{
    internal class TestableNode : Node
    {
        public TestableNode(char name) : base(name)
        {
        }

        public TestableNode WithRoutesTo(Routes routes)
        {
            RoutesTo = routes;
            return this;
        }
    }
}