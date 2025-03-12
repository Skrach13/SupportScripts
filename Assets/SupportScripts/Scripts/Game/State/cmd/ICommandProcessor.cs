public interface ICommandProcessor
{
    void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
    bool Proceess<TCommand>(TCommand command) where TCommand : ICommand;
}