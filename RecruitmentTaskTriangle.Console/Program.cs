using RecruitmentTaskTriangle.Console.Interfaces;

namespace RecruitmentTaskTriangle.Console;

public class Program
{
    public static void Main(string[] args)
    {
        // Added wrapper to make it testable
        var console = new ConsoleWrapper();

        var program = new Program();

        program.Run(console);
    }

    public void Run(IConsoleWrapper console)
    {
        var sides = new List<double>();

        for (int i = 0; i < 3; i++)
        {
            console.WriteLine($"Enter side {i + 1}:");
            if (!double.TryParse(console.ReadLine(), out var side))
            {
                // If user inputs value that is not a number, we should inform him about it and ask for input again
                console.WriteLine("Invalid input. Please enter a number.");
                i--;
                continue;
            }

            sides.Add(side);
        }

        var type = GetTriangleType(sides);
        console.WriteLine($"It a {type} triangle.");

        return;
    }

    private static string GetTriangleType(List<double> sides)
    {
        // All sides must be greater than 0
        if (sides[0] < 0 && sides[1] < 0 && sides[2] < 0)
        {
            return "not a valid";
        }

        // Sum of two sides must be greater than the third side, for all sides
        if (sides[0] + sides[1] <= sides[2] || sides[0] + sides[2] <= sides[1] || sides[1] + sides[2] <= sides[0])
        {
            return "not a valid";
        }

        // If sides equal, it's equilateral
        if (sides[0] == sides[1] && sides[1] == sides[2])
        {
            return "equilateral";
        }

        // If two sides are equal, it's isosceles
        if (sides[0] == sides[1] || sides[1] == sides[2] || sides[0] == sides[2])
        {
            return "isosceles";
        }

        // If all sides are different, it's scalene
        return "scalene";
    }
}
