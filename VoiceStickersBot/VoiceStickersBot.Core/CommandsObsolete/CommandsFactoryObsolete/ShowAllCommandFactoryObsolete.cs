/*using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.CommandsFactory;
using VoiceStickersBot.Core.Commands.ShowAll;

namespace VoiceStickersBot.Core.CommandsObsolete.CommandsFactoryObsolete;

public class ShowAllCommandFactoryObsolete : ICommandFactoryObsolete
{
    public IReadOnlyList<string> CommandPrefixes { get; } = new[] { "Показать все", "/show_all" };
    public ICommandObsolete CreateCommand(RequestContextObsolete requestContext)
    {
        return new ShowAllCommandObsolete(requestContext);
    }
}*/