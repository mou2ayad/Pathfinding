using Pathfinding.Errors;
using Pathfinding.Models;

namespace Test.Pathfinding.Test.Models
{
    public class RouteRegistrationRequestTest
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void GivenDistanceLessThanOrEqualZero_ShouldThrowInvalidInputException(int distance)
        {
            var from = 'A';
            var to = 'B';

            var ex = Assert.Throws<InvalidInputException>(() => RouteRegistrationRequest.Create(from, to, distance));

            Assert.That(ex.Message, Is.EqualTo("invalid distance value, distance should be > 0"));
        }

        [Test]
        public void GivenFromEqualsToToNode_ShouldThrowInvalidInputException()
        {
            var from = 'A';
            var to = 'A';

            var ex = Assert.Throws<InvalidInputException>(() => RouteRegistrationRequest.Create(from, to, 10));
            Assert.That(ex.Message, Is.EqualTo("invalid route from node to same node"));
        }

        [Test]
        public void GivenValidInputs_ShouldReturnNewInstanceFromRouteRegistrationRequest()
        {
            var from = 'A';
            var to = 'B';
            var distance = 10;

            var instance = RouteRegistrationRequest.Create(from, to, distance);

            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.GetType(), Is.EqualTo(typeof(RouteRegistrationRequest)));
            Assert.That(instance.From, Is.EqualTo(from));
            Assert.That(instance.To, Is.EqualTo(to));
            Assert.That(instance.Distance, Is.EqualTo(distance));
        }
    }
}