namespace VoiceStickersBot.Core.Commands.CommandsFactory;

public class EnterCommandFactory : CommandFactoryBase
{
    public override IReadOnlyList<string> CommandPrefixes { get; } = new[] { "pack_id" };
    public override ICommand CreateCommand(RequestContext requestContext)
    {
        return new EnterCommand.EnterCommand(requestContext);
    }
}