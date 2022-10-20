using Pathfinding.Errors;
using System.Text.RegularExpressions;

namespace Pathfindindg.app
{
    internal class Helper
    {
        public static List<(char From, char To, int distance)> ParseMapStringToTuples(string? input)
        {
            if (string.IsNullOrEmpty(input))
                InvalidInputException.Throw($"routes can't be null or empty");

            var routes = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            List<(char From, char To, int distance)> output = new();
            foreach (var route in routes)
            {
                if (!Regex.IsMatch(route, "^[A-Za-z]{2}[0-9]{1,6}$"))
                    InvalidInputException.Throw($"invalid route {route}");
                output.Add((route[0], route[1], int.Parse(route.Substring(2))));
            }
            return output;
        }

        public static (char From, char To, int maxNumOfStops) ParseTripStringWithStops(string? input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var stops = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
                if (stops.Count == 3)
                {
                    if (Regex.IsMatch(stops[0], "^[A-Za-z]$") &&
                        Regex.IsMatch(stops[1], "^[A-Za-z]$") &&
                        int.TryParse(stops[2], out int num))
                        return new(stops[0][0], stops[1][0], num);
                }
            }
            throw new InvalidInputException($"invalid input");
        }

        public static (char From, char To) ParseTripString(string? input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var stops = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
                if (stops.Count == 2)
                {
                    if (Regex.IsMatch(stops[0], "^[A-Za-z]$") &&
                        Regex.IsMatch(stops[1], "^[A-Za-z]$"))
                        return new(stops[0][0], stops[1][0]);
                }
            }
            throw new InvalidInputException($"invalid input");
        }
    }
}