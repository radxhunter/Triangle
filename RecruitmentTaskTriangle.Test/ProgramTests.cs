using Moq;
using NUnit.Framework;
using RecruitmentTaskTriangle.Console;
using RecruitmentTaskTriangle.Console.Interfaces;

namespace RecruitmentTaskTriangle.Test;

public class ProgramTests
{
    private readonly Mock<IConsoleWrapper> _mockConsoleWrapper = new();

    private Program _program = null!;

    [SetUp]
    public void SetUp()
    {
        _mockConsoleWrapper.Reset();

        _program = new Program();
    }

    [Test]
    public void Run_InvalidInput()
    {
        // Arrange
        _mockConsoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("2")
            .Returns("invalid")
            .Returns("3");

        // Act
        _program.Run(_mockConsoleWrapper.Object);


        // Assert
        _mockConsoleWrapper.Verify(x => x.WriteLine("Enter side 1:"), Times.Once);
        _mockConsoleWrapper.Verify(x => x.WriteLine("Enter side 2:"), Times.Once);
        _mockConsoleWrapper.Verify(x => x.WriteLine("Enter side 3:"), Times.Exactly(2));
        _mockConsoleWrapper.Verify(x => x.WriteLine("Invalid input. Please enter a number."), Times.Once);
        _mockConsoleWrapper.Verify(x => x.WriteLine("It a not a valid triangle."), Times.Once);
    }

    [TestCase(3, 3, 3, "equilateral")]
    [TestCase(6, 6, 10, "isosceles")]
    [TestCase(3, 4, 5, "scalene")]
    [TestCase(4, 4, 10, "not a valid")]
    public void Run_ValidInput_Returns(double a, double b, double c, string type)
    {
        // Arrange
        _mockConsoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns(a.ToString())
            .Returns(b.ToString())
            .Returns(c.ToString());

        // Act
        _program.Run(_mockConsoleWrapper.Object);

        // Assert
        _mockConsoleWrapper.Verify(x => x.WriteLine("Enter side 1:"), Times.Once);
        _mockConsoleWrapper.Verify(x => x.WriteLine("Enter side 2:"), Times.Once);
        _mockConsoleWrapper.Verify(x => x.WriteLine("Enter side 3:"), Times.Once);
        _mockConsoleWrapper.Verify(x => x.WriteLine("Invalid input. Please enter a number."), Times.Never);
        _mockConsoleWrapper.Verify(x => x.WriteLine($"It a {type} triangle."), Times.Once);
    }
}
