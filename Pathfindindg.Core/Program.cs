// See https://aka.ms/new-console-template for more information

using Pathfindindg.app;
using Pathfinding.Errors;
using Pathfinding.Models;

var choices = new List<string>() { "1", "2", "3", "4", "5" };

while(true)
{
    try
    {
        Console.WriteLine("Insert the routes : 'ex': AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
        var input = Console.ReadLine();

        var tuples = Helper.ParseMapStringToTuples(input);
        Map map = new Map();
        foreach (var tuple in tuples)
        {
            map.RegisterRoute(RouteRegistrationRequest.Create(tuple.From, tuple.To, tuple.distance));
        }

        while (true)
        {
            try
            {
                switch (chooseChallange())
                {
                    case "1":
                        FindDistanceOfRoute(map);
                        break;

                    case "2":
                        NumberOfTripsWithMaximumStops(map);
                        break;

                    case "3":
                        NumberOfTripsWithExactStops(map);
                        break;

                    case "4":
                        ShortestRoute(map);
                        break;

                    case "5":
                        NumberOfPossibleRoutesWithLessThanDistance(map);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is BaseInvalidInputException)
                    Console.WriteLine(ex.Message);
                else
                    throw;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);        ;
    }
}

void FindDistanceOfRoute(Map map)
{
    Console.WriteLine("Insert the route to calculate the trip distance : 'ex': A-B-C");
    var input = Console.ReadLine();

    var result = map.CalculateTripDistance(input);
    Console.WriteLine(result);
}
void NumberOfTripsWithMaximumStops(Map map)
{
    Console.WriteLine("Insert From & to & Max Stops: 'ex': C C 3");
    var input = Console.ReadLine();
    var splits = input.Split(" ");
    var parsedInput = Helper.ParseTripStringWithStops(input);
    var result = map.FindNumOfTripsWithMaximumStops(parsedInput.From, parsedInput.To, parsedInput.maxNumOfStops);
    Console.WriteLine(result);
}
void NumberOfTripsWithExactStops(Map map)
{
    Console.WriteLine("Insert From & to & Number of Stops: 'ex': A C 4");
    var input = Console.ReadLine();
    var splits = input.Split(" ");
    var parsedInput = Helper.ParseTripStringWithStops(input);
    var result = map.FindNumOfTripsToWithExactNumOfStops(parsedInput.From, parsedInput.To, parsedInput.maxNumOfStops);
    Console.WriteLine(result);
}
void ShortestRoute(Map map)
{
    Console.WriteLine("Insert From & to [ex => A C]");
    var input = Console.ReadLine();
    var splits = input.Split(" ");
    var parsedInput = Helper.ParseTripString(input);
    var result = map.FindShortestRouteLength(parsedInput.From, parsedInput.To);
    Console.WriteLine(result);
}
void NumberOfPossibleRoutesWithLessThanDistance(Map map)
{
    Console.WriteLine("Insert From & to [ex => C C 30]");
    var input = Console.ReadLine();
    var splits = input.Split(" ");
    var parsedInput = Helper.ParseTripStringWithStops(input);
    var result = map.FindNumberOfPossibleRoutesWithLessThanDistance(parsedInput.From, parsedInput.To, parsedInput.maxNumOfStops);
    Console.WriteLine(result);
}
string chooseChallange()
{
    Console.WriteLine("Choose The Challenge:");
    Console.WriteLine("1- Find Distance of Route");
    Console.WriteLine("2- Number of Trips with maximum stops");
    Console.WriteLine("3- Number of Trips with exact number of stops");
    Console.WriteLine("4- Find shortest route between 2 stops");
    Console.WriteLine("5- Find number of possible routes with distance less than specific threshold");
    string choice = Console.ReadLine();
    if (!choices.Contains(choice))
    {
        Console.WriteLine("Invalid Choice... Try again:");
        return chooseChallange();
    }
    return choice;
}