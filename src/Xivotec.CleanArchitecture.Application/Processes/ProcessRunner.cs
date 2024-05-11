using Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;
using Xivotec.CleanArchitecture.Application.Processes.Exceptions;

namespace Xivotec.CleanArchitecture.Application.Processes;

public sealed class ProcessRunner : IProcessRunner
{
    private readonly Dictionary<Type, IProcessAction> _availableActions = [];
    private readonly Dictionary<Type, IProcessDefinition> _availableDefinitions = [];

    public ProcessRunner(IEnumerable<IProcessAction> processActions,
        IEnumerable<IProcessDefinition> processDefinitions)
    {
        // Add all registered ProcessActions to the dictionary
        foreach (var action in processActions)
        {
            var actionType = action.GetType();
            _availableActions.Add(actionType, action);
        }

        // Add all registered ProcessDefinitions to the dictionary
        foreach (var definition in processDefinitions)
        {
            var definitionType = definition.GetType();
            _availableDefinitions.Add(definitionType, definition);
        }
    }

    /// <summary>
    /// Executes a Process of type <typeparamref name="TProcessDefinitionType"/>.
    /// </summary>
    /// <typeparam name="TProcessDefinitionType">Process type to be executed</typeparam>
    /// <param name="token">Optional cancellation token</param>
    public async Task ExecuteProcessDefinitionAsync<TProcessDefinitionType>(CancellationToken token = default)
    {
        var processDefinition = GetProcessDefinition(typeof(TProcessDefinitionType));

        var processDefinitionActionTypeList = processDefinition.ProcessActions;
        var processDataObject = processDefinition.ProcessDataObject;

        foreach (var actionType in processDefinitionActionTypeList)
        {
            var processAction = GetProcessAction(actionType);
            processDataObject = await processAction.ExecuteAsync(processDataObject, token);
        }

        // write back, just to be sure changes are registered
        processDefinition.ProcessDataObject = processDataObject;

        await Task.CompletedTask;
    }

    /// <summary>
    /// Returns a <see cref="IProcessAction"/> registered with the DI Container 
    /// of type <paramref name="processActionType"/>.
    /// </summary>
    /// <param name="processActionType">ProcessAction type to returned</param>
    /// <exception cref="ProcessDefinitionTypeUnknownException"></exception>
    private IProcessAction GetProcessAction(Type processActionType)
    {
        // Try get ProcessAction, throw exception if none was found
        if (_availableActions.TryGetValue(processActionType, out var processAction))
        {
            return processAction;
        }
        throw new ProcessActionTypeUnknownException($"ProcessDefinition Type {processActionType} unknown.");
    }

    /// <summary>
    /// Returns a <see cref="IProcessDefinition"/> registered with the DI Container 
    /// of type <paramref name="processDefinitionType"/>.
    /// </summary>
    /// <param name="processDefinitionType">ProcessDefinition Type to return</param>
    /// <exception cref="ProcessDefinitionTypeUnknownException"></exception>
    private IProcessDefinition GetProcessDefinition(Type processDefinitionType)
    {
        // Try get ProcessDefinition, throw exception if none was found
        if (_availableDefinitions.TryGetValue(processDefinitionType, out var processDefinition))
        {
            return processDefinition;
        }
        throw new ProcessDefinitionTypeUnknownException($"ProcessDefinition Type {processDefinitionType} unknown.");
    }
}