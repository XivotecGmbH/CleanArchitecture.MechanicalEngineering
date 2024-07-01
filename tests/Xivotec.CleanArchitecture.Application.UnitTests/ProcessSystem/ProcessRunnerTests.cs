using FluentAssertions;
using Xivotec.CleanArchitecture.Application.Common.Process;
using Xivotec.CleanArchitecture.Application.Common.Process.Exceptions;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ProcessSystem;

public class ProcessRunnerTests
{
    private ProcessRunner? _sut;
    private readonly ProcessSystemTestObjects _testObjects = new();

    [Fact]
    public async Task ProcessRunner_ShouldThrowProcessActionTypeUnknownException_WhenProcessActionIsUnknown()
    {
        // Arrange
        _sut = new(_testObjects.EmptyTestProcessActions, _testObjects.TestProcessDefinitions);

        // Act
        var action = () => _sut.ExecuteProcessDefinitionAsync<TestProcessDefinition>();

        // Assert
        await action.Should().ThrowAsync<ProcessActionTypeUnknownException>();
    }

    [Fact]
    public async Task ProcessRunner_ShouldThrowProcessDefinitionTypeUnknownException_WhenProcessDefinitionIsUnknown()
    {
        // Arrange
        _sut = new(_testObjects.TestProcessActions, _testObjects.EmptyTestProcessDefinitions);

        // Act
        var action = () => _sut.ExecuteProcessDefinitionAsync<TestProcessDefinition>();

        // Assert
        await action.Should().ThrowAsync<ProcessDefinitionTypeUnknownException>();
    }

    [Fact]
    public async Task ProcessRunner_ShouldReturnTaskCompleted_WhenAttributesAreValid()
    {
        // Arrange
        _sut = new(_testObjects.TestProcessActions, _testObjects.TestProcessDefinitions);

        // Act
        var task = _sut.ExecuteProcessDefinitionAsync<TestProcessDefinition>();

        // Assert
        task.Should().Be(Task.CompletedTask);

        await Task.CompletedTask;
    }
}