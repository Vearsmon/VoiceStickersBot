namespace VoiceStickersBot.Core.CommandArguments.CommandArgumentsFactory;

public abstract class CommandArgumentsFactoryBase<TCommandArgument> : ICommandArgumentsFactory
    where TCommandArgument : ICommandArguments
{
    public abstract IReadOnlyList<string> CommandPrefixes { get; }

    public ICommandArguments CreateCommand(RequestContext requestContext)
    {
        throw new NotImplementedException();
    }

    protected abstract ICommandArguments CreateCommand(TCommandArgument commandArgument);
}