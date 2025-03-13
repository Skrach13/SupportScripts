using System;
using System.Collections.Generic;

public class CommandProcessor : ICommandProcessor
{
    private readonly IGameStateProvider _gameStateProvider;
    private readonly Dictionary<Type, object> _hendlesMap = new();

    public CommandProcessor(IGameStateProvider gameStateProvider)
    {
        _gameStateProvider = gameStateProvider;
    }
    public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
        _hendlesMap[typeof(TCommand)] = handler;
    }
    public bool Process<TCommand>(TCommand command) where TCommand : ICommand
    {
        if(_hendlesMap.TryGetValue(typeof(TCommand), out var handler))
        {
            var typeHandler = (ICommandHandler<TCommand>)handler;
            var result = typeHandler.Handle(command);

            if (result)
            {
                _gameStateProvider.SaveGameState();
            }

            return result;
        }

        return false;
    }

}