namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public class EnterCommandFactory : CommandFactoryBase
{
    public override IReadOnlyList<string> CommandPrefixes { get; }
    public override ICommand CreateCommand(CommandObject commandObject)
    {
        return new EnterCommand.EnterCommand(commandObject.RequestContext.UserBotState);
    }
}