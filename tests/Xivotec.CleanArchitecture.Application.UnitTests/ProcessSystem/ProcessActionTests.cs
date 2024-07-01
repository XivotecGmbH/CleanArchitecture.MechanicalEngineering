using FluentAssertions;
using Xivotec.CleanArchitecture.Application.Common.Process;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ProcessSystem;

public sealed class ProcessActionTests
{
    private readonly TestProcessAction _sut = new();

    [Fact]
    public async Task ProcessAction_ShouldReturnProcessDataObject_WhenRun()
    {
        // Arrange
        ProcessDataObject processData = new();

        // Act
        await _sut.ExecuteAsync(processData, default);

        // Assert
        processData.ProcessData.Should().NotBeNull();
        processData.ProcessData.Should().Contain("Addition", 2);
    }
}
