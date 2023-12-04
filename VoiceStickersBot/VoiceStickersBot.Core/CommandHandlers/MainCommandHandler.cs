namespace VoiceStickersBot.Core.CommandHandler;

public class MainCommandHandler : ICommandHandler<ICommand>
{
    public Dictionary<Type, ICommandHandler<ICommand>> CommandHandlers;

    public MainCommandHandler(List<ICommandHandler<ICommand>> commandHandlers)
    {
        CommandHandlers = commandHandlers.ToDictionary(key => key.GetType(), value => value);
    }

    public Type CommandType { get; }

    public ICommandResult Handle(ICommand command)
    {
        throw new NotImplementedException();
    }
}