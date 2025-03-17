using RecruitmentTaskTriangle.Console.Interfaces;

namespace RecruitmentTaskTriangle.Console;

public class ConsoleWrapper : IConsoleWrapper
{
    public void WriteLine(string message) => System.Console.WriteLine(message);
    public string? ReadLine() => System.Console.ReadLine();
}
