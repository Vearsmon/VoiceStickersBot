namespace VoiceStickersBot.Core.CommandHandler;

public interface ICommandHandler<in TCommand>
    where TCommand: ICommand
{
    Type CommandType { get; }
    
    ICommandResult Handle(TCommand command);
}