using VoiceStickersBot.Core.CommandArguments;

namespace VoiceStickersBot.TgGateway.CommandArgumentsFactory;

public interface ICommandArgumentsFactory
{
    public IReadOnlyList<string> CommandPrefixes { get; }
    public ICommandArguments CreateCommand(QueryContext queryContext);
}