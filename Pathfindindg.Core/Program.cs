﻿// See https://aka.ms/new-console-template for more information

using Pathfindindg.app;
using Pathfinding.Errors;
using Pathfinding.Models;

var choices = new List<string>() { "1", "2", "3", "4" };
Console.WriteLine("Insert the routes : 'ex': AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
var input = Console.ReadLine();

try
{
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
    Console.WriteLine(ex.Message);
    Console.ReadLine();
}

void execute(string question, Func<string, object> action)
{
    Console.WriteLine(question);
    input = Console.ReadLine();
    var result = action(input);
    Console.WriteLine(result);
}

void FindDistanceOfRoute(Map map)
{
    Console.WriteLine("Insert the route to calculate the trip distance : 'ex': A-B-C");
    input = Console.ReadLine();

    var result = map.CalculateTripDistance(input);
    Console.WriteLine(result);
}
void NumberOfTripsWithMaximumStops(Map map)
{
    Console.WriteLine("Insert From & to & Max Stops: 'ex': A B 5");
    input = Console.ReadLine();
    var splits = input.Split(" ");
    var parsedInput = Helper.ParseTripString(input);
    var result = map.FindNumOfTripsWithMaximumStops(parsedInput.From, parsedInput.To, parsedInput.maxNumOfStops);
    Console.WriteLine(result);
}
void NumberOfTripsWithExactStops(Map map)
{
    Console.WriteLine("Insert From & to & Number of Stops: 'ex': A B 5");
    input = Console.ReadLine();
    var splits = input.Split(" ");
    var parsedInput = Helper.ParseTripString(input);
    var result = map.FindNumOfTripsToWithExactNumOfStops(parsedInput.From, parsedInput.To, parsedInput.maxNumOfStops);
    Console.WriteLine(result);
}

string chooseChallange()
{
    Console.WriteLine("Choose The Challenge:");
    Console.WriteLine("1- Find Distance of Route");
    Console.WriteLine("2- Number of Trips with maximum stops");
    Console.WriteLine("3- Number of Trips with exact number of stops");
    string choice = Console.ReadLine();
    if (!choices.Contains(choice))
    {
        Console.WriteLine("Invalid Choice... Try again:");
        return chooseChallange();
    }
    return choice;
}